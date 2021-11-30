using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using UnityEngine.SceneManagement;

public class Intro4_Cut : MonoBehaviour
{
    public GameObject guard1;
    public GameObject guard2;
    public PlayerController player;
    public GameObject president;
    public GameObject oruma;
    public float switchDelay;
    public float switchDelay2;
    public float switchDelay3;
    public string nextScene;
    public AudioSource musicPlayer;
    public AudioClip insideClip;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    public TextboxScript.TextBlock[] textToSend3;
    public TextboxScript.TextBlock[] textToSend4;
    public TextboxScript.TextBlock[] textToSend5;
    int mode;
    TextboxScript tbs;
    float delay;
    FollowScript[] listeners;
    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        tbs = FindObjectOfType<TextboxScript>();
        if (PasscodeHandler.hardcore){
            textToSend3[14] = new TextboxScript.TextBlock{speakerName = "?????", text = "A blind woman in a bikini washes up on the beach and you arrest her?!?! Give her the cane back, you blockhead! And get a towel!", emphasis = false};
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tbs == null){
            tbs = FindObjectOfType<TextboxScript>();
        }
        if (mode == 1){
            player.controlEnabled = false;
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
            for (int i = 0; i < bss.Length; i++){
                Destroy(bss[i].gameObject);
            }
            mode = 2;
        } else if (mode == 2){
            player.controlEnabled = false;
            if (tbs.IsEmpty()){
                guard1.SetActive(true);
                guard2.SetActive(true);
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
                for (int i = 0; i < bss.Length; i++){
                    Destroy(bss[i].gameObject);
                }
                mode = 3;
            }
        } else if (mode == 3){
            player.controlEnabled = false;
            if (tbs.IsEmpty()){
                musicPlayer.Stop();
                if (listeners == null){
                    listeners = FindObjectsOfType<FollowScript>();
                    foreach (FollowScript fs in listeners){
                        fs.gameObject.SetActive(false);
                    }
                }
                delay += Time.deltaTime;
                if (delay > switchDelay){
                    foreach (TextboxScript.TextBlock textBlock in textToSend3){
                        tbs.AddTextBlock(textBlock);
                    }
                    delay = 0f;
                    mode = 4;
                    BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
                    for (int i = 0; i < bss.Length; i++){
                        Destroy(bss[i].gameObject);
                    }
                    musicPlayer.clip = insideClip;
                    musicPlayer.Play();
                }
            }
        } else if (mode == 4){
            player.controlEnabled = false;
            president.SetActive(true);
            guard1.SetActive(false);
            guard2.SetActive(false);
            if (tbs.IsEmpty()){
                delay += Time.deltaTime;
                if (delay > switchDelay2){
                    foreach (FollowScript fs in listeners){
                        fs.gameObject.SetActive(true);
                    }

                    foreach (TextboxScript.TextBlock textBlock in textToSend4){
                        tbs.AddTextBlock(textBlock);
                    }
                    delay = 0f;
                    mode = 5;
                    BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
                    for (int i = 0; i < bss.Length; i++){
                        Destroy(bss[i].gameObject);
                    }
                }
            }
        } else if (mode == 5){
            player.controlEnabled = false;
            if (tbs.IsEmpty()){
                oruma.SetActive(true);
                oruma.transform.position += Vector3.right*5f*Time.deltaTime;
                oruma.GetComponent<Animator>().SetFloat("velocityX", 1f);
                if (oruma.transform.position.x > -0.75f){
                    foreach (TextboxScript.TextBlock textBlock in textToSend5){
                        tbs.AddTextBlock(textBlock);
                    }
                    player.GetComponent<SpriteRenderer>().flipX = true;
                    oruma.GetComponent<Animator>().SetFloat("velocityX", 0f);
                    mode = 6;
                    BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
                    for (int i = 0; i < bss.Length; i++){
                        Destroy(bss[i].gameObject);
                    }
                }
            }
        } else if (mode == 6){
            player.controlEnabled = false;
            if (tbs.IsEmpty()){
                oruma.transform.position += Vector3.right*-1.5f*Time.deltaTime;
                oruma.GetComponent<SpriteRenderer>().flipX = true;
                oruma.GetComponent<Animator>().SetFloat("velocityX", 1f);
                player.GetComponent<PlayerController>().SetMove(new Vector2(-1f,0f));

                foreach (FollowScript fs in listeners){
                    fs.enabled = false;
                }

                if (delay > switchDelay3/2f){
                    foreach (FollowScript fs in listeners){
                        fs.gameObject.SetActive(false);
                    }
                }

                delay += Time.deltaTime;
                if (delay > switchDelay3){
                    SceneManager.LoadScene(nextScene);
                }
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
