using WMBT;
using WMState;

public class RedRoleTree : BTTree
{
    protected override void InitBehavior()
    {
        root = new BTPrioritySelector(null, "Root");

        BTPrioritySelector injuredNode = new BTPrioritySelector(new RedRoleStateCheck(MonsterStateEnum.Injured), "Injured");

        BTPrioritySelector dieNode = new BTPrioritySelector(new RedRoleStateCheck(MonsterStateEnum.Die), "Die");
        dieNode.AddChild(new RoleDieState(RoleAnimNmae.Die));

        MosterInjuredState injuredState = new MosterInjuredState(MonsterStateEnum.Injured, "Injured");

        BTPrioritySelector patoplNode = new BTPrioritySelector(new RedRoleStateCheck(MonsterStateEnum.Patrol), "Tracking");
        RedMonsterTrackingState patrolState = new RedMonsterTrackingState(MonsterStateEnum.Patrol, "Run");

        BTPrioritySelector attackNode = new BTPrioritySelector(new RedRoleStateCheck(MonsterStateEnum.Attack), "MonsterAttack");

        BTPrioritySelector raNode = new BTPrioritySelector(new RedRoleStateCheck(MonsterStateEnum.Attack3), "RA1");

        RedRoleRAState redRoleRAState = new RedRoleRAState(MonsterStateEnum.Attack3, "RA1");
        raNode.AddChild(redRoleRAState);

        BTPrioritySelector taNode = new BTPrioritySelector(new RedRoleStateCheck(MonsterStateEnum.Attack2), "TA1");
        RedRoleTAState redRoleTAState = new RedRoleTAState(MonsterStateEnum.Attack2, "TA1");
        taNode.AddChild(redRoleTAState);

        BTPrioritySelector naNode = new BTPrioritySelector(new RedRoleStateCheck(MonsterStateEnum.Attack1), "NA");
        RedNAState attackState = new RedNAState(MonsterStateEnum.Attack1, "NA");
        naNode.AddChild(attackState);

        attackNode.AddChild(raNode);//远程攻击节点
        attackNode.AddChild(taNode);//重击节点
        attackNode.AddChild(naNode);//普通攻击节点

        injuredNode.AddChild(injuredState);
        patoplNode.AddChild(patrolState);

        root.AddChild(dieNode);
        root.AddChild(injuredNode);
        root.AddChild(attackNode);
        root.AddChild(patoplNode);
        root.Init(database);

    }
}
