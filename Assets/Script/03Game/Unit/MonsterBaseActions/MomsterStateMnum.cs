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

        Attack=1001,
        Attack1=1002,
        Attack2=1003,
        Attack3=1004,
        Attack4=1005,
    }

}