using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airship4_Cut : MonoBehaviour
{
    public PatrolScript soldierStationed;
    public PatrolScript soldierEnter;
    public TextboxScript.TextBlock[] textToSend1;
    int mode;
    TextboxScript tbs;
    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        tbs = FindObjectOfType<TextboxScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tbs == null){
            tbs = FindObjectOfType<TextboxScript>();
        }
        if (mode == 1){
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            mode = 2;
        } else if (mode == 2){
            if (soldierEnter.transform.position.x < soldierStationed.transform.position.x-1f){
                soldierEnter.GetComponent<Rigidbody2D>().velocity = new Vector3(2.5f, 0f, 0f);
            } else {
                soldierEnter.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            if (tbs.IsEmpty()){
                soldierStationed.GetComponent<Collider2D>().enabled = false;
                soldierStationed.GetComponent<Rigidbody2D>().gravityScale = 0f;
                mode = 3;
            }
        } else if (mode == 3){
            soldierEnter.GetComponent<Rigidbody2D>().velocity = new Vector3(-2.5f, 0f, 0f);
            soldierEnter.GetComponent<SpriteRenderer>().flipX = true;
            soldierStationed.GetComponent<Rigidbody2D>().velocity = new Vector3(-2.5f, 0f, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (mode == 0){
            mode = 1;
        }
    }
}
