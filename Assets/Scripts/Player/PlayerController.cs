using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera cam;
   
    private float playerLevelMultiplier = 0.4f;

    TargetManager targetManager;
    ZombieController target;
    ZombieLookup zombieLookup;
    int layerMask = 1 << 9;

    Weapon myWeapon;
    GameManager gameManager;

    void Start()
    {
        cam = Camera.main;
        myWeapon = GetComponent<Weapon>();
        zombieLookup = FindObjectOfType<ZombieLookup>();
        gameManager = FindObjectOfType<GameManager>();
        layerMask = ~layerMask;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(GetRay(), out hit, 200f, layerMask))
            {
                if (hit.transform.tag == "Zombie")
                {
                    Shoot(hit);
                }
            }
        }
    }

    #region Player Controlled Shooting
    void Shoot(RaycastHit hit)
    {
        transform.LookAt(hit.transform);
        ZombieController zombieHit = zombieLookup.GetFromDictionary(hit.transform.name);
        if (zombieHit != null)
        {
            bool targetDead = zombieHit.health.DealDamage(myWeapon.CalculateWeaponDamage(true));
            if (targetDead)
            {
                gameManager.AddCurrency(zombieHit.currencyReward);
            }
        }
    }

    Ray GetRay()
    {
        return cam.ScreenPointToRay(Input.mousePosition);
    }
    #endregion
}
