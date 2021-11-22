using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateVelocityScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ang = Vector3.SignedAngle(Vector3.right, GetComponent<Rigidbody2D>().velocity, new Vector3(0f, 0f, 1f));
        transform.eulerAngles = new Vector3(0f, 0f, ang);
    }
}
