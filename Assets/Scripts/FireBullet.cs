using Unity.VisualScripting;
using UnityEngine;

public class FireBullet : Bullet
{
    [SerializeField] private int burnDamage = 10;
    [SerializeField] private float burnRate = 1f;
    
    protected override void HitEnemy(GameObject enemyObject)
    {
        base.HitEnemy(enemyObject);
        EnemyHealth enemyHealth = enemyObject.GetComponent<EnemyHealth>();
        enemyHealth.StartBurning(burnDamage, burnRate);
    }
}
