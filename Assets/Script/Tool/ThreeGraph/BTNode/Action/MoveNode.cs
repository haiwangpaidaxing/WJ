using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMTreeGraph;

namespace WMTreeAction
{
    public class MoveNode : BTActionNode
    {
        public Vector2 speed;
        public enum MoveType
        {
            Not,UpdateX, UpdateY,EnterX,EnterY
        }
        protected override void Enter()
        {
            base.Enter();
            switch (moveType)
            {
                case MoveType.EnterX:
                    database.unitController.MoveX(database.InputDir.x, speed.x);
                    break;
                case MoveType.EnterY:
                    database.unitController.MoveY(database.InputDir.y, speed.y);
                    break;
            }
        }
        public MoveType moveType;
        protected override BTResult Execute()
        {
            switch (moveType)
            {
                case MoveType.UpdateX:
                    database.unitController.MoveX(database.InputDir.x, speed.x);
                    break;
                case MoveType.UpdateY:
                    database.unitController.MoveY(database.InputDir.y, speed.y);
                    break;
                case MoveType.Not:

                    break;
            }
            return base.Execute();
        }
    }
}