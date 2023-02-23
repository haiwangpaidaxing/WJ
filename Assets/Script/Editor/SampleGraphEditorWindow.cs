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
        //���ո����Ŀ��ȫ�����
        this.StretchToParentSize();
        //��������
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        //ѡ��Node�ƶ�����
        this.AddManipulator(new SelectionDragger());
        //���node��ѡ����
        this.AddManipulator(new SelectionDragger());
        // Ŀǰ���� GraphView �Ĺ��캯���д�����һ������������Դ��Ҽ������˵��������
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
        //�ڵ��������ڵ���ʹ�� ���ȣ��� InputPort �� OutputPort ��ӵ��ڵ㡣
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