using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMTreeGraph;
using XNode;

namespace WMTreePrecondition
{
    public class BTInputNode : BTPreconditionNode
    {
        [Header("当前方向")]
        public Vector2 dir;
        public bool value;
        public InputState inputState;
        // Use this for initialization
        protected override void Init()
        {
            base.Init();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }

        public override bool DoEvaluate()
        {
            dir = database.InputDir;
            switch (inputState)
            {
                case InputState.Left:
                    if (dir.x == 1)
                    {
                        value = true;
                        return true;
                    }
                    break;
                case InputState.Right:
                    if (dir.x == -1)
                    {
                        value = true;
                        return true;
                    }
                    break;
                case InputState.Up:
                    if (dir.y == 1)
                    {
                        value = true;
                        return true;
                    }
                    break;
                case InputState.Down:
                    if (dir.y == -1)
                    {
                        value = true;
                        return true;
                    }
                    break;
                case InputState.LeftOrRight:
                    if (dir.x != 0)
                    {
                        value = true;
                        return true;
                    }
                    break;
                case InputState.Null:
                    if (dir.x == 0 || dir.y == 0)
                    {
                        value = true;
                        return true;
                    }
                    break;
            }
            value = false;
            return false;
        }

        public enum InputState
        {
            Null, Left = -1, Right = 1, Up = 11, Down = -11, LeftOrRight,
        }
    }
}