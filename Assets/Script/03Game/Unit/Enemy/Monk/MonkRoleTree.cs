using WMBT;
using WMState;

public class MonkRoleTree : BTTree
{
    public BTPrioritySelector attackNode;
    public BTPrioritySelector monkNANode;
    public BTPrioritySelector monkTANode;
    public BTPrioritySelector monkSkillNode;
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


        monkNANode = new BTPrioritySelector(new MonkRoleStateCheck(MonsterStateEnum.Attack1), "NA");
        MonkNAState monkNAState = new MonkNAState(MonsterStateEnum.Attack1, "NA");
        monkNANode.AddChild(monkNAState);

        monkTANode = new BTPrioritySelector(new MonkRoleStateCheck(MonsterStateEnum.Attack1), "TA");
        MonkTAState monkTAState = new MonkTAState(MonsterStateEnum.Attack2, "TA");
        monkNANode.AddChild(monkTAState);

        monkSkillNode = new BTPrioritySelector(new MonkRoleStateCheck(MonsterStateEnum.Attack1), "Skill");
        MonkNAState monkSkillState = new MonkNAState(MonsterStateEnum.Attack1, "Skill");

        monkNANode.AddChild(monkSkillState);
        attackNode = new BTPrioritySelector(new MonkRoleStateCheck(MonsterStateEnum.Attack), "MonsterAttack");

        root.AddChild(dieNode);
        root.AddChild(injuredNode);
        root.AddChild(attackNode);
        root.Init(database);
        root.AddChild(patoplNode);
        root.Init(database);
    }
    public void AddNA()
    {
        attackNode.AddChild(monkNANode);
    }
    public void AddTA()
    {
        attackNode.AddChild(monkTANode);
    }
    public void AddSkill()
    {
        attackNode.AddChild(monkSkillNode);
    }
}
