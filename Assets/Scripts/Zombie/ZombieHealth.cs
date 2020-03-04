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
    MeshRenderer meshRenderer;
    CapsuleCollider capsuleCollider;
    ZombiePool zombiePool;
    GameManager gameManager;

    void Awake()
    {
        currentHealth = maxHealth;
        meshRenderer = GetComponent<MeshRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        zombiePool = FindObjectOfType<ZombiePool>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        CalculateMaxHealth();
    }

    public void CalculateMaxHealth()
    {
        maxHealth = Mathf.Round(gameManager.playerLevel * healthMultiplier);
    }

    public bool DealDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && healthState == ZombieHealthState.alive)
        {
            healthState = ZombieHealthState.dead;
            meshRenderer.material.color = Color.black;
            capsuleCollider.enabled = false;
            StartCoroutine(DeathTimer());
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
        meshRenderer.material.color = Color.white;
        capsuleCollider.enabled = true;
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(3f);
        zombiePool.ReturnToPool(gameObject);
    }
}
