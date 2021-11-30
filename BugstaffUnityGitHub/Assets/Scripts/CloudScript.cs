using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0f, Random.Range(-4.5f, -5f), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5.2f){
            transform.position = new Vector3(Random.Range(-7f, 7f), Random.Range(9f,10f), 0f);
            transform.position += Vector3.up*10f;
        }
    }
}
