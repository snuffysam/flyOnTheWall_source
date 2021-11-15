using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugShotScript : MonoBehaviour
{
    public GameObject listenerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate<GameObject>(listenerPrefab).GetComponent<FollowScript>().go = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
        GetComponent<Rigidbody2D>().isKinematic = true;
        Destroy(GetComponent<Collider2D>());
        transform.SetParent(collision.collider.transform);
        //transform.parent = collision.collider.transform;
        //transform.localScale = 1f/transform.parent.localScale;
    }
}
