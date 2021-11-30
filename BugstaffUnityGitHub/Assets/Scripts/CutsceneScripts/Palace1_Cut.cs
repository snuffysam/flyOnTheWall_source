using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palace1_Cut : MonoBehaviour
{
    public GameObject guard;
    public GameObject limo;
    public GameObject empress;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
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
            limo.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, 0f, 0f);
            if (limo.transform.position.x < -3.97f){
                limo.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                foreach (TextboxScript.TextBlock textBlock in textToSend1){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 2;
                AudioHandlerScript.PlayClipAtPoint("DoorClosing", "DoorClosing", 1f, limo.transform.position);
            }
        } else if (mode == 2){
            empress.transform.position = new Vector3(-5f, -0.6f, 0f);
            if (tbs.IsEmpty()){
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                empress.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, 0f, 0f);
                empress.GetComponent<Animator>().SetFloat("velocityX", 1f);
                mode = 3;
            }
        } else if (mode == 3){
            if (empress.transform.position.x < -6.5f){
                empress.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                empress.GetComponent<Animator>().SetFloat("velocityX", 0f);
            }
            if (tbs.IsEmpty()){
                empress.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, 0f, 0f);
                empress.GetComponent<Animator>().SetFloat("velocityX", 1f);
                guard.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, 0f, 0f);
                guard.GetComponent<Rigidbody2D>().gravityScale = 0f;
                guard.GetComponent<Collider2D>().enabled = false;
                guard.GetComponent<SpriteRenderer>().flipX = true;
                AudioHandlerScript.PlayClipAtPoint("DoorOpening", "DoorOpening", 1f, limo.transform.position);
                mode = 4;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (mode == 0){
            mode = 1;
        }
    }
}
