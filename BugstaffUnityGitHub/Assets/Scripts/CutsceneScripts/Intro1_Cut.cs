using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class Intro1_Cut : MonoBehaviour
{
    public GameObject alenaWakeUp;
    public GameObject alena;
    public float startDelay;
    public float textDelay;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    int mode;
    TextboxScript tbs;
    float delay;
    FollowScript listener;
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
        if (listener == null){
            listener = FindObjectOfType<FollowScript>();
        }
        if (mode == 0){
            alenaWakeUp.GetComponent<Animator>().speed = 0f;
            alena.GetComponent<PlayerController>().controlEnabled = false;
            if (listener != null){
                listener.gameObject.SetActive(false);
            }
        } else if (mode == 1){
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            mode = 2;
        } else if (mode == 2){
            alenaWakeUp.GetComponent<Animator>().speed = 0f;
            alena.GetComponent<PlayerController>().controlEnabled = false;
            if (tbs.IsEmpty()){
                alenaWakeUp.GetComponent<Animator>().speed = 0.55f;
                mode = 3;
            }
        } else if (mode == 3){
            alena.GetComponent<PlayerController>().controlEnabled = false;
            if (alenaWakeUp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f){
                alenaWakeUp.GetComponent<SpriteRenderer>().enabled = false;
                alena.GetComponent<SpriteRenderer>().enabled = true;
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 5;
            }
        } else if (mode == 4){
            alenaWakeUp.GetComponent<Animator>().speed = 0f;
            alena.GetComponent<PlayerController>().controlEnabled = false;
            delay += Time.deltaTime;
            if (delay > startDelay){
                listener.gameObject.SetActive(true);
            }
            if (delay > textDelay){
                mode = 1;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (mode == 0){
            mode = 4;
        }
    }
}
