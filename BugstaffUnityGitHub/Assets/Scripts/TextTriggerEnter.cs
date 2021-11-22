using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class TextTriggerEnter : MonoBehaviour
{
    public float delay;
    public TextboxScript.TextBlock[] textToSend;
    bool alreadyActivated;
    float delayTimer;
    bool alreadySent;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (alreadyActivated && !alreadySent){
            delayTimer += Time.deltaTime;
            if (playerController != null){
                playerController.controlEnabled = false;
            }
            if (delayTimer > delay){
            if (playerController != null){
                playerController.controlEnabled = true;
            }
                if (!alreadySent){
                    alreadySent = true;
                    TextboxScript tbs = FindObjectOfType<TextboxScript>();
                    foreach (TextboxScript.TextBlock textBlock in textToSend){
                        tbs.AddTextBlock(textBlock);
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!alreadyActivated){
            alreadyActivated = true;
            if (other.GetComponent<PlayerController>() != null){
                playerController = other.GetComponent<PlayerController>();
            }
        }
    }
}
