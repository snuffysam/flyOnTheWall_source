using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using UnityEngine.SceneManagement;

public class Finale_Cut : MonoBehaviour
{
    public GameObject redAdmiral;
    public PlayerController player;
    public GameObject environment;
    public GameObject soldier1alive;
    public GameObject soldier2alive;
    public GameObject soldier1hurt;
    public GameObject soldier2hurt;
    public GameObject herculesStanding;
    public GameObject herculesGround;
    public GameObject orumaFloat;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    public TextboxScript.TextBlock[] textToSend3;
    public TextboxScript.TextBlock[] textToSend4;
    public TextboxScript.TextBlock[] textToSend5;
    public TextboxScript.TextBlock[] textToSend6;
    public TextboxScript.TextBlock[] textToSend7;
    public TextboxScript.TextBlock[] textToSend8;
    int mode;
    TextboxScript tbs;
    FollowScript[] listeners;
    float delay;
    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        tbs = FindObjectOfType<TextboxScript>();
        MissionManager.beatPalmMission = true;
        textToSend8[textToSend8.Length-1] = new TextboxScript.TextBlock{speakerName = "", text = "[ Passcode : " + PasscodeHandler.GetCurrentPasscode() + " ]", emphasis = false};
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
            Health.currentHP = Health.maxHP;
            MissionManager.beatPalmMission = true;
            //Debug.Log("beat palm mission: " + Mi)
            mode = 2;
        } else if (mode == 2){
            if (tbs.IsEmpty()){
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                redAdmiral.GetComponent<SpriteRenderer>().flipX = true;
                mode = 3;
            }
        } else if (mode == 3){
            if (listeners == null){
                listeners = FindObjectsOfType<FollowScript>();
            }
            player.controlEnabled = false;
            if (tbs.IsEmpty()){
                player.SetMove(new Vector2(5f, 0f));
                redAdmiral.GetComponent<Rigidbody2D>().velocity = new Vector3(5f, 0f, 0f);
                redAdmiral.GetComponent<SpriteRenderer>().flipX = false;
                redAdmiral.GetComponent<Animator>().SetFloat("deltaX", 1f);
                foreach (FollowScript fs in listeners){
                    fs.gameObject.SetActive(false);
                }
                player.controlEnabled = false;
                delay = 0f;
                mode = 4;
            }
        } else if (mode == 4){
            delay += Time.deltaTime;
            player.controlEnabled = false;
            BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
            for (int i = 0; i < bss.Length; i++){
                Destroy(bss[i].gameObject);
            }
            if (delay > 0.5f){
                player.SetMove(Vector2.zero);
            }
            if (delay > 2f){
                foreach (FollowScript fs in listeners){
                    fs.gameObject.SetActive(true);
                }
                player.SetMove(Vector2.zero);
                redAdmiral.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                redAdmiral.GetComponent<SpriteRenderer>().flipX = false;
                redAdmiral.GetComponent<Animator>().SetFloat("deltaX", 0f);
                player.transform.position = new Vector3(0f, -0.51f, 0f);
                redAdmiral.transform.position = new Vector3(0.85f, -0.61f, 0f);
                environment.SetActive(true);
                foreach (TextboxScript.TextBlock textBlock in textToSend3){
                    tbs.AddTextBlock(textBlock);
                }
                delay = 0f;
                mode = 5;
            }
        } else if (mode == 5){
            player.controlEnabled = false;
            if (tbs.IsEmpty()){
                soldier1alive.SetActive(true);
                soldier2alive.SetActive(true);
                delay += Time.deltaTime;
                if (delay > 0.75f){
                    BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
                    for (int i = 0; i < bss.Length; i++){
                        Destroy(bss[i].gameObject);
                    }
                    delay = 0f;
                    foreach (TextboxScript.TextBlock textBlock in textToSend4){
                        tbs.AddTextBlock(textBlock);
                    }
                    mode = 6;
                }
            }
        } else if (mode == 6){
            player.controlEnabled = false;
            if (tbs.IsEmpty()){
                if (soldier1alive.activeInHierarchy){
                    AudioHandlerScript.PlayHitAtPoint(soldier1alive.transform.position, 4);
                    AudioHandlerScript.PlayClipAtPoint("EnemyFootsteps6", "EnemyFootstepsBugvision6", 1f, soldier1alive.transform.position);
                }
                soldier1alive.SetActive(false);
                soldier1hurt.SetActive(true);
                delay += Time.deltaTime;
                if (delay > 0.75f){
                    BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
                    for (int i = 0; i < bss.Length; i++){
                        Destroy(bss[i].gameObject);
                    }
                    delay = 0f;
                    soldier1hurt.transform.eulerAngles = new Vector3(0f, 0f, -90f);
                    soldier1hurt.transform.position = new Vector3(soldier1hurt.transform.position.x, -1.06f, soldier1hurt.transform.position.z);
                    foreach (TextboxScript.TextBlock textBlock in textToSend5){
                        tbs.AddTextBlock(textBlock);
                    }
                    AudioHandlerScript.PlayDeathAtPoint(soldier1alive.transform.position, 4);
                    //AudioHandlerScript.PlayClipAtPoint("EnemyFootstepsBugvision8", "EnemyFootstepsBugvision8", 0.8f, transform.position);
                    mode = 7;
                }
            }
        } else if (mode == 7){
            player.controlEnabled = false;
            if (tbs.IsEmpty()){
                if (soldier2alive.activeInHierarchy){
                    AudioHandlerScript.PlayHitAtPoint(soldier2alive.transform.position, 4);
                    AudioHandlerScript.PlayClipAtPoint("EnemyFootsteps6", "EnemyFootstepsBugvision6", 1f, soldier1alive.transform.position);
                }
                soldier2alive.SetActive(false);
                soldier2hurt.SetActive(true);
                delay += Time.deltaTime;
                if (delay > 0.75f){
                    BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
                    for (int i = 0; i < bss.Length; i++){
                        Destroy(bss[i].gameObject);
                    }
                    delay = 0f;
                    soldier2hurt.transform.eulerAngles = new Vector3(0f, 0f, -90f);
                    soldier2hurt.transform.position = new Vector3(soldier2hurt.transform.position.x, -1.06f, soldier2hurt.transform.position.z);
                    AudioHandlerScript.PlayDeathAtPoint(soldier2alive.transform.position, 4);
                    //AudioHandlerScript.PlayClipAtPoint("EnemyFootstepsBugvision8", "EnemyFootstepsBugvision8", 0.8f, transform.position);
                    mode = 8;
                }
            }
        } else if (mode == 8){
            player.controlEnabled = false;
            delay += Time.deltaTime;
            if (delay > 1.5f){
                BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
                for (int i = 0; i < bss.Length; i++){
                    Destroy(bss[i].gameObject);
                }
                delay = 0f;
                herculesStanding.SetActive(true);
                AudioHandlerScript.PlayHitAtPoint(herculesStanding.transform.position, 4);
                AudioHandlerScript.PlayClipAtPoint("EnemyFootsteps6", "EnemyFootstepsBugvision6", 1f, soldier1alive.transform.position);
                player.transform.position += Vector3.right*-0.3f;
                redAdmiral.transform.position = player.transform.position + Vector3.right*0.4f;
                foreach (TextboxScript.TextBlock textBlock in textToSend6){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 9;
            }
        } else if (mode == 9){
            player.controlEnabled = false;
            if (tbs.IsEmpty()){
                BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
                for (int i = 0; i < bss.Length; i++){
                    Destroy(bss[i].gameObject);
                }
                redAdmiral.GetComponent<Rigidbody2D>().velocity = new Vector3(5f, 0f, 0f);
                redAdmiral.GetComponent<SpriteRenderer>().flipX = false;
                redAdmiral.GetComponent<Animator>().SetFloat("deltaX", 1f);
                foreach (TextboxScript.TextBlock textBlock in textToSend7){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 10;
            }
        } else if (mode == 10){
            player.controlEnabled = false;
            redAdmiral.GetComponent<Rigidbody2D>().velocity = new Vector3(5f, 0f, 0f);
            if (tbs.IsEmpty()){
                if (herculesStanding.activeInHierarchy){
                    AudioHandlerScript.PlayDeathAtPoint(herculesStanding.transform.position, 4);
                    //AudioHandlerScript.PlayClipAtPoint("EnemyFootstepsBugvision8", "EnemyFootstepsBugvision8", 0.8f, transform.position);
                }
                herculesStanding.SetActive(false);
                herculesGround.SetActive(true);
                delay += Time.deltaTime;
                if (delay > 2.5f){
                    BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
                    for (int i = 0; i < bss.Length; i++){
                        Destroy(bss[i].gameObject);
                    }
                    delay = 0f;
                    orumaFloat.SetActive(true);
                    foreach (TextboxScript.TextBlock textBlock in textToSend8){
                        tbs.AddTextBlock(textBlock);
                    }
                    mode = 11;
                }
            }
        } else if (mode == 11){
            player.controlEnabled = false;
            if (tbs.IsEmpty()){
                SceneManager.LoadScene("Finale2");
                mode = 12;
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
