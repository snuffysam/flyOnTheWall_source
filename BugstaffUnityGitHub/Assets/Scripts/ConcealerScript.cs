using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcealerScript : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesHitTriggers = false;
        Collider2D col = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), transform.localScale.x, LayerMask.GetMask("ListenerLayer"));
        Physics2D.queriesHitTriggers = true;
        if (col == null){
            Color tmp = GetComponent<SpriteRenderer>().color;
            tmp.a += Time.deltaTime*2f;
            if (tmp.a > 1f){
                tmp.a = 1f;
            }
            GetComponent<SpriteRenderer>().color = tmp;
            //spriteRenderer.enabled = true;
        } else {
            Color tmp = GetComponent<SpriteRenderer>().color;
            tmp.a -= Time.deltaTime*2f;
            if (tmp.a < 0f){
                tmp.a = 0f;
            }
            GetComponent<SpriteRenderer>().color = tmp;
            //spriteRenderer.enabled = false;
        }
    }
}
