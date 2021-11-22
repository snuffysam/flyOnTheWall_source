using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWhenBugged : MonoBehaviour
{
    public TextboxScript.TextBlock[] textToSend;
    bool alreadySent;
    TextboxScript tbs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tbs == null){
            tbs = FindObjectOfType<TextboxScript>();
        }
        if (!alreadySent && GetComponentInChildren<BugShotScript>() != null){
            alreadySent = true;
            foreach (TextboxScript.TextBlock textBlock in textToSend){
                tbs.AddTextBlock(textBlock);
            }
        } else if (alreadySent && !tbs.IsEmpty()){
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (GetComponent<PatrolScript>() != null){
                GetComponent<PatrolScript>().enabled = false;
            }
        } else if (alreadySent && tbs.IsEmpty()){
            if (GetComponent<PatrolScript>() != null){
                GetComponent<PatrolScript>().enabled = true;
            }
        }
    }
}
