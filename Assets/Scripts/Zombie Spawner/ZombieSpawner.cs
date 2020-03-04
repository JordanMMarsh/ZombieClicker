using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    //TODO: make zombie spawn manager to allow for multiple zombie spawners in various locations
    ZombiePool zombiePool;
    ZombieLookup zombieLookup;

    void Start()
    {
        zombiePool = FindObjectOfType<ZombiePool>();
        zombieLookup = zombiePool.GetComponent<ZombieLookup>();
    }

    //Grabs zombie from pool, setting its status to alive and placing it on spawner
    public void SpawnZombie()
    {
        //Grab zombie game object from pool and set its position to this spawner and set the object as active
        GameObject newZombie = zombiePool.GetZombie();
        newZombie.transform.position = transform.position;
        newZombie.SetActive(true);

        //Then grab its controller from the zombie lookup and recalculate max health based on player level
        ZombieController newZombieController = zombieLookup.GetFromDictionary(newZombie.name);
        newZombieController.health.CalculateMaxHealth();

        //Finally instruct zombie to reset its max health and status to alive
        newZombieController.health.SetAlive();
    }
}
