using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivateMoveScript : ButtonActivateScript
{
    public Vector2 displacement;
    public float displacementTime;
    public bool repeatable;
    int mode;
    Vector3 finalPos;
    Vector3 startPos;
    Vector3 displacement3;
    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        displacement3 = displacement;
        finalPos = transform.position + displacement3;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 1){
            transform.position += displacement3*(Time.deltaTime/displacementTime);
            //Debug.Log("magnitude: " + (transform.position-finalPos).magnitude);
            if ((transform.position-startPos).magnitude > (displacement3).magnitude){
                mode = 2;
            }
        }
        if (repeatable && mode == 2){
            transform.position = finalPos;
            mode = 0;
        }
        //Debug.Log("mode: " + mode);
    }

    public override void ExecuteButton(){
        base.ExecuteButton();
        if (mode == 0){
            mode = 1;
        }
    }
}
