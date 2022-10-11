using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillOperate
{
    Null,
    /// <summary>
    /// 重击
    /// </summary>
    Thump,
    /// <summary>
    /// 普通攻击
    /// </summary>
    NormalAttack,
    /// <summary>
    /// 闪避
    /// </summary>
    Dodge,
    //重击, 轻击, 闪避,

}

public class InputController : MonoSingle<InputController>
{
    [SerializeField, Header("输入方向")]
    private Vector2 inputDir;
    public Vector2 InputDir { get { return inputDir; } }
    private Vector2 oldInput;
    public System.Action<Vector2> inputCB;
    public System.Action<int> operaterCB;
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    operaterCB?.Invoke(1001);
        //}
        if (Input.GetKeyDown(KeyCode.J))
        {
            operaterCB?.Invoke(1001);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            operaterCB?.Invoke(1002);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            operaterCB?.Invoke(1003);
        }
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");
        if (inputDir != oldInput)
        {
            oldInput = inputDir;
            inputCB?.Invoke(oldInput);
        }
       
    }
}
