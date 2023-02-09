using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;

public class MonkTAState : MonsterAttackState
{
    public MonkTAState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
    {
    }
}
