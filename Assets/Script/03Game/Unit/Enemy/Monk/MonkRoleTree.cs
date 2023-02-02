using WMBT;
using WMState;

public class MonkRoleTree : BTTree
{
    protected override void InitBehavior()
    {
        root = new BTPrioritySelector(null, "Root");

        BTPrioritySelector dieNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Die), "Die");
        dieNode.AddChild(new RoleDieState(RoleAnimNmae.Die));

        BTPrioritySelector injuredNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Injured), "Injured");
        MosterInjuredState injuredState = new MosterInjuredState(MonsterStateEnum.Injured, "Injured");



        BTPrioritySelector patoplNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Patrol), "Patrol");
        MonsterTackingState patrolState = new MonsterTackingState(MonsterStateEnum.Patrol, "Run");

        BTPrioritySelector attackNode = new BTPrioritySelector(new MonkRoleStateCheck(MonsterStateEnum.Attack), "MonsterAttack");

        root.AddChild(dieNode);
        dieNode.AddChild(injuredState);
        injuredNode.AddChild(injuredState);
        patoplNode.AddChild(patrolState);

        root.AddChild(patoplNode);
    }
}
