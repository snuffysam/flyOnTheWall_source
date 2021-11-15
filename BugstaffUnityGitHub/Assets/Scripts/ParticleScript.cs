using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public float time;
    float scaleMult;
    float timer;
    float rotSpeed;
    float currentRot;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        currentRot = Random.Range(-180f, 180f);
        rotSpeed = Random.Range(-180f, 180f);
        transform.eulerAngles = new Vector3(0f, 0f, currentRot);
        scaleMult = time*Random.Range(0.75f, 1.3f)/2f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < time/2f){
            transform.localScale = Vector3.one*timer/scaleMult;
        } else {
            transform.localScale = Vector3.one*(time-timer)/scaleMult;
            if (timer > time){
                Destroy(this.gameObject);
            }
        }
        currentRot += rotSpeed*Time.deltaTime;
        transform.eulerAngles = new Vector3(0f, 0f, currentRot);
    }
}
