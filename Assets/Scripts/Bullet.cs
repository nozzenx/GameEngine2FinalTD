using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;

    private bool _hasHit;
    private int _bulletDamage;

    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_hasHit) return;
        if (((1 << other.gameObject.layer) & enemyLayer) == 0) return;
        
        Debug.Log("Enemy hit");
        _hasHit = true;
        GetComponent<Collider>().enabled = false;
        HitEnemy(other.gameObject);
        Destroy(gameObject);
    }

    public void SetBulletDamage(int damageAmount)
    {
        _bulletDamage = damageAmount;
    }

    protected virtual void HitEnemy(GameObject enemyObject)
    {
        enemyObject.GetComponent<EnemyHealth>().DecreaseHealth(_bulletDamage);
    }
}
