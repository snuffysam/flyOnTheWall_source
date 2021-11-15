using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class HeartScript : MonoBehaviour
{
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        if (Health.HasHeart(this.gameObject.name)){
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = transform.position + (Vector3.up*Mathf.Sin(timer)*0.005f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null){
            Health.AddHeart(this.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
