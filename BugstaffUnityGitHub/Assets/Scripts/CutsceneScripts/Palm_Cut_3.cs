using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class Palm_Cut_3 : MonoBehaviour
{
    public GameObject playerTripped;
    public GameObject chandelier;
    public PlayerController player;
    public PalmBossScript bossScript;
    public AudioSource musicPlayer;
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
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            AudioHandlerScript.PlayClipAtPoint("EnemyFootstepsBugvision8", "EnemyFootstepsBugvision8", 0.8f, player.transform.position);
            playerTripped.SetActive(true);
            player.GetComponent<SpriteRenderer>().enabled = false;
            player.controlEnabled = false;
            bossScript.FinishBattle();
            mode = 2;
            player.transform.position = playerTripped.transform.position;
        } else if (mode == 2){
            player.controlEnabled = false;
            player.transform.position = playerTripped.transform.position;
            if (bossScript.IsAtEnd() && tbs.IsEmpty()){
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 3;
            }
        } else if (mode == 3){
            player.controlEnabled = false;
            player.transform.position = playerTripped.transform.position;
            if (tbs.IsEmpty()){
                mode = 4;
            }
        } else if (mode == 4){
            player.controlEnabled = false;
            player.transform.position = playerTripped.transform.position;
            if (chandelier.transform.position.y > 20f){
                chandelier.transform.position += Vector3.up*Time.deltaTime*-12f;
            } else {
                bossScript.StopMoving();
                foreach (TextboxScript.TextBlock textBlock in textToSend3){
                    tbs.AddTextBlock(textBlock);
                }
                AudioHandlerScript.PlayClipAtPoint("DoorClosing", "DoorClosing", 5f, player.transform.position);
                chandelier.GetComponent<Collider2D>().enabled = true;
                mode = 5;
                musicPlayer.Stop();
            }
        } else if (mode == 5){
            player.controlEnabled = false;
            player.transform.position = playerTripped.transform.position;
            if (tbs.IsEmpty()){
                playerTripped.SetActive(false);
                player.GetComponent<SpriteRenderer>().enabled = true;
                player.controlEnabled = true;
                mode = 6;
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
