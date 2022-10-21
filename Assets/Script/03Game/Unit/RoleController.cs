using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制角色移动相关.....
/// </summary>
public class RoleController : MonoBehaviour, IInjured
{
    public List<SkillData> skillDataList;
    protected SkillManager skillManager;
    protected Rigidbody2D rig;
    public Rigidbody2D RoleRigidbody { get { return rig; } }
    public Vector2 RigVelocity { get { return rig.velocity; } }
    [Header("同步图片方向")]
    public bool syncImge = true;
    [Header("下落倍率")]
    public float fallMultiplier = 2.5f;
    public bool isFall = true;
    public float roleDir;
    public Action animatorClipCb;
    public Animator animator;
    public BaseSkill currentSkill;
    public RoleAttribute roleAttribute;
    [Header("受伤位置")]
    public Transform injuredPos;
    [SerializeField]
    PhysicsMaterial2D AirMaterial;
    [SerializeField]
    public PhysicsMaterial2D GroundMaterial;
    public virtual void Init()
    {
        AirMaterial = ResourceSvc.Single.Load<PhysicsMaterial2D>("AirMaterial");
        GroundMaterial = ResourceSvc.Single.Load<PhysicsMaterial2D>("GroundMaterial");
        injuredPos = transform.Find("InjuredPos");
        this.roleAttribute = GetComponent<RoleAttribute>();
        rig = GetComponent<Rigidbody2D>();
        skillManager = gameObject.AddComponent<SkillManager>();
        animator = GetComponent<Animator>();
        InputEvene();
    }
    public virtual void InputEvene()
    {
        InputController.Single.operaterCB = USESkill;
    }
    public virtual void USESkill(int skillID)
    {
      
    }
    private void FixedUpdate()
    {
        if (currentSkill != null)
        {
            currentSkill.OnFixedUpdate();
        }
    }

    private void Update()
    {
        if (currentSkill != null)
        {
            currentSkill.OnUpdate();
        }
    }

    public virtual void MoveX(float dir, float speed)
    {

        SyncImage(dir);

        if (dir != 0)
        {
            roleDir = dir;
        }
        rig.velocity = new Vector2(dir * speed, rig.velocity.y);
    }
    public virtual void MoveX(float dir, float speed, ForceMode2D forceMode2D)
    {
        Vector2 f = Vector2.right * dir * speed;
        rig.AddForce(f, forceMode2D);
    }

    public virtual void MoveY(float dir, float speed)
    {
        rig.velocity = new Vector2(rig.velocity.x, dir * speed);
    }
    public virtual void Move(Vector2 dir, float speed)
    {
        rig.velocity = dir * speed;
    }
    public virtual void Fall()
    {
        rig.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }
    /// <summary>
    /// 同步方向
    /// </summary>
    /// <param name="dir"></param>
    public virtual void SyncImage(float dir)
    {
        if (syncImge)
        {
            Vector3 trLocalScale;
            trLocalScale = transform.localScale;
            if (dir == 1) trLocalScale.x = Mathf.Abs(trLocalScale.x);
            if (dir == -1 && trLocalScale.x > 0) trLocalScale.x = -trLocalScale.x;
            transform.localScale = trLocalScale;
        }
    }
    public void AnimatorEvent()
    {
        ///因为状态不是挂载在人物身上所以，需要用到动画帧事件是，需要挂载脚本中转
        animatorClipCb?.Invoke();
    }
    public virtual void Injured(InjuredData injuredData)
    {
        ResourceSvc.Single.LoadOrCreate<GameObject>(UIItemPath.DamageTextitem).GetComponent<DamageText>().Init(injuredData.harm, injuredPos);
    }
    public void SetAirPhysicsMaterial2D()
    {
        if (rig.sharedMaterial != AirMaterial)
        {
            rig.sharedMaterial = AirMaterial;
        }
    }
    public void SetGroundPhysicsMaterial2D()
    {
        if (rig.sharedMaterial != GroundMaterial)
        {
            rig.sharedMaterial = AirMaterial;
        }
    }
}

