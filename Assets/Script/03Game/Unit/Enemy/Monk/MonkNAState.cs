using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;

public class MonkNAState : MonsterAttackState
{
    public MonkNAState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
    {
    }
}
