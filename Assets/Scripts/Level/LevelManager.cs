using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    ZombieSpawnManager zombieSpawnManager;

    private void Start()
    {
        zombieSpawnManager = FindObjectOfType<ZombieSpawnManager>();
        StartCoroutine(LevelStartTimer());
    }

    //Wait at start of level before beginning zombie spawns
    private IEnumerator LevelStartTimer()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(ZombieTimer());
    }

    //Time to wait before spawning next zombie
    private IEnumerator ZombieTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            zombieSpawnManager.SpawnZombie(Random.Range(1, 3));
        }
    }
}
