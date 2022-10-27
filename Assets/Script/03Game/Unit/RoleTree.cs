using UnityEngine;
using WMState;
using WUBT;
public class RoleTree : BTTree<HeroDatabase>
{
    public override void Init()
    {
        base.Init();
        InitBasedBehavior();
    }
    protected virtual void InitBasedBehavior()
    {
        root = new BTPrioritySelector(null, "root");
        //选择节点地面与空中
        BTPrioritySelector groundSelector = new BTPrioritySelector(new RoleStateCheck(RoleStateCheck.CheckType.Ground), "Ground");
        //技能

        //  BTPrioritySelector skillPS = new BTPrioritySelector(new SkillCheck(), "SelectSkill");
        //  BTPrioritySelector thump = new BTPrioritySelector(, "普通攻击");//轻击节点

        //站立
        BTParallel grIdleParallel = new BTParallel(new RoleStateCheck(RoleStateCheck.CheckType.Idle), BTParallel.ParallelFunction.Or, "Idle");
        RoleIdleState roleIdle = new RoleIdleState(RoleAnimNmae.Idle);
        grIdleParallel.AddChild(roleIdle);
        //跑步执行节点
        BTParallel grRunParallel = new BTParallel(new RoleStateCheck(RoleStateCheck.CheckType.Run), BTParallel.ParallelFunction.Or, "Run");
        RoleRunStaet roleRun = new RoleRunStaet(RoleAnimNmae.Run);//执行节点
        grRunParallel.AddChild(roleRun);
        //跳跃执行节点
        BTParallel grJumpParallel = new BTParallel(new RoleStateCheck(RoleStateCheck.CheckType.GroundJump), BTParallel.ParallelFunction.Or, "GroundJump");
        RoleGroundJump roleJump = new RoleGroundJump(RoleAnimNmae.Jump);
        grJumpParallel.AddChild(roleJump);
        //空中节点的创建
        BTPrioritySelector airSelect = new BTPrioritySelector(new RoleStateCheck(RoleStateCheck.CheckType.Air), "Air");
        //空中跳跃
        BTPrioritySelector airJumpParallel = new BTPrioritySelector(new RoleStateCheck(RoleStateCheck.CheckType.AirJump), "AirJump");
        RoleAirJumpState roleAirJumpState = new RoleAirJumpState(RoleAnimNmae.Jump);
        airJumpParallel.AddChild(roleAirJumpState);
        //滑墙
        BTPrioritySelector wallSliderSelector = new BTPrioritySelector(new RoleStateCheck(RoleStateCheck.CheckType.WallSlider), "WallSlider");
        RoleWallSlider roleWallSlider = new RoleWallSlider(RoleAnimNmae.WallSlide);
        wallSliderSelector.AddChild(roleWallSlider);
        //下落的创建
        BTPrioritySelector airFallSelector = new BTPrioritySelector(new RoleStateCheck(RoleStateCheck.CheckType.Fall), "Fall");
        RoleFall roleFall = new RoleFall(RoleAnimNmae.Fall);
        airFallSelector.AddChild(roleFall);

        BTPrioritySelector injuredNode = new BTPrioritySelector(new RoleStateCheck( RoleStateCheck.CheckType.Injured),"Injured");
        RoleInjuredState roleInjuredState = new RoleInjuredState("Injured");


        injuredNode.AddChild(roleInjuredState);
        //添加空中子节点
        airSelect.AddChild(wallSliderSelector);
        airSelect.AddChild(airJumpParallel);
        airSelect.AddChild(airFallSelector);

        //地面节点的添加
        groundSelector.AddChild(grRunParallel);//跑步
        groundSelector.AddChild(grJumpParallel);//条约
        groundSelector.AddChild(grIdleParallel);//站立

        //根节点的添加
        root.AddChild(injuredNode);
        root.AddChild(groundSelector);
        root.AddChild(airSelect);
        root.Init(database);
    }



}
