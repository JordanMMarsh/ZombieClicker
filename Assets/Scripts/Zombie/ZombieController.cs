using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    public ZombieHealth health;
    public int currentTargetIndex;
    public int currencyReward = 10;
    private float attackDistance = 2f;
    private float attackDamage = 2f;
    private float attackDelay = 2f;
    private float moveSpeed = 1f;
    private bool attackStarted = false;
    private Coroutine attackDelayCoroutine;
    
    void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        health = GetComponent<ZombieHealth>();
    }

    private void Update()
    {
        if (health.healthState == ZombieHealth.ZombieHealthState.alive && Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            transform.LookAt(player);
        }
        else if (health.healthState == ZombieHealth.ZombieHealthState.alive && Vector3.Distance(transform.position, player.position) <= attackDistance)
        {
            if (!attackStarted)
            {
                attackStarted = true;
                attackDelayCoroutine = StartCoroutine(AttackDelay());
            }
        }
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        Attack();
    }

    private void Attack()
    {
        playerHealth.DealDamage(attackDamage);
        attackDelayCoroutine = StartCoroutine(AttackDelay());
    }

    public void StopAttack()
    {
        if (attackDelayCoroutine != null)
        {
            StopCoroutine(attackDelayCoroutine);
        }        
    }
}
