using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Platformer.Mechanics;

public class Casino5_Cut_2 : MonoBehaviour
{
    public GameObject cricketCutscene;
    public GameObject mantisCutscene;
    public GameObject cricketBoss;
    public GameObject mantisBoss;
    public PlayerController player;
    public GameObject missionCompleteText;
    public AudioSource musicPlayer;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    public TextboxScript.TextBlock[] textToSend3;
    int mode;
    TextboxScript tbs;
    float delay;
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
        if (mode == 0){
            musicPlayer.Stop();
            
            cricketCutscene.transform.position = cricketBoss.transform.position;
            cricketCutscene.GetComponent<SpriteRenderer>().flipX = cricketBoss.GetComponent<SpriteRenderer>().flipX;
            cricketBoss.SetActive(false);
            cricketCutscene.SetActive(true);

            mantisCutscene.transform.position = mantisBoss.transform.position;
            mantisCutscene.GetComponent<SpriteRenderer>().flipX = mantisBoss.GetComponent<SpriteRenderer>().flipX;
            mantisBoss.SetActive(false);
            mantisCutscene.SetActive(true);

            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            mode = 1;
        } else if (mode == 1){
            if (tbs.IsEmpty()){
                cricketCutscene.GetComponent<Animator>().SetTrigger("dead");
                mantisCutscene.GetComponent<Animator>().SetTrigger("dead");
                mode = 2;
            }
        } else if (mode == 2){
            player.controlEnabled = false;
            player.SetMove(new Vector2(-0.75f, 0f));
            player.GetComponent<SpriteRenderer>().flipX = true;
            if (player.transform.position.x < -9f){
                player.SetMove(Vector2.zero);
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 3;
            }
        } else if (mode == 3){
            if (tbs.IsEmpty()){
                player.controlEnabled = false;
                player.SetMove(new Vector2(-0.75f, 0f));
                FollowScript[] fs = FindObjectsOfType<FollowScript>();
                foreach (FollowScript listener in fs){
                    listener.gameObject.SetActive(false);
                }
                foreach (TextboxScript.TextBlock textBlock in textToSend3){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 4;
            }
        } else if (mode == 4){
            if (tbs.IsEmpty()){
                missionCompleteText.SetActive(true);
                MissionManager.CompleteMission();
                mode = 5;
            }
        } else if (mode == 5){
            player.controlEnabled = false;
            delay += Time.deltaTime;
            if (delay > 5f){
                SceneManager.LoadScene("MissionSelect");
            }
        }
    }
}
