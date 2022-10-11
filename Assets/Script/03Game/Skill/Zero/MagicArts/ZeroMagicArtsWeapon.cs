using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroMagicArtsWeapon : MonoBehaviour
{
    ///作用：标记距离范围最近的敌人 
    ///待机状态时 跟随释放着身边  角色死亡是也跟着销毁    
    ///标记状态时 获取怪物的坐标等
    Transform master;
    [SerializeField]
    public float speed;
    [SerializeField]
    EnemyFinder enemyFinder;

    public GameObject enemy;
    public enum ZeroMagicArtsWeaponState
    {
        /// <summary>
        /// 待机状态
        /// </summary>
        Idle,
        /// <summary>
        /// 标记状态
        /// </summary>
        Flag,
    }
    public ZeroMagicArtsWeaponState currentState = ZeroMagicArtsWeaponState.Idle;
    public void Init(Transform master)
    {
        this.master = master;
        currentState = ZeroMagicArtsWeaponState.Idle;
    }
    private void FixedUpdate()
    {

        switch (currentState)
        {
            case ZeroMagicArtsWeaponState.Idle:
                IdleState();
                break;
            case ZeroMagicArtsWeaponState.Flag:
                FlagState();
                break;
        }
    }
    float timer;
    private void IdleState()
    {
        //    timer += Time.deltaTime;
        if (master != null)
        {
            Vector2 tr = transform.position;
            transform.position = Vector2.Lerp(tr, master.transform.position, speed * Time.deltaTime);
        }
        //Debug.Log(Vector3.Distance(transform.position, master.position));
        //Vector2 tr = transform.position;
        //if (Vector3.Distance(transform.position, master.position) >= 2.5f)
        //{
        //    transform.position = Vector2.Lerp(tr, master.transform.position, 0.5f* Time.deltaTime);
        //}
        //else
        //{
        //    transform.position = Vector2.Lerp(tr, master.transform.position, speed * Time.deltaTime);
        //}
    }
    public void Use()
    {
        IInjured injured = enemyFinder.FindCloseRange(ref enemy);
        if (injured == null)
        {
            return;
        }
        enemy = enemy.GetComponent<RoleController>().injuredPos.gameObject;
        currentState = ZeroMagicArtsWeaponState.Flag;
    }
    private void FlagState()
    {
        //得角公式：float 角 = Vector2.Angle(Vector2.up, 向量)
        // Vector2 v = enemy.transform.position - transform.position;
        if (enemy == null)
        {
            currentState = ZeroMagicArtsWeaponState.Idle;
        }
        Vector3 targtePos = enemy.transform.position;
        if (Vector2.Distance(transform.position, targtePos) > 0.1f)
        {
            Vector2 v = targtePos - transform.position;
            float r;
            r = Vector2.Angle(Vector2.up, v);
            Vector3 vr = new Vector3();
            vr.z = r;
            if (targtePos.x > transform.position.x)
            {
                vr.y = 180;
            }
            else
            {
                vr.y = 0;
            }
            transform.eulerAngles = vr;
            transform.position = Vector2.Lerp(transform.position, targtePos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = targtePos;
        }
    }


    public void Recycle()
    {
        enemy = null;
        currentState = ZeroMagicArtsWeaponState.Idle;
    }

}
