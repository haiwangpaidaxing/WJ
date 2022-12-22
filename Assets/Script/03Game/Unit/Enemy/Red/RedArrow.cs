using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedArrow : BaseArrow
{
    public InjuredData InjuredData { get; set; }
    EnemyFinder enemyFinder;
    private void Awake()
    {
        enemyFinder = GetComponent<EnemyFinder>();
        enemyFinder.enemyCB = Attack;
    }

    public void Attack(IInjured injured)
    {
        injured.Injured(InjuredData);
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        Move();
        LookTarget();
        enemyFinder.OpenFindTargetAll();
    }

    public override void Injured(InjuredData injuredData)
    {
        Destroy(gameObject);
    }
}
    