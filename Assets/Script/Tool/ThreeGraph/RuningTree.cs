using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;
using WMTreeGraph;
public class RuningTree : MonoBehaviour
{
    public BTRootNode rootNode;
    void Start()
    {
        rootNode.OnInit(GetComponent<Database>());
    }

    // Update is called once per frame
    void Update()
    {
        if (rootNode.Evaluate())
        {
            rootNode.Update();
        }
    }
}
