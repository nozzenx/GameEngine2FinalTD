using UnityEngine;

public class IceBullet : Bullet
{
    [Tooltip("It divides enemy speed by speed divider. (enemySpeed/speedDivider)")]
    [SerializeField] private float speedDivider = 2;
    
    protected override void HitEnemy(GameObject enemyObject)
    {
        base.HitEnemy(enemyObject);
        EnemyMovement enemyMovement = enemyObject.GetComponent<EnemyMovement>();
        
        enemyMovement.StartIceEffect(speedDivider);
    }
}
