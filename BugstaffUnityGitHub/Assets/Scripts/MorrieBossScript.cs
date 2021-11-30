using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class MorrieBossScript : MonoBehaviour
{
    public GameObject mouthFront;
    public GameObject mouthBack;
    public float minX;
    public float maxX;
    public float speed;
    int mode = 0;
    int hurtCounter = 0;
    public int maxHurtCounter = 3;
    PlayerController player;
    public GameObject doorSummon;
    public AudioSource musicPlayer;
    public TextboxScript.TextBlock[] textToSend1;
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
        float velMult = 1f;
        if (GetComponent<SpriteRenderer>().flipX){
            velMult = -1f;
        }
        if (player == null){
            player = FindObjectOfType<PlayerController>();
        }
        Vector3 mouthPos = mouthFront.transform.position;
        if (GetComponent<SpriteRenderer>().flipX){
            mouthPos = mouthBack.transform.position;
        }

        if (mode == 0){
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GetComponent<Rigidbody2D>().velocity = new Vector3(speed*velMult, 0f, 0f);
            if (transform.position.x > minX){
                mode = 1;
                AudioHandlerScript.PlayClipAtPoint("DoorOpening", "DoorOpening", 3f, transform.position);
                GetComponent<Animator>().SetTrigger("hurt");
            }
        } else if (mode == 1){
            GetComponent<Collider2D>().enabled = true;
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run")){
                GetComponent<Rigidbody2D>().velocity = new Vector3(speed*velMult, 0f, 0f);
                if (player != null && (player.transform.position-mouthPos).magnitude < 1.15f){
                    player.GetComponent<Health>().Decrement();
                    player.PlayerHit();
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                }
                //Debug.Log("running running...");
            } else {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            if (transform.position.x < minX){
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (transform.position.x > maxX){
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (hurtCounter >= maxHurtCounter){
                musicPlayer.Stop();
                GetComponent<Animator>().SetTrigger("unconscious");
                foreach (TextboxScript.TextBlock textBlock in textToSend1){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 2;
            }
        } else if (mode == 2){
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (tbs.IsEmpty()){
                doorSummon.SetActive(true);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<MoveOverTime>() != null){
            GetComponent<Animator>().SetTrigger("hurt");
            AudioHandlerScript.PlayClipAtPoint("DoorOpening", "DoorOpening", 3f, transform.position);
            hurtCounter++;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<DealDamageScript>() != null && col.gameObject.GetComponent<DealDamageScript>().playerOwned){
            GetComponent<Animator>().SetTrigger("spice");
        }
    }
}