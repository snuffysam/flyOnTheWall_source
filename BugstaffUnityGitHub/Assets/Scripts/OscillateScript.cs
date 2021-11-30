using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillateScript : MonoBehaviour
{
    public float period;
    public float minX;
    public float maxX;
    public bool vertical;
    float timer;
    float midPoint;
    float dist;
    // Start is called before the first frame update
    void Start()
    {
        midPoint = (minX+maxX)/2f;
        dist = (maxX-minX)/2f;   
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        midPoint = (minX+maxX)/2f;
        dist = (maxX-minX)/2f;
        if (vertical){
            transform.position = new Vector3(transform.position.x, midPoint+(Mathf.Sin(timer/period)*dist), transform.position.z);
        } else {
            transform.position = new Vector3(midPoint+(Mathf.Sin(timer/period)*dist), transform.position.y, transform.position.z);
        }
    }
}
