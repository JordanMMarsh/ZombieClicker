using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    private float weaponDamage = 1f;
    private float playerLevelMultiplier = 0.4f;
    private float fireDelay = 1f;

    TargetManager targetManager;
    ZombieController target;
    ZombieLookup zombieLookup;
    int layerMask = 1 << 9;

    GameManager gameManager;

    void Start()
    {
        cam = Camera.main;
        zombieLookup = FindObjectOfType<ZombieLookup>();
        gameManager = FindObjectOfType<GameManager>();
        layerMask = ~layerMask;
        targetManager = FindObjectOfType<TargetManager>();
        GetNewTarget();
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

    //Calculate player weapon damage based on current weapon and player level
    private float CalculateWeaponDamage()
    {
        //float calculatedDamage = weaponDamage * gameManager.playerLevel * playerLevelMultiplier;
        //return calculatedDamage;
        return 1.0f;
    }

    #region Player Controlled Shooting
    void Shoot(RaycastHit hit)
    {
        transform.LookAt(hit.transform);
        ZombieController zombieHit = zombieLookup.GetFromDictionary(hit.transform.name);
        if (zombieHit != null)
        {
            bool targetDead = zombieHit.health.DealDamage(CalculateWeaponDamage());
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

    //TODO
    //This will need to be stripped out to also allow for survivors to fire
    #region Auto-Shooting
    //Attempt to get a new target from target manager
    private void GetNewTarget()
    {
        target = targetManager.GetTarget();

        //If one is found, start firing on a delay
        if (target != null)
        {
            StartCoroutine(FireDelay());
        }
        //Otherwise, start rechecking for target on a delay
        else
        {
            StartCoroutine(TargetDelay());
        }
    }

    //Small delay between checking for targets
    private IEnumerator TargetDelay()
    {
        yield return new WaitForSeconds(.1f);
        GetNewTarget();
    }

    //Delay between shooting at target
    private IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(fireDelay);
        FireAtTarget();
    }

    //If target exists and is alive, shoot it
    private void FireAtTarget()
    {
        if (target != null && target.health.healthState == ZombieHealth.ZombieHealthState.alive)
        {
            //Deal damage to target and look at them
            float weaponDmg = CalculateWeaponDamage();
            bool targetDead = target.health.DealDamage(weaponDmg);
            transform.LookAt(target.transform);

            //If damage kills target, give player currency reward and get new target
            if (targetDead)
            {
                gameManager.AddCurrency(target.currencyReward);
                GetNewTarget();
                return;
            }
            StartCoroutine(FireDelay());
        }
        else
        {
            GetNewTarget();
        }
    }
    #endregion
}
