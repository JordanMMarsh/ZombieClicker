using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    private float weaponDamage = 1f;
    private float levelMultiplier = 0.4f;
    private float fireDelay = 1f;

    TargetManager targetManager;
    ZombieController target;
    ZombieLookup zombieLookup;

    Weapon myWeapon;
    GameManager gameManager;

    void Start()
    {
        myWeapon = GetComponent<Weapon>();
        zombieLookup = FindObjectOfType<ZombieLookup>();
        gameManager = FindObjectOfType<GameManager>();
        targetManager = FindObjectOfType<TargetManager>();
        GetNewTarget();
    }

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
        Debug.Log(name + " is shooting at " + target.name + ".");
        if (target != null && target.health.healthState == ZombieHealth.ZombieHealthState.alive)
        {
            //Deal damage to target and look at them
            float weaponDmg = myWeapon.CalculateWeaponDamage(false);
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
