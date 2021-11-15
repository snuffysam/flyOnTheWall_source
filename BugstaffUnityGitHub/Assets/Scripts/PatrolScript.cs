using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class PatrolScript : MonoBehaviour
{
    public float patrolDistance;
    public float viewDistance;
    public float delayTime;
    public float shootDelay;
    public float moveSpeed;
    public GameObject shootPrefab;
    public float shootXOffset;
    public float shootYOffset;
    bool spotted;
    float startX;
    float endX;
    float waitTimer;
    float shootTimer;
    int state;
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        endX = startX+patrolDistance;
        state = 0;
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

        if (state == 0){
            waitTimer += Time.deltaTime;
            GetComponent<Animator>().speed = 0.5f;
            if (waitTimer > delayTime){
                state = 1;
                GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                waitTimer = 0f;
            }
        } else if (state == 1){
            GetComponent<Animator>().speed = 1;
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < moveSpeed){
                GetComponent<Rigidbody2D>().velocity = new Vector3(moveSpeed*velMult, 0f, 0f);
            }
            if ((!GetComponent<SpriteRenderer>().flipX && transform.position.x > Mathf.Max(startX, endX)) || (GetComponent<SpriteRenderer>().flipX && transform.position.x < Mathf.Min(startX, endX))){
                state = 0;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        } else if (state == 2){
            GetComponent<Animator>().speed = 1;
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootDelay){
                shootTimer = 0f;
                if (shootPrefab != null){
                    GameObject go = Instantiate<GameObject>(shootPrefab);
                    go.transform.position = transform.position + new Vector3(shootXOffset*velMult, shootYOffset);
                    if (go.GetComponent<SpriteRenderer>() != null){
                        go.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
                    }
                }
            }
        }

        LayerMask lm = LayerMask.GetMask("Default", "PlayerLayer");

        Bounds b = GetComponent<BoxCollider2D>().bounds;

        RaycastHit2D rch2d = Physics2D.Raycast(new Vector2(b.center.x + (b.size.x*velMult*1.1f), b.center.y), new Vector2(viewDistance*velMult, 0f), viewDistance, lm);
        if (rch2d.collider != null && rch2d.collider.GetComponent<PlayerController>() != null && !rch2d.collider.GetComponent<PlayerController>().IsInvisible()){
            state = 2;
            spotted = true;
        } else if (state == 2){
            state = 0;
            shootTimer = shootDelay-(1f/60f);
            spotted = false;
        }

        if (rch2d.collider != null){
            Debug.Log("Spotted: " + rch2d.collider.gameObject.name);
        }

        GetComponent<Animator>().SetBool("Spotted", spotted);

        GetComponent<Animator>().SetFloat("deltaX", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }
}
