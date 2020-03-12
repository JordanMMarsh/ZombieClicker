using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public enum ZombieHealthState
    {
        alive,
        dead
    };
    public ZombieHealthState healthState = ZombieHealthState.alive;
    private float maxHealth = 20f;
    private float currentHealth;
    private float healthMultiplier = 1.2f;
    SphereCollider sphereCollider;
    ZombiePool zombiePool;
    ZombieController zombieController;
    GameManager gameManager;

    void Awake()
    {
        currentHealth = maxHealth;
        sphereCollider = GetComponent<SphereCollider>();
        zombiePool = FindObjectOfType<ZombiePool>();
        zombieController = GetComponent<ZombieController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        CalculateMaxHealth();
    }

    public void CalculateMaxHealth()
    {
        //maxHealth = Mathf.Round(gameManager.playerLevel * healthMultiplier);
        maxHealth = 100f;
        currentHealth = maxHealth;
    }

    public bool DealDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && healthState == ZombieHealthState.alive)
        {
            healthState = ZombieHealthState.dead;
            sphereCollider.enabled = false;
            zombieController.StopAttack();
            StartCoroutine(DeathTimer());
            transform.Rotate(-90f, 0f, 0f);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetAlive()
    {
        currentHealth = maxHealth;
        healthState = ZombieHealthState.alive;
        sphereCollider.enabled = true;
        transform.Rotate(90f, 0f, 0f);
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(3f);
        zombiePool.ReturnToPool(gameObject);
    }
}
