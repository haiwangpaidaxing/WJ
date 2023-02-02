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
    [Header("受伤位置")]
    public Transform injuredPos;
    //PhysicsMaterial2D AirMaterial;
    //PhysicsMaterial2D GroundMaterial;
    public Action<InjuredData> injuredCB;
    public MonsterInjuredState injuredState;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public BaseSkill currentSkill;
    [HideInInspector]
    public RoleAttribute roleAttribute;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    public Action DieCB;

    public virtual void Init()
    {

        //AirMaterial = ResourceSvc.Single.Load<PhysicsMaterial2D>("AirMaterial");
        //GroundMaterial = ResourceSvc.Single.Load<PhysicsMaterial2D>("GroundMaterial");

        injuredPos = transform.Find("InjuredPos");
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        this.roleAttribute = GetComponent<RoleAttribute>();
        rig = GetComponent<Rigidbody2D>();
        skillManager = gameObject.AddComponent<SkillManager>();
        animator = GetComponentInChildren<Animator>();
        injuredState = new MonsterInjuredState(this);
        roleAttribute.hpValueCB += DieCheck;
        ghostList = new List<GhostData>();
        roleDir = 1;

    }

    protected virtual void FixedUpdate()
    {
        if (currentSkill != null)
        {
            //Debug.Log(currentSkill.skillData.currentCD);
            currentSkill.OnFixedUpdate();
        }
    }

    protected virtual void Update()
    {
        if (currentSkill != null)
        {
            currentSkill.OnUpdate();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OpenGhost();
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
        //创建受伤的文本
        ResourceSvc.Single.LoadOrCreate<GameObject>(UIItemPath.DamageTextitem).GetComponent<DamageText>().Init(injuredData.harm, injuredPos);
    }
    //public void SetAirPhysicsMaterial2D()
    //{
    //    rig.sharedMaterial = AirMaterial;
    //}
    //public void SetGroundPhysicsMaterial2D()
    //{
    //    rig.sharedMaterial = AirMaterial;
    //}
    bool die;
    public virtual void Die()
    {
        die = true;
        DieCB?.Invoke();

    }
    private void DieCheck(int value)
    {
        if (value <= 0 && !die)
        {
            Die();
        }
    }
    float currentGhostTime;
    float createSinleGhostTime;
    List<GhostData> ghostList;
    [SerializeField]
    protected CreateGhostData createGhostData;
    /// <summary>
    /// 开启
    /// </summary>
    public void OpenGhost()
    {
        currentGhostTime = 0;
        createSinleGhostTime = 0;
        StartCoroutine(Ghost());
    }
    /// <summary>
    /// 残影携程
    /// </summary>
    /// <returns></returns>
    IEnumerator Ghost()
    {
        while (true)
        {
            currentGhostTime += 0.02f;//整个残影效果的时间
            createSinleGhostTime += 0.02f;

            if (currentGhostTime >= createGhostData.ghostTime)//整个残影效果的时间
            {
                if (ghostList.Count == 0)
                {
                    yield break;
                }
            }
            if (ghostList.Count > 0)
            {
                for (int i = ghostList.Count - 1; i >= 0; i--)
                {
                    if (ghostList[i].Update(0.02f))
                    {
                        ghostList.RemoveAt(i);
                    }
                }
            }

            if (currentGhostTime <= createGhostData.ghostTime)
            {
                if (createSinleGhostTime >= createGhostData.singleCreateGohstInterval)//创建单个残影
                {
                    GhostData ghostData = new GhostData();
                    ghostData.Open(createGhostData.singleGhostTime, spriteRenderer.sprite, transform.position, transform.localScale, createGhostData.color);
                    ghostList.Add(ghostData);
                    createSinleGhostTime = 0;
                }
            }

            yield return new WaitForSecondsRealtime(0.02f);
        }
    }

}
[System.Serializable]
public struct CreateGhostData
{
    [Header("残影效果持续时间")]
    public float ghostTime;
    [Header("创建单个残影的间隔")]
    public float singleCreateGohstInterval;
    [Header("单个残剑的存活时间")]
    public float singleGhostTime;
    [Header("初始颜色")]
    public Color color;
}
public class GhostData
{
    public float currentTime;
    public Sprite sprite;
    GameObject ghost;
    float setTime;
    public void Open(float setTime, Sprite sprite, Vector3 startPos, Vector3 trLocalScale, Color color)
    {
        this.setTime = setTime;
        this.sprite = sprite;
        ghost = ResourceSvc.Single.LoadOrCreate<GameObject>(HeroPath.Ghost);
        SpriteRenderer spriteRenderer = ghost.GetComponent<SpriteRenderer>();
        ghost.GetComponent<SpriteRenderer>().sortingOrder = -1;
        ghost.transform.position = startPos;
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;
        ghost.transform.localScale = trLocalScale;

    }
    //
    public void Open(float setTime, Sprite sprite, Vector3 startPos, Vector3 trLocalScale)
    {
        this.setTime = setTime;
        this.sprite = sprite;
        ghost = ResourceSvc.Single.LoadOrCreate<GameObject>(HeroPath.Ghost);
        ghost.transform.position = startPos;
        ghost.GetComponent<SpriteRenderer>().sprite = sprite;
        ghost.GetComponent<SpriteRenderer>().sortingOrder = -1;
        ghost.transform.localScale = trLocalScale;
    }

    public bool Update(float value)
    {
        currentTime += value;
        if (currentTime >= setTime)
        {
            Clear();
            return true;
        }
        return false;
    }

    public void Clear()
    {
        GameObject.Destroy(ghost);
    }
}
