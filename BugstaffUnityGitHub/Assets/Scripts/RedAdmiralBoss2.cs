using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class RedAdmiralBoss2 : MonoBehaviour
{
    public float turnMinX;
    public float turnMaxX;
    public float viewDistance;
    public float delayTime;
    public float moveSpeed;
    public GameObject endBattleObj;
    bool slashed;
    int hurtCounter;
    int maxHurtCounter = 3;
    int mode = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float velMult = 1f;
        if (GetComponent<SpriteRenderer>().flipX){
            velMult = -1f;
        }

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Hurt")){
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            mode = 0;
            return;
        }

        if (hurtCounter >= maxHurtCounter){
            endBattleObj.SetActive(true);
            endBattleObj.transform.position = transform.position;
            endBattleObj.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(GetComponent<Rigidbody2D>());
            this.enabled = false;
        }

        if (transform.position.x > turnMaxX || transform.position.x < turnMinX){
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Animator>().SetTrigger("hurt");
            AudioHandlerScript.PlayClipAtPoint("DoorClosing", "DoorClosing", 0.5f, transform.position);
            hurtCounter++;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            if (transform.position.x > turnMaxX){
                transform.position = new Vector3(turnMaxX-0.2f, transform.position.y, transform.position.z);
            } else {
                transform.position = new Vector3(turnMinX+0.2f, transform.position.y, transform.position.z);
            }
            return;
        }

        LayerMask lm = LayerMask.GetMask("PlayerLayer");
        Bounds b = GetComponent<BoxCollider2D>().bounds;
        float viewMult = 15f;
        RaycastHit2D playerCloseToHit = Physics2D.Raycast(new Vector2(b.center.x + (b.size.x*velMult*1.1f), b.center.y), new Vector2(viewDistance*velMult, 0f), viewDistance, lm);
        RaycastHit2D playerCloseToChase = Physics2D.Raycast(new Vector2(b.center.x + (b.size.x*velMult*1.1f), b.center.y), new Vector2(viewDistance*viewMult*velMult, 0f), viewDistance*viewMult, lm);

        if (mode == 0){
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (DidHit(playerCloseToChase)){
                mode = 1;
            }
        } else if (mode == 1){
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < moveSpeed){
                GetComponent<Rigidbody2D>().velocity = new Vector3(moveSpeed*velMult, 0f, 0f);
            }
            if (DidHit(playerCloseToHit)){
                GetComponent<Animator>().SetTrigger("Spotted");
                AudioHandlerScript.PlayClipAtPoint("EnemyFootsteps6", "EnemyFootstepsBugvision6", 1f, transform.position);
                mode = 2;
            }
        } else if (mode == 2){
            if (!slashed && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Slash") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.18f){
                slashed = true;
                if (DidHit(playerCloseToHit)){
                    playerCloseToHit.collider.GetComponent<PlayerController>().GetComponent<Health>().Decrement();
                    playerCloseToHit.collider.GetComponent<PlayerController>().PlayerHit();
                }
            } else if (slashed && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Slash")){
                slashed = false;
                mode = 0;
            }
        }

        GetComponent<Animator>().SetFloat("deltaX", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }

    bool DidHit(RaycastHit2D rch2d){
        return rch2d.collider != null && rch2d.collider.GetComponent<PlayerController>() != null && !rch2d.collider.GetComponent<PlayerController>().IsInvisible();
    }
}
