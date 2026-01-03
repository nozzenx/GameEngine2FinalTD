using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform body;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform nozzle;
    [SerializeField] private GameObject classicBulletPrefab;
    [SerializeField] private GameObject fireBulletPrefab;
    [SerializeField] private GameObject iceBulletPrefab;
    

    [Header("Config")] 
    [SerializeField] private float radius = 1.5f;
    [SerializeField] private float fireInterval = 1f;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private int damage = 50;

    private float _fireTimer;

    [Header("Debug")]
    public List<GameObject> enemiesInRange;
    public GameObject currentTarget;
    
    public TowerElement element;
    
    
    private SphereCollider _rangeCollider;
    
    
    private void Start()
    {
        _rangeCollider = GetComponent<SphereCollider>();
        
        _rangeCollider.radius = radius;
    }

    private void Update()
    {
        LookAndFireAtTarget();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) == 0) return;
        
        enemiesInRange.Add(other.gameObject);
        TryFindTarget();
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) == 0) return;
        
        enemiesInRange.Remove(other.gameObject);
        TryFindTarget();
    }

    private void LateUpdate()
    {
        enemiesInRange.RemoveAll(e => e is null || !e);

        if (!currentTarget)
        {
            TryFindTarget();
            return;
        }
    }

    private void TryFindTarget()
    {
        if(currentTarget && enemiesInRange.Contains(currentTarget)) return;


        currentTarget = enemiesInRange.Count > 0 ? enemiesInRange[0] : null;
    }

    private void LookAndFireAtTarget()
    {
        if (currentTarget)
        {
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            body.transform.rotation = Quaternion.LookRotation(direction); 
            
            _fireTimer += Time.deltaTime;
            if (_fireTimer >= fireInterval)
            {
                Fire(direction);
                _fireTimer = 0;
            }
        }
    }
    private void Fire(Vector3 direction) // Assagi ates etmiyor !!!!!!!!!!!!!!!!
    {
        switch (element)
        {
            case TowerElement.Fire:
                GameObject fireBullet = Instantiate(fireBulletPrefab, nozzle.position, Quaternion.LookRotation(direction));
                fireBullet.GetComponent<Bullet>().SetBulletDamage(damage);
                fireBullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
                break;
            case TowerElement.None:
                GameObject bullet = Instantiate(classicBulletPrefab, nozzle.position, Quaternion.LookRotation(direction));
                bullet.GetComponent<Bullet>().SetBulletDamage(damage);
                bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
                break;
            case TowerElement.Ice:
                GameObject iceBullet = Instantiate(iceBulletPrefab, nozzle.position, Quaternion.LookRotation(direction));
                iceBullet.GetComponent<Bullet>().SetBulletDamage(damage);
                iceBullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
                break;
        }
        
        
    }

    
}

public enum TowerElement
{
    None,
    Ice,
    Fire
}
