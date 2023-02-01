using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDeathBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInjured injured = collision.GetComponent<IInjured>();
        if (injured != null)
        {
            InjuredData injuredData = new InjuredData();
            injuredData.harm = 10000;
            injured.Injured(injuredData);
        }

    }
}

