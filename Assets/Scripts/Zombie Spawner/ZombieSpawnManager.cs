using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    //Array of all zombie spawners on level
    ZombieSpawner[] zombieSpawners;

    //Index to store which spawner the next zombie will be spawned at
    int spawnerIndex = 0;

    private void Awake()
    {
        zombieSpawners = FindObjectsOfType<ZombieSpawner>();
    }

    public void SpawnZombie(int numZombies)
    {
        for (int i = 0; i < numZombies; i++)
        {
            if (zombieSpawners.Length <= spawnerIndex)
            {
                spawnerIndex = 0;
            }
            
            zombieSpawners[spawnerIndex].SpawnZombie();
            spawnerIndex++;
        }
    }
}
