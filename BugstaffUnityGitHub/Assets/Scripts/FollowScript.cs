using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        if (go.GetComponent<BugShotScript>() != null){
            transform.localScale = Vector3.one*3f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (go == null){
            Destroy(this.gameObject);
            return;
        }
        transform.position = go.transform.position;
        GetComponent<Rigidbody2D>().WakeUp();
    }
}
