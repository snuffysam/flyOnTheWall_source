using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palace5_Cut : MonoBehaviour
{
    public GameObject guardMessenger;
    public GameObject guardPosted;
    public GameObject empress;
    public GameObject secondTrigger;
    public TextboxScript.TextBlock[] textToSend1;
    int mode;
    TextboxScript tbs;
    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        tbs = FindObjectOfType<TextboxScript>();
        guardMessenger.GetComponent<Rigidbody2D>().gravityScale = 0f;
        guardMessenger.GetComponent<Collider2D>().enabled = false;
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
            guardMessenger.GetComponent<Rigidbody2D>().velocity = new Vector3(3f, 0f, 0f);
            mode = 2;
        } else if (mode == 2){
            guardMessenger.GetComponent<Rigidbody2D>().velocity = new Vector3(3f, 0f, 0f);
            if (guardMessenger.transform.position.x > empress.transform.position.x-1.5f){
                guardMessenger.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
            }
            if (tbs.IsEmpty()){
                mode = 3;
            }
        } else if (mode == 3){
            guardMessenger.GetComponent<SpriteRenderer>().flipX = true;
            guardMessenger.GetComponent<Rigidbody2D>().velocity = new Vector3(-3f, 0f, 0f);
            empress.GetComponent<Rigidbody2D>().velocity = new Vector3(-2f, 0f, 0f);
            empress.GetComponent<Animator>().SetFloat("velocityX", 1f);
            guardPosted.GetComponent<Rigidbody2D>().velocity = new Vector3(-4f, 0f, 0f);
            guardMessenger.GetComponent<Rigidbody2D>().gravityScale = 0f;
            guardMessenger.GetComponent<Collider2D>().enabled = false;
            guardPosted.GetComponent<Rigidbody2D>().gravityScale = 0f;
            guardPosted.GetComponent<Collider2D>().enabled = false;
            if (guardPosted.transform.position.x < transform.position.x){
                secondTrigger.SetActive(true);
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
