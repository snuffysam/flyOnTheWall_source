using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectScript : MonoBehaviour
{
    float timer = 0f;
    float midTimer = 0.02f;
    int midMultiplier = 25;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < midTimer){
            transform.localScale = Vector3.one*timer/midTimer;
        } else if (timer > midTimer*(midMultiplier-1)) {
            transform.localScale = Vector3.one*(midTimer*midMultiplier-timer)/midTimer;
            if (timer > midTimer*midMultiplier){
                Destroy(this.gameObject);
            }
        } else {
            transform.localScale = Vector3.one;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DealDamageScript>() != null){
            other.gameObject.GetComponent<DealDamageScript>().playerOwned = !other.gameObject.GetComponent<DealDamageScript>().playerOwned;
            if (other.gameObject.GetComponent<Rigidbody2D>() != null){
                Vector3 tmp = other.gameObject.GetComponent<Rigidbody2D>().velocity;
                tmp.x = tmp.x*-1f;
                other.gameObject.GetComponent<Rigidbody2D>().velocity = tmp;
            }
            if (other.gameObject.GetComponent<SpriteRenderer>() != null){
                other.gameObject.GetComponent<SpriteRenderer>().flipX = !other.gameObject.GetComponent<SpriteRenderer>().flipX;
            }
        }
    }
}
