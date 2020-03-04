using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    //Array of all target spheres in scene
    TargetSphere[] targetSpheres;

    void Awake()
    {
        targetSpheres = FindObjectsOfType<TargetSphere>();
        OrderTargetSpheres();
    }

    //Sorts all target spheres into order of [Closest to player -> Furthest from player]
    void OrderTargetSpheres()
    {
        //Bubble sort target spheres array
        TargetSphere temp;
        for (int i = 0; i < targetSpheres.Length; i++)
        {
            for (int j = 0; j < targetSpheres.Length - i - 1; j++)
            {
                if (targetSpheres[j].transform.localScale.x > targetSpheres[j+1].transform.localScale.x)
                {
                    temp = targetSpheres[j];
                    targetSpheres[j] = targetSpheres[j + 1];
                    targetSpheres[j + 1] = temp;
                }
            }
        }

        for (int i = 0; i < targetSpheres.Length; i++)
        {
            targetSpheres[i].targetIndex = i;
        }
    }
    
    public ZombieController GetTarget()
    {
        ZombieController tempTarget = null;
        for (int i = 0; i < targetSpheres.Length; i++)
        {
            tempTarget = targetSpheres[i].GetTarget();
            if (tempTarget != null)
            {
                return tempTarget;
            }
        }

        return null;
    }
}
