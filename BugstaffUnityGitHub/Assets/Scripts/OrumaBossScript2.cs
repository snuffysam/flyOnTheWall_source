using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using UnityEngine.SceneManagement;

public class OrumaBossScript2 : MonoBehaviour
{
    public PlayerController player;
    public GameObject knifePrefab;
    public GameObject unconscious;
    public AudioSource musicPlayer;
    public Vector3[] teleportPoints;
    float delay;
    int mode;
    int counter;
    Vector3 goToPos;
    // Start is called before the first frame update
    void Start()
    {
        musicPlayer.pitch = AudioHandlerScript.bossMult2;
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

        //transform.position = goToPos;

        if (mode == 0){
            delay += Time.deltaTime;
            if (delay > 2f){
                delay = 0f;
                goToPos = GetTeleportPos();
                transform.position = goToPos;
                Debug.Log("teleported to: " + transform.position);
                GetComponent<OscillateScript>().minX = transform.position.y-0.2f;
                GetComponent<OscillateScript>().maxX = transform.position.y+0.2f;
                counter++;
            }
            if (Mathf.Abs(transform.position.y-player.transform.position.y) < 1f && !player.IsInvisible()){
                mode = 2;
            }
            if (counter > 4){
                delay = 0f;
                counter = 0;
                mode = 1;
            }
        } else if (mode == 1){
            delay += Time.deltaTime;
            if (delay > 0.5f){
                delay = 0f;
                goToPos = GetTeleportPos();
                transform.position = goToPos;
                Debug.Log("teleported to: " + transform.position);
                GetComponent<OscillateScript>().minX = transform.position.y-0.2f;
                GetComponent<OscillateScript>().maxX = transform.position.y+0.2f;
                counter++;
            }
            if (Mathf.Abs(transform.position.y-player.transform.position.y) < 1f && !player.IsInvisible()){
                mode = 2;
            }
            if (counter > 10){
                delay = 0f;
                counter = 0;
                mode = 0;
            }
        } else if (mode == 2){
            if (Mathf.Abs(transform.position.y-player.transform.position.y) >= 1f || player.IsInvisible()){
                mode = 0;
            }

            delay += Time.deltaTime;

            if (delay > 0.05f && counter < 10){
                delay = 0f;
                counter++;
                AudioHandlerScript.PlayClipAtPoint("EnemyFootsteps6", "EnemyFootstepsBugvision6", 3f, transform.position);
                GameObject go = Instantiate<GameObject>(knifePrefab);
                float knifeMult = 6f;
                Vector3 vel = Vector3.right*velMult*knifeMult;
                go.GetComponent<Rigidbody2D>().velocity = vel;
                go.transform.position = transform.position + Vector3.up*Random.Range(-0.5f, 0.5f) + vel*0.2f;
            }
        } else if (mode == 3){
            unconscious.SetActive(true);
            unconscious.transform.position = transform.position;
            unconscious.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
            GetComponent<SpriteRenderer>().enabled = false;
            mode = 4;
        } else if (mode == 4){
            if ((transform.position-player.transform.position).magnitude < 1.5f){
                SceneManager.LoadScene("Finale4");
                //next scene
            }
        }
    }

    Vector3 GetTeleportPos(){
        Vector3 pos = transform.position;
        while ((pos-transform.position).magnitude < 1f || (pos-player.transform.position).magnitude < 2f){
            pos = teleportPoints[Random.Range(0,teleportPoints.Length)];
        }
        Debug.Log("teleport pos: " + pos);
        return pos;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (mode < 3 && col.gameObject.GetComponent<DealDamageScript>() != null && col.gameObject.GetComponent<DealDamageScript>().playerOwned){
            //AudioHandlerScript.PlayHitAtPoint(transform.position, 3);
            AudioHandlerScript.PlayDeathAtPoint(transform.position, 3);
            delay = 0f;
            mode = 3;
        }
    }
}
