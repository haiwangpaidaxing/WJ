using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSys<T> : MonoSingle<T> where T : MonoSingle<T>
{
    protected ResourceSvc resourceSvc;
    public override void Init()
    {
        resourceSvc = ResourceSvc.Single;
    }
    public virtual void Enter()
    {

    }
    public virtual void Quit() { }
}
