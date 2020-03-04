using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : MonoBehaviour
{
    ZombieLookup zombieLookup;

    Queue<GameObject> zombiePool = new Queue<GameObject>();
    public GameObject zombiePrefab;
    private int startingPoolSize = 10;
    private int incrementPoolSize = 10;

    void Awake()
    {
        //Fill pool at start
        zombieLookup = GetComponent<ZombieLookup>();
        FillPool(startingPoolSize);
    }

    void FillPool(int numObjects)
    {
        if (zombiePrefab == null)
        {
            Debug.LogError("No zombie prefab hooked up to zombie spawner.");
            return;
        }

        //Fill pool and deactivate zombies
        for (int i = 0; i < numObjects; i++)
        {
            GameObject newObject = Instantiate(zombiePrefab);
            zombieLookup.AddToDictionary(newObject);
            newObject.SetActive(false);
            zombiePool.Enqueue(newObject);
        }
    }

    public void ReturnToPool(GameObject zombie)
    {
        zombie.SetActive(false);
        zombiePool.Enqueue(zombie);
    }

    public GameObject GetZombie()
    {
        if (zombiePool.Count <= 0)
        {
            FillPool(incrementPoolSize);
        }

        return zombiePool.Dequeue();
    }
}
