using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using System.Linq;

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

        rootVisualElement.Add(new Button(graphView.Execute) { text = "Execute" });
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

        //������ͬ�ڵ㹦��
        var searchWindowProvider = new SampleSearchWindowProvider();
        searchWindowProvider.Initialize(this);

        nodeCreationRequest += context =>
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
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
    public void Execute()
    {
        var rootEdge = root.outputPort.connections.FirstOrDefault();//�ҵ�����ڵ�
        if (rootEdge == null) return;

        var currentNode = rootEdge.input.node as ProcessNode;

        while (true)
        {
            currentNode.Execute();
            var edge = currentNode.outputPort.connections.FirstOrDefault();
            if (edge == null) break;
            currentNode = edge.input.node as ProcessNode;
        }
    }


}

public class SampleSearchWindowProvider : ScriptableObject, ISearchWindowProvider
{
    private SampleGraphView graphView;

    public void Initialize(SampleGraphView graphView)
    {
        this.graphView = graphView;
    }

    List<SearchTreeEntry> ISearchWindowProvider.CreateSearchTree(SearchWindowContext context)
    {
        var entries = new List<SearchTreeEntry>();
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));

        foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass && !type.IsAbstract && (type.IsSubclassOf(typeof(SampleNode)))
                    && type != typeof(RootNode))
                {
                    entries.Add(new SearchTreeEntry(new GUIContent(type.Name)) { level = 1, userData = type });
                }
            }
        }

        return entries;
    }

    bool ISearchWindowProvider.OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
    {
        var type = searchTreeEntry.userData as System.Type;
        var node = Activator.CreateInstance(type) as SampleNode;
        graphView.AddElement(node);
        return true;
    }
}
public abstract class SampleNode : Node
{


}
public abstract class ProcessNode : SampleNode
{
    public Port outputPort;
    public ProcessNode()
    {
        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
        inputPort.portName = "Input";
        inputContainer.Add(inputPort);

        outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
        outputPort.portName = "Out";
        outputContainer.Add(outputPort);
    }

    public abstract void Execute();
}
public class BaseState : ProcessNode
{
    public BaseState():base()
    {
        title = "BaseState";
    }
    public override void Execute()
    {
    }
}
public class LogNode : ProcessNode
{
    private Port inputString;
    public LogNode() : base()
    {
        title = "Log";
        inputString = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(BaseState));
        inputContainer.Add(inputString);//����˿�
    }

    public override void Execute()
    {
        // Edge edge = inputString.connections.FirstOrDefault();
        //   Debug.Log("aaa");
        //var node = edge.output.node as StringNode;
        //if (node == null) return;
        //Debug.Log(node.Text);
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
    public Port outputPort;
    public RootNode() : base()
    {
        title = "Root";

        capabilities -= Capabilities.Deletable;//���ڵ㱣֤�ڵ㲻�ᱻɾ��

        outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
        outputPort.portName = "Out";
        outputContainer.Add(outputPort);
    }


}
