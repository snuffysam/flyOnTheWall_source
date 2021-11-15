using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palace3_Cutscene : MonoBehaviour
{
    public GameObject guard;
    public GameObject arva;
    public GameObject empress;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    public TextboxScript.TextBlock[] textToSend3;
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
            empress.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, 0f, 0f);
            empress.GetComponent<Animator>().SetFloat("velocityX", 1f);
            arva.GetComponent<Rigidbody2D>().velocity = new Vector3(-1.05f, 0f, 0f);
            arva.GetComponent<Animator>().SetFloat("velocityX", 1f);
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            mode = 2;
        } else if (mode == 2){
            if (empress.transform.position.x < 3f){
                empress.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                empress.GetComponent<Animator>().SetFloat("velocityX", 0f);
            }
            if (arva.transform.position.x < 2f){
                arva.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                arva.GetComponent<Animator>().SetFloat("velocityX", 0f);
                arva.GetComponent<SpriteRenderer>().flipX = false;
                if (tbs.IsEmpty()){
                    foreach (TextboxScript.TextBlock textBlock in textToSend2){
                        tbs.AddTextBlock(textBlock);
                    }
                    guard.GetComponent<Rigidbody2D>().velocity = new Vector3(-4f, 0f, 0f);
                    mode = 3;
                }
            }
        } else if (mode == 3){
            if (guard.transform.position.x < 2.5f){
                arva.GetComponent<Rigidbody2D>().velocity = new Vector3(-4f, 0f, 0f);
                arva.GetComponent<Animator>().SetFloat("velocityX", 1f);
            }
            if (guard.transform.position.x < 2.5f && tbs.IsEmpty()){
                foreach (TextboxScript.TextBlock textBlock in textToSend3){
                    tbs.AddTextBlock(textBlock);
                }
                empress.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, 0f, 0f);
                empress.GetComponent<Animator>().SetFloat("velocityX", 1f);
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
