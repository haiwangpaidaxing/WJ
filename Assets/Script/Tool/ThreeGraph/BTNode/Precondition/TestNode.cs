using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNodeEditor;
using WMTreePrecondition;
using WMTreeGraph;

[CustomNodeEditor(typeof(WMTreeGraph.BaseBTNode))]
public class TestNode : NodeEditor
{
    public override void OnHeaderGUI()
    {
        GUI.color = Color.white;
        BaseBTNode node = target as BaseBTNode;
        if (node.isRuning)
        {
            GUI.color =Color.blue;
        }
        string title = target.name;
        GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        GUI.color = Color.white;
    }
    public override void OnBodyGUI()
    {
		GUILayout.BeginHorizontal();
		GUILayout.EndHorizontal();
		base.OnBodyGUI();

		
	}
}
