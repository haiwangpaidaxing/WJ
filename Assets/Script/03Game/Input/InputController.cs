using UnityEngine;

public class InputController : MonoSingle<InputController>
{
    [SerializeField, Header("输入方向")]
    private Vector2 inputDir;
    public Vector2 InputDir { get { return inputDir; } }
    private Vector2 oldInput;
    public System.Action<Vector2> inputCB;
    public System.Action<int> operaterCB;
    public bool LockDir { get; set; }

    public bool isEnabled { get; private set; }
    [SerializeField]
    bool isRocker;

    public void Close()
    {
        inputCB = null;
        operaterCB = null;
    }
    private void Start()
    {
      
        Rocker.Single.dirEvent += (dir) =>
        {
            if (dir != oldInput)
            {
                isRocker = true;
                oldInput = dir;
                if (LockDir)
                {
                    inputCB(Vector2.zero);
                    return;
                }
                inputCB?.Invoke(oldInput);
                if (oldInput == Vector2.zero)
                {
                    isRocker = false;
                }
            }
        };
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    operaterCB?.Invoke(1001);
        //}
        if (Input.GetKeyDown(KeyCode.J))
        {
            operaterCB?.Invoke(1000);
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
        if (inputDir != oldInput && !isRocker)
        {
            oldInput = inputDir;
            if (LockDir)
            {
                inputCB(Vector2.zero);
                return;
            }
            inputCB?.Invoke(oldInput);
        }
    }


}
