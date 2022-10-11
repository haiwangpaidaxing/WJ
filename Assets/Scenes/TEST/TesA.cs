using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TesA : MonoBehaviour
{
    public EnemyFinder enemyFinder;
    public bool isOpen;

    private void Update()
    {
        if (isOpen)
        {
            enemyFinder.FindCloseRange();
            isOpen = false;
        }
    }
}
