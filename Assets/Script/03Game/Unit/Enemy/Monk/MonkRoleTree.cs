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
        injuredNode.AddChild(injuredState);

        BTPrioritySelector patoplNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Patrol), "Patrol");
        MonsterTackingState patrolState = new MonsterTackingState(MonsterStateEnum.Patrol, "Run");
        patoplNode.AddChild(patrolState);

        BTPrioritySelector monkNANode = new BTPrioritySelector(new MonkRoleStateCheck(MonsterStateEnum.Attack1), "NA");
        MonkNAState monkNAState = new MonkNAState(MonsterStateEnum.Attack1, "NA");
        monkNANode.AddChild(monkNAState);

        BTPrioritySelector monkTANode = new BTPrioritySelector(new MonkRoleStateCheck(MonsterStateEnum.Attack1), "NA");
        MonkTAState monkTAState = new MonkTAState(MonsterStateEnum.Attack2, "TA");
        monkNANode.AddChild(monkTAState);

        BTPrioritySelector monkSkillNode = new BTPrioritySelector(new MonkRoleStateCheck(MonsterStateEnum.Attack1), "NA");
        MonkNAState monkSkillState = new MonkNAState(MonsterStateEnum.Attack1, "Skill");
        monkNANode.AddChild(monkSkillNode);

        BTPrioritySelector attackNode = new BTPrioritySelector(new MonkRoleStateCheck(MonsterStateEnum.Attack), "MonsterAttack");
        attackNode.AddChild(monkNANode);
        attackNode.AddChild(monkTANode);
        attackNode.AddChild(monkSkillNode);

        root.AddChild(dieNode);
        root.AddChild(injuredNode);
        root.AddChild(attackNode);
        root.Init(database);
        root.AddChild(patoplNode);
        root.Init(database);
    }
}
