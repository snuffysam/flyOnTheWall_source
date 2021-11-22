using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class RedAdmiralBoss1 : MonoBehaviour
{
    public float[] pauseX;
    public float turnMinX;
    public float turnMaxX;
    public float viewDistance;
    public float delayTime;
    public float moveSpeed;
    public GameObject endBattleObj;
    bool slashed;
    float waitTimer;
    int state;
    int hurtCounter;
    int maxHurtCounter = 3;
    float[] pauseXPrevDist;
    float prevXPos;
    int checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        pauseXPrevDist = new float[pauseX.Length];
        for (int i = 0; i < pauseXPrevDist.Length; i++){
            pauseXPrevDist[i] = 999f;
        }
        prevXPos = transform.position.x;
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
        
        if (transform.position.x > turnMaxX && !GetComponent<SpriteRenderer>().flipX){
            checkpoint = 3;
            GetComponent<SpriteRenderer>().flipX = true;
            return;
        }
        
        if (transform.position.x < turnMinX && GetComponent<SpriteRenderer>().flipX){
            checkpoint = 0;
            GetComponent<SpriteRenderer>().flipX = false;
            return;
        }

        LayerMask lm = LayerMask.GetMask("Default", "PlayerLayer");

        Bounds b = GetComponent<BoxCollider2D>().bounds;

        RaycastHit2D rch2d = Physics2D.Raycast(new Vector2(b.center.x + (b.size.x*velMult*1.1f), b.center.y), new Vector2(viewDistance*velMult, 0f), viewDistance, lm);

        if (state == 0){
            slashed = false;
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < moveSpeed){
                GetComponent<Rigidbody2D>().velocity = new Vector3(moveSpeed*velMult, 0f, 0f);
            }
            if (checkpoint == 0 && transform.position.x > pauseX[0]){
                checkpoint = 1;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                waitTimer = 0f;
                state = 1;
            } else if (checkpoint == 1 && transform.position.x > pauseX[1]){
                checkpoint = 2;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                waitTimer = 0f;
                state = 1;
            } else if (checkpoint == 3 && transform.position.x < pauseX[1]){
                checkpoint = 4;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                waitTimer = 0f;
                state = 1;
            } else if (checkpoint == 4 && transform.position.x < pauseX[0]){
                checkpoint = 5;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                waitTimer = 0f;
                state = 1;
            }
            /*waitTimer += Time.deltaTime;
            float dX = Mathf.Abs(transform.position.x-prevXPos);
            for (int i = 0; i < pauseX.Length; i++){
                float dist = Mathf.Abs(transform.position.x-pauseX[i]);
                if (dist < dX*2f && waitTimer > 0.1f){
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    waitTimer = 0f;
                    state = 1;
                }
            }
            prevXPos = transform.position.x;*/
        } else if (state == 1){
            slashed = false;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            waitTimer += Time.deltaTime;
            if (waitTimer > delayTime){
                state = 0;
                waitTimer = 0f;
            }
        } else if (state == 2){
            if (!slashed && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Slash") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.18f){
                slashed = true;
                if (rch2d.collider != null && rch2d.collider.GetComponent<PlayerController>() != null){
                    rch2d.collider.GetComponent<PlayerController>().GetComponent<Health>().Decrement();
                    rch2d.collider.GetComponent<PlayerController>().PlayerHit();
                }
            } else if (slashed){
                state = 1;
            }
        }

        if (state != 2 && rch2d.collider != null && rch2d.collider.GetComponent<PlayerController>() != null && !rch2d.collider.GetComponent<PlayerController>().IsInvisible()){
            state = 2;
            GetComponent<Animator>().SetTrigger("Spotted");
        }

        GetComponent<Animator>().SetFloat("deltaX", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<MoveOverTime>() != null){
            GetComponent<Animator>().SetTrigger("hurt");
            hurtCounter++;
        }
    }
}
