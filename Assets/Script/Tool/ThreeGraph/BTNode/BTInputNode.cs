using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace WMTreeGraph
{
    public class BTInputNode : BTPreconditionNode
    {
        public Vector2 dir;
        public int target;
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

        protected override bool DoEvaluate()
        {
            dir.x = Input.GetAxisRaw("Horizontal");
            switch (inputState)
            {
                case InputState.Left:
                    if (dir.x == 1)
                    {
                        return true;
                    }
                    break;
                case InputState.Right:
                    if (dir.x == -1)
                    {
                        return true;
                    }
                    break;
                case InputState.Up:
                    if (dir.y == 1)
                    {
                        return true;
                    }
                    break;
                case InputState.Down:
                    if (dir.y == -1)
                    {
                        return true;
                    }
                    break;
                case InputState.LeftOrRight:
                    if (dir.x != 0)
                    {
                        return true;
                    }
                    break;
                case InputState.Null:
                    if (dir.x == 0 || dir.y == 0)
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }
           
            return false;
        }

        public enum InputState
        {
            Null, Left = -1, Right = 1, Up = 11, Down = -11, LeftOrRight,
        }
    }
}