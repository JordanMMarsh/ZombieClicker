using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLookup : MonoBehaviour
{ 
    //Dictionary stores unique names as keys hooked up to the ZombieController component. 
    //This allows for the GetComponent calls to be made once (when a zombie is added to the pool)
    //And then just be looked up when needed
    Dictionary<string, ZombieController> zombieDictionary = new Dictionary<string, ZombieController>();
    int zombieIndex = 0;
    string zombieNaming = "Zombie";

    public void AddToDictionary(GameObject obj)
    {
        string key = zombieNaming + zombieIndex;
        obj.name = key;
        zombieDictionary.Add(key, obj.GetComponent<ZombieController>());
        zombieIndex++;
    }

    public ZombieController GetFromDictionary(string name)
    {
        ZombieController zombie;
        zombieDictionary.TryGetValue(name, out zombie);
        if (zombie != null)
        {
            return zombie;
        }
        else
        {
            return null;
        }
    }
}
