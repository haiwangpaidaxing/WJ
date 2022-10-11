using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    protected Text text;
    public float validTime = 1;
    float timer;
    public float speed = 1f;
   
    public void Init(float harm, Transform pos, float validTime = 1)
    {
        text =GetComponentInChildren<Text>();
        transform.position = pos.position;
        transform.position += Random.insideUnitSphere;
        text.text = harm.ToString();
        this.validTime = validTime;
    }

    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= validTime)
        {
            Destroy(gameObject);
        }
    }
}
