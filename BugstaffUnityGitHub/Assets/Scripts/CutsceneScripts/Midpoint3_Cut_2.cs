using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class Midpoint3_Cut_2 : MonoBehaviour
{
    public GameObject door;
    public PlayerController player;
    public AudioSource musicPlayer;
    public AudioClip ambientClip;
    int mode;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    public TextboxScript.TextBlock[] textToSend3;
    public TextboxScript.TextBlock[] textToSend4;
    TextboxScript tbs;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < textToSend4.Length; i++){
            TextboxScript.TextBlock tb = textToSend4[i];
            string str = tb.text.Replace("[b]", MissionManager.GetCharmTutorial());
            textToSend4[i] = new TextboxScript.TextBlock{speakerName = tb.speakerName, text = str, emphasis = tb.emphasis};
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tbs == null){
            tbs = FindObjectOfType<TextboxScript>();
        }
        if (mode == 0){
            if (player.transform.position.x > transform.position.x){
                player.GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<SpriteRenderer>().flipX = false;
            } else {
                player.GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            musicPlayer.clip = ambientClip;
            musicPlayer.pitch = 1f;
            musicPlayer.Play();
            mode = 1;
        } else if (mode == 1){
            if (tbs.IsEmpty()){
                mode = 2;
            }
        } else if (mode == 2){
            player.controlEnabled = false;
            player.SetMove(new Vector2(transform.position.x-player.transform.position.x, 0f).normalized);
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 0.5f){
                player.SetMove(Vector2.zero);
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 3;
            }
        } else if (mode == 3){
            if (tbs.IsEmpty()){
                GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                foreach (TextboxScript.TextBlock textBlock in textToSend3){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 4;
            }
        } else if (mode == 4){
            if (tbs.IsEmpty()){
                BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
                for (int i = 0; i < bss.Length; i++){
                    Destroy(bss[i].gameObject);
                }
                GetComponent<SpriteRenderer>().enabled = false;
                foreach (TextboxScript.TextBlock textBlock in textToSend4){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 5;
            }
        } else if (mode == 5){
            if (tbs.IsEmpty()){
                MissionManager.UpdatePlayerGadgets();
                door.SetActive(true);
                mode = 6;
            }
        }
    }
}
