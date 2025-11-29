using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;

    
    [Header("Burn Config")]
    private bool _isBurning;
    private float _burnRate;
    private int _burnDamage;
    private float _burnTimer;

    public void DecreaseHealth(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_isBurning)
        {
            Burn();
        }
    }

    private void Burn()
    {
        _burnTimer += Time.deltaTime;

        if (_burnTimer >= _burnRate)
        {
            DecreaseHealth(_burnDamage);
            _burnTimer = 0;
        }
    }

    public void StartBurning(int damage, float rate)
    {
        _burnDamage = damage;
        _burnRate = rate;
        _isBurning = true;
    }
}
