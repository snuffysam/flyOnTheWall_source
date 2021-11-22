using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutScript : MonoBehaviour
{
    public bool fadeOut = true;
    public float fadeSpeed = 0.33f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;

        if (fadeOut){
            tmp.a -= Time.deltaTime*fadeSpeed;
            if (tmp.a < 0f){
                tmp.a = 0f;
            }
        } else {
            tmp.a += Time.deltaTime*fadeSpeed;
            if (tmp.a > 1f){
                tmp.a = 1f;
            }
        }

        GetComponent<SpriteRenderer>().color = tmp;
    }
}
