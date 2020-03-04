using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSphere : MonoBehaviour
{
    ZombieLookup zombieLookup;

    Queue<ZombieController> targets = new Queue<ZombieController>();
    public int targetIndex;

    private void Start()
    {
        zombieLookup = FindObjectOfType<ZombieLookup>();
    }
    
    //On collision with a zombie, grab the zombie's controller from the dictionary and store it in queue
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            ZombieController otherController = zombieLookup.GetFromDictionary(other.name);
            otherController.currentTargetIndex = targetIndex;
            targets.Enqueue(otherController);
        }
    }
    
    //Check if next zombie in queue is alive and at or past this checkpoint.
    //If it is, pass it back to player/survivor.
    public ZombieController GetTarget()
    {
        if (targets.Count > 0)
        {
            ZombieController tempTarget;
            for (int i = 0; i < targets.Count; i++)
            {
                tempTarget = targets.Dequeue();
                if (tempTarget.currentTargetIndex <= targetIndex && tempTarget.health.healthState == ZombieHealth.ZombieHealthState.alive)
                {
                    return tempTarget;
                }
            }
        }

        return null;
    }
}
