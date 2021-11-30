using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class PalmBossScript : MonoBehaviour
{
    public PlayerController player;
    public float speed;
    public List<Vector2> keyPoints;
    float mult;
    int mode;
    int index;
    float prevDist;
    float dist;
    Vector3 prevPos;
    float stunTimer;
    // Start is called before the first frame update
    void Start()
    {
        mult = 1f;
        index = 0;
        mode = 0;
        prevDist = 9999f;
        stunTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        stunTimer -= Time.deltaTime;
        if (stunTimer > 0f){
            mult = 0.5f;
        }

        Vector3 nextPoint = transform.position;
        if (index < keyPoints.Count){
            nextPoint = (Vector3)(keyPoints[index]);
        }

        dist = (transform.position-nextPoint).magnitude;
        float deltaMove = (transform.position-prevPos).magnitude;
        bool close = dist*2f > deltaMove;
        Vector3 move = (nextPoint - transform.position).normalized*speed*mult;
        GetComponent<Animator>().speed = (mult+0.25f);

        if (mode == 1){
            if (close){
                GetComponent<Rigidbody2D>().velocity = move;
            } else {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        } else if (mode == 2){
            if (close){
                GetComponent<Rigidbody2D>().velocity = move;
            } else {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                prevDist = 9999f;
                index++;
            }
            if (player.transform.position.y > transform.position.y + 0.5f){
                mult = 3f;
            } else {
                mult = 1f;
            }
        } else if (mode == 3){
            if (close){
                GetComponent<Rigidbody2D>().velocity = move;
                mult = 5f;
            } else {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                mult = 2f;
                mode = 4;
            }
            index = keyPoints.Count-1;
        } else if (mode == 5){
            GetComponent<Animator>().speed = 0f;
        }
        //Debug.Log("mode: " + mode + ", speed: " + GetComponent<Rigidbody2D>().velocity.magnitude + ", dist: " + dist + ", delta: " + deltaMove + ", index: " + index);

        prevPos = transform.position;

        prevDist = dist;
    }

    public void Reveal(){
        if (mode == 0){
            mode = 1;
            prevDist = 9999f;
        }
    }

    public void StartBattle(){
        if (mode == 1){
            mode = 2;
            prevDist = 9999f;
        }
    }

    public void FinishBattle(){
        if (mode == 2){
            mode = 3;
            prevDist = 9999f;
        }
    }

    public bool IsAtEnd(){
        return mode == 4;
    }

    public void StopMoving(){
        if (mode == 4){
            mode = 5;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<DealDamageScript>() != null || other.GetComponent<ReflectScript>() != null){
            stunTimer = 1f;
        }
        if (other.gameObject == player.gameObject){
            player.GetComponent<Health>().Die();
            player.PlayerHit();
        }
    }
}
