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
        AudioHandlerScript.PlayClipAtPoint("MenuSelect", "MenuSelect", 0.8f, transform.position);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
        GetComponent<Rigidbody2D>().isKinematic = true;
        Destroy(GetComponent<Collider2D>());
        transform.SetParent(collision.collider.transform);
        transform.position = collision.contacts[0].point;
        //transform.localPosition = transform.localPosition*0.33f;
        //transform.parent = collision.collider.transform;
        //transform.localScale = 1f/transform.parent.localScale;
    }
}
