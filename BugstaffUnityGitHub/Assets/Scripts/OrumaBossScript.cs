using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using UnityEngine.SceneManagement;

public class OrumaBossScript : MonoBehaviour
{
    public float minX, minY, maxX, maxY;
    public GameObject knifePrefab;
    public PlayerController player;
    public Sprite floatSprite, hurtSprite;
    public float speed;
    public GameObject spikeball;
    public AudioSource musicPlayer;
    Vector3 targetPos;
    int mode;
    float delay;
    int hits;
    // Start is called before the first frame update
    void Start()
    {
        musicPlayer.pitch = AudioHandlerScript.bossMult;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < transform.position.x){
            GetComponent<SpriteRenderer>().flipX = true;
        } else {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        float velMult = 1f;
        if (GetComponent<SpriteRenderer>().flipX){
            velMult = -1f;
        }
        
        if (hits > 3){
            mode = 3;
        }
        //Debug.Log("current mode: " + mode);

        if (mode == 0){
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = floatSprite;
            GetComponent<OscillateScript>().enabled = true;
            if ((transform.position-player.transform.position).magnitude < 4.15f){
                targetPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
                mode = 1;
            }
        } else if (mode == 1){
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<OscillateScript>().enabled = false;
            Vector3 disp = (targetPos-transform.position).normalized*speed*Time.deltaTime;
            if ((targetPos-transform.position).magnitude < disp.magnitude*2f){
                mode = 0;
            } else {
                transform.position += disp;
            }
            GetComponent<OscillateScript>().minX = transform.position.y-0.2f;
            GetComponent<OscillateScript>().maxX = transform.position.y+0.2f;

            if (Random.Range(0f, 1f) < Time.deltaTime*0.33f){
                GameObject go = Instantiate<GameObject>(knifePrefab);
                AudioHandlerScript.PlayClipAtPoint("EnemyFootsteps6", "EnemyFootstepsBugvision6", 3f, transform.position);
                float knifeMult = 6f;
                Vector3 vel = (player.transform.position-transform.position).normalized*knifeMult;
                if (player.IsInvisible()){
                    vel = (Vector3.right*velMult+Vector3.up*0.2f).normalized*knifeMult;
                }
                go.GetComponent<Rigidbody2D>().velocity = vel;
                go.transform.position = transform.position + vel*0.2f;
            }
        } else if (mode == 2){
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = hurtSprite;
            GetComponent<OscillateScript>().enabled = false;
            delay += Time.deltaTime;
            if (delay > 1f){
                mode = 0;
            }
        } else if (mode == 3){
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = hurtSprite;
            GetComponent<OscillateScript>().enabled = false;
            spikeball.transform.localScale = Vector3.one*delay*10f;
            AudioHandlerScript.PlayFootstep(new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f));
            delay += Time.deltaTime;
            if (delay > 3f){
                SceneManager.LoadScene("Finale3");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (mode < 2 && col.gameObject.GetComponent<DealDamageScript>() != null && col.gameObject.GetComponent<DealDamageScript>().playerOwned){
            hits++;
            AudioHandlerScript.PlayClipAtPoint("DoorClosing", "DoorClosing", 0.5f, transform.position);
            delay = 0f;
            mode = 2;
        }
    }
}
