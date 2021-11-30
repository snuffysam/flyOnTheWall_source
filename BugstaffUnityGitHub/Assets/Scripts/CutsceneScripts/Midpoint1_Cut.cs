using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class Midpoint1_Cut : MonoBehaviour
{
    public GameObject player;
    public GameObject redAdmiral;
    public GameObject redAdmiralBoss;
    public AudioSource musicPlayer;
    public AudioClip bossClip;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    int mode;
    TextboxScript tbs;
    float stopX;
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
            player.GetComponent<PlayerController>().controlEnabled = false;
            player.GetComponent<SpriteRenderer>().flipX = true;
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            stopX = player.transform.position.x-2f;
            mode = 2;
        } else if (mode == 2){
            player.GetComponent<PlayerController>().controlEnabled = false;
            if (tbs.IsEmpty()){
                player.GetComponent<PlayerController>().SetMove(new Vector2(-1f,0f));
                if (player.transform.position.x < stopX){
                    player.GetComponent<PlayerController>().SetMove(new Vector2(0f,0f));
                    redAdmiral.transform.position = player.transform.position + new Vector3(-1f, 0f, 0f);
                    foreach (TextboxScript.TextBlock textBlock in textToSend2){
                        tbs.AddTextBlock(textBlock);
                    }
                    mode = 3;
                }
            }
        } else if (mode == 3){
            player.GetComponent<PlayerController>().controlEnabled = false;
            if (tbs.IsEmpty()){
                mode = 4;
            }
        } else if (mode == 4){
            player.GetComponent<PlayerController>().controlEnabled = false;
            redAdmiral.transform.position += new Vector3(-6f*Time.deltaTime, 0f, 0f);
            if (redAdmiral.transform.position.x < player.transform.position.x-4f){
                player.GetComponent<PlayerController>().controlEnabled = true;
                redAdmiral.SetActive(false);
                redAdmiralBoss.SetActive(true);
                musicPlayer.clip = bossClip;
                musicPlayer.pitch = AudioHandlerScript.bossMult;
                musicPlayer.Play();
                mode = 5;
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
