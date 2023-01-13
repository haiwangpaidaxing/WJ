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
    float t;
    private void FixedUpdate()
    {
        Move();
        //t += Time.fixedDeltaTime;
        //Vector2 startPos = transform.position;
        //Vector2 p1 = new Vector2(startPos.x, startPos.y);
        //Vector2 p2 = targetPos.position;
        //Vector2 point = CalculateCubicBezierPoint(t, startPos, p1, p2);
        //LookTarget(point);
        LookTarget();
        enemyFinder.OpenFindTargetAll();
    }

    public override void Injured(InjuredData injuredData)
    {
        Destroy(gameObject);
    }
}
