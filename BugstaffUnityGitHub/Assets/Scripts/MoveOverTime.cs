using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTime : MonoBehaviour
{
    public Vector2 targetPosition;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //return;
        Vector3 posDir = new Vector3(targetPosition.x, targetPosition.y, 0f);
        Vector3 dir = (posDir-transform.position).normalized;
        float dist = (posDir-transform.position).magnitude;
        if (dist > 0.05f){
            transform.position += dir*moveSpeed*Time.deltaTime;
        }
    }
}
