using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWhenBugged : MonoBehaviour
{
    public TextboxScript.TextBlock[] textToSend;
    bool alreadySent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!alreadySent && GetComponentInChildren<BugShotScript>() != null){
            alreadySent = true;
            TextboxScript tbs = FindObjectOfType<TextboxScript>();
            foreach (TextboxScript.TextBlock textBlock in textToSend){
                tbs.AddTextBlock(textBlock);
            }
        }
    }
}
