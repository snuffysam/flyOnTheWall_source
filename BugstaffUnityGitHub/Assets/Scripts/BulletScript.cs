using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public bool spreadshot;
    // Start is called before the first frame update
    void Start()
    {
        if (spreadshot){
            speed *= Random.Range(0.8f,1.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float velMult = 1f;
        if (GetComponent<SpriteRenderer>().flipX){
            velMult = -1f;
        }
        float vert = 0f;
        if (spreadshot){
            vert = Random.Range(-speed, speed);
        }
        GetComponent<Rigidbody2D>().velocity = new Vector3(speed*velMult, vert, 0f);
    }
}
