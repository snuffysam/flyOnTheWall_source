using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHardcoreEnable : MonoBehaviour
{
    public bool enableWhenHardcore;
    // Start is called before the first frame update
    void Start()
    {
        if (PasscodeHandler.hardcore == enableWhenHardcore){
            GetComponent<TextTriggerEnter>().enabled = true;
        } else {
            GetComponent<TextTriggerEnter>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
