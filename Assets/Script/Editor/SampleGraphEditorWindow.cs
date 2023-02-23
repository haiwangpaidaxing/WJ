using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class SampleGraphEditorWindow : EditorWindow
{

    [MenuItem("Window/Open SampleGraphView")]
    public static void Open()
    {
        GetWindow<SampleGraphEditorWindow>("SampleGraphView");
    }
    void OnEnable()
    {
        var graphView = new SampleGraphView()
        {
            style = { flexGrow = 1 }
        };
        rootVisualElement.Add(graphView);
    }
}


public class SampleGraphView : GraphView
{
    public RootNode root;
    public SampleGraphView() : base()
    {
        root = new RootNode();
        AddElement(root);
        //按照父级的宽高全屏填充
        this.StretchToParentSize();
        //滚轮缩放
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        //选中Node移动功能
        this.AddManipulator(new SelectionDragger());
        //多个node框选功能
        this.AddManipulator(new SelectionDragger());
        // 目前，在 GraphView 的构造函数中创建了一个，因此您可以从右键单击菜单添加它。
        nodeCreationRequest += context =>
        {
            AddElement(new SampleNode());
        };
    }
    public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        foreach (var port in ports.ToList())
        {
            if (startAnchor.node == port.node ||
                startAnchor.direction == port.direction ||
                startAnchor.portType != port.portType)
            {
                continue;
            }

            compatiblePorts.Add(port);
        }
        return compatiblePorts;
    }
}


public class SampleNode : Node
{
    public SampleNode()
    {
        title = "Sample";
        //节点与其他节点结合使用 首先，将 InputPort 和 OutputPort 添加到节点。
        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
        inputContainer.Add(inputPort);

        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
        outputContainer.Add(outputPort);
    }

}

public class LogNode : SampleNode
{
    public LogNode() : base()
    {
        title = "Log";

        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(string));
        inputContainer.Add(inputPort);
    }
}



public class StringNode : SampleNode
{
    private TextField textField;
    public string Text { get { return textField.value; } }

    public StringNode() : base()
    {
        title = "String";

        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(string));
        outputContainer.Add(outputPort);

        textField = new TextField();
        mainContainer.Add(textField);
    }
}



public class RootNode : SampleNode
{
    public RootNode() : base()
    {
        title = "Root";

        capabilities -= Capabilities.Deletable;
        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
        outputPort.portName = "Out";
        outputContainer.Add(outputPort);
    }
}