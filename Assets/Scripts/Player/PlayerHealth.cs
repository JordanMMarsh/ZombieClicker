using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHitpoints = 100f;
    private float hitpoints;

    private void Start()
    {
        hitpoints = maxHitpoints;
    }

    public void DealDamage(float damage)
    {
        Debug.Log("Taking " + damage + " damage. HP is now " + hitpoints);
        hitpoints -= damage;
        if (hitpoints <= 0)
        {
            hitpoints = maxHitpoints;
            Debug.Log("You died.");
        }
    }
}
