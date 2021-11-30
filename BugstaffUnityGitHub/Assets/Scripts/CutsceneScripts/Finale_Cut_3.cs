using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using UnityEngine.SceneManagement;

public class Finale_Cut_3 : MonoBehaviour
{
    public GameObject oruma;
    public SpriteRenderer playerSpr;
    public SpriteRenderer vanessaSpr;
    public Rigidbody2D soldier1;
    public Rigidbody2D soldier2;
    public Rigidbody2D biitle;
    public Rigidbody2D empress;
    public GameObject environmentRoom;
    public GameObject environmentBoat;
    public AudioSource musicPlayer;
    public AudioClip boatClip;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    public TextboxScript.TextBlock[] textToSend3;
    public TextboxScript.TextBlock[] textToSend4;
    public TextboxScript.TextBlock[] textToSend5;
    public TextboxScript.TextBlock[] textToSend6;
    public TextboxScript.TextBlock[] textToSend7;
    int mode;
    TextboxScript tbs;
    FollowScript[] listeners;
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
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            mode = 1;
        } else if (mode == 1){
            if (playerSpr.GetComponent<PlayerController>().IsGrounded && playerSpr.GetComponent<PlayerController>().enabled){
                playerSpr.GetComponent<PlayerController>().controlEnabled = false;
                playerSpr.GetComponent<PlayerController>().FireBug();
                playerSpr.GetComponent<PlayerController>().enabled = false;
                playerSpr.GetComponent<Animator>().SetBool("chargingGun", false);
                playerSpr.GetComponent<Animator>().Play("Player-Idle");
            }
            if (tbs.IsEmpty()){
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                oruma.SetActive(true);
                playerSpr.flipX = true;
                vanessaSpr.flipX = true;
                playerSpr.GetComponent<Animator>().SetBool("chargingGun", false);
                playerSpr.GetComponent<Animator>().Play("Player-Idle");
                mode = 2;
            }
        } else if (mode == 2){
            if (tbs.IsEmpty()){
                soldier1.velocity = Vector3.right*-3f;
                soldier2.velocity = Vector3.right*-3f;
                if (soldier1.transform.position.x < oruma.transform.position.x + 0.4f){
                    soldier1.velocity = Vector3.zero;
                    soldier2.velocity = Vector3.zero;
                    foreach (TextboxScript.TextBlock textBlock in textToSend3){
                        tbs.AddTextBlock(textBlock);
                    }
                    mode = 3;
                }
            }
        } else if (mode == 3){
            if (tbs.IsEmpty()){
                oruma.transform.position = (soldier1.transform.position + soldier2.transform.position)*0.5f;
                oruma.transform.eulerAngles = new Vector3(0f, 0f, 180f);
                foreach (TextboxScript.TextBlock textBlock in textToSend4){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 4;
            }
        } else if (mode == 4){
            if (tbs.IsEmpty()){
                oruma.GetComponent<Rigidbody2D>().velocity = Vector3.right*-2f;
                soldier1.velocity = Vector3.right*-2f;
                soldier2.velocity = Vector3.right*-2f;
                biitle.velocity = Vector3.right*-1.5f;
                empress.velocity = Vector3.right*-1.5f;
                biitle.GetComponent<Animator>().SetFloat("velocityX", 1f);
                empress.GetComponent<Animator>().SetFloat("velocityX", 1f);
                playerSpr.flipX = false;
                vanessaSpr.flipX = false;
                foreach (TextboxScript.TextBlock textBlock in textToSend5){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 5;
            }
        } else if (mode == 5){
            if (oruma.transform.position.x > -5f){
                soldier1.velocity = Vector3.right*-2f;
                soldier2.velocity = Vector3.right*-2f;
            } else {
                soldier1.velocity = Vector3.zero;
                soldier2.velocity = Vector3.zero;
            }
            if (empress.transform.position.x < vanessaSpr.transform.position.x + 0.4f){
                biitle.velocity = Vector3.zero;
                empress.velocity = Vector3.zero;
                biitle.GetComponent<Animator>().SetFloat("velocityX", 0f);
                empress.GetComponent<Animator>().SetFloat("velocityX", 0f);
            }
            if (tbs.IsEmpty()){
                playerSpr.flipX = false;
                vanessaSpr.flipX = true;
                biitle.velocity = Vector3.right*-1.5f;
                empress.velocity = Vector3.right*-1.5f;
                biitle.GetComponent<Animator>().SetFloat("velocityX", 1f);
                empress.GetComponent<Animator>().SetFloat("velocityX", 1f);
                foreach (TextboxScript.TextBlock textBlock in textToSend6){
                    tbs.AddTextBlock(textBlock);
                }
                delay = 0f;
                mode = 6;
            }
        } else if (mode == 6){
            if (listeners == null){
                listeners = FindObjectsOfType<FollowScript>();
            }
            if (tbs.IsEmpty()){
                foreach (FollowScript fs in listeners){
                    fs.gameObject.SetActive(false);
                }
                delay += Time.deltaTime;
                if (delay > 3f){
                    delay = 0f;
                    foreach (FollowScript fs in listeners){
                        fs.gameObject.SetActive(true);
                    }
                    playerSpr.flipX = false;
                    vanessaSpr.flipX = false;
                    environmentRoom.SetActive(false);
                    environmentBoat.SetActive(true);
                    musicPlayer.clip = boatClip;
                    musicPlayer.Play();
                    mode = 7;
                }
            }
        } else if (mode == 7){
            delay += Time.deltaTime;
            if (delay > 1f){
                delay = 0f;
                foreach (TextboxScript.TextBlock textBlock in textToSend7){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 8;
            }
        } else if (mode == 8){
            if (tbs.IsEmpty()){
                foreach (FollowScript fs in listeners){
                    fs.gameObject.SetActive(false);
                }
                delay += Time.deltaTime;
                if (delay > 1f){
                    SceneManager.LoadScene("Finale6");
                    //load scene
                }
            }
        }
    }
}
