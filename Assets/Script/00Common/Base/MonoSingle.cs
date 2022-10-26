using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingle<T> : MonoBehaviour where T : MonoBehaviour
{
    static T single;
    public static T Single
    {
        get
        {
            if (single == null)
            {
                single = GameObject.FindObjectOfType<T>();
                if (single == null)
                {
                    GameObject gameObject = new GameObject(typeof(T).Name);
                    single = gameObject.AddComponent<T>();
                }
            }
            return single;
        }
    }

    public virtual void Init()
    { }
}
