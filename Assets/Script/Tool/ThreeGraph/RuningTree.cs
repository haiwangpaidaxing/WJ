using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;
using WMTree;
using WMTreeGraph;
public class RuningTree : MonoBehaviour
{
    public BTRootNode rootNode;
    void Start()
    {
        rootNode.OnInit(GetComponent<TreeDatabase>());
    }

    void FixedUpdate()
    {
        if (rootNode.Evaluate())
        {
            rootNode.Update();
        }
    }
}
