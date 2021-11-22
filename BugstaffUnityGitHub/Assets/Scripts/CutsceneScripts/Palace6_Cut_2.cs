using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Palace6_Cut_2 : MonoBehaviour
{
    public GameObject empress;
    public GameObject advisor1;
    public GameObject advisor2;
    public GameObject advisor3;
    public float[] xPosStop;
    public GameObject missionCompleteText;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    float delay;
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
            empress.GetComponent<Rigidbody2D>().velocity = new Vector3(-1.3f, 0f, 0f);
            empress.GetComponent<Animator>().SetFloat("velocityX", 1f);
            advisor1.GetComponent<Rigidbody2D>().velocity = new Vector3(-1.1f, 0f, 0f);
            advisor1.GetComponent<Animator>().SetFloat("velocityX", 1f);
            advisor2.GetComponent<Rigidbody2D>().velocity = new Vector3(-1.4f, 0f, 0f);
            advisor2.GetComponent<Animator>().SetFloat("velocityX", 1f);
            advisor3.GetComponent<Rigidbody2D>().velocity = new Vector3(-1.5f, 0f, 0f);
            advisor3.GetComponent<Animator>().SetFloat("velocityX", 1f);
            mode = 2;
        } else if (mode == 2){
            if (advisor1.transform.position.x < xPosStop[0]){
                advisor1.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                advisor1.GetComponent<Animator>().SetFloat("velocityX", 0f);
                advisor1.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (empress.transform.position.x < xPosStop[1]){
                empress.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                empress.GetComponent<Animator>().SetFloat("velocityX", 0f);
                empress.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (advisor2.transform.position.x < xPosStop[2]){
                advisor2.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                advisor2.GetComponent<Animator>().SetFloat("velocityX", 0f);
            }
            if (advisor3.transform.position.x < xPosStop[3]){
                advisor3.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                advisor3.GetComponent<Animator>().SetFloat("velocityX", 0f);
            }
            if (tbs.IsEmpty()){
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 3;
            }
        } else if (mode == 3){
            empress.GetComponent<Rigidbody2D>().velocity = new Vector3(1.5f, 0f, 0f);
            empress.GetComponent<Animator>().SetFloat("velocityX", 1f);
            empress.GetComponent<SpriteRenderer>().flipX = false;

            advisor1.GetComponent<Rigidbody2D>().velocity = new Vector3(1.1f, 0f, 0f);
            advisor1.GetComponent<Animator>().SetFloat("velocityX", 1f);
            advisor1.GetComponent<SpriteRenderer>().flipX = false;

            advisor2.GetComponent<Rigidbody2D>().velocity = new Vector3(1.4f, 0f, 0f);
            advisor2.GetComponent<Animator>().SetFloat("velocityX", 1f);
            advisor2.GetComponent<SpriteRenderer>().flipX = false;

            advisor3.GetComponent<Rigidbody2D>().velocity = new Vector3(1.3f, 0f, 0f);
            advisor3.GetComponent<Animator>().SetFloat("velocityX", 1f);
            advisor3.GetComponent<SpriteRenderer>().flipX = false;

            if (tbs.IsEmpty()){
                missionCompleteText.SetActive(true);
                MissionManager.CompleteMission();
                mode = 4;
            }
        } else if (mode == 4){
            delay += Time.deltaTime;
            if (delay > 5f){
                SceneManager.LoadScene("MissionSelect");
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
