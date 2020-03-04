using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Transform player;
    public ZombieHealth health;
    public int currentTargetIndex;
    public int currencyReward = 10;
    private float attackDistance = 2f;
    private float moveSpeed = 1f;
    
    void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
        health = GetComponent<ZombieHealth>();
    }

    private void Update()
    {
        if (health.healthState == ZombieHealth.ZombieHealthState.alive && Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            transform.LookAt(player.transform);
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
