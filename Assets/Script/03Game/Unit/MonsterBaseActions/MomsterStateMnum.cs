using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WMState
{
    public enum MonsterStateEnum
    {
        Patrol, 
        Tracking, 
        Idle,
        Injured,
        Die,

        Attack=1000,
        Attack1=1001,
        Attack2=1002,
        Attack3=1003,
        Attack4=1004,
    }

}