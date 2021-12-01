using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class TwinsBossFight : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float moveSpeed;
    public float waitTime;
    public TwinsBossFight otherTwin;
    public PlayerController player;
    int hits;
    public int maxHits;
    public GameObject spikeballPrefab;
    public GameObject targetOverlay;
    public GameObject endCutscene;
    int mode;
    float targetX;
    float currentDelay;
    GameObject spawnedSpikeball;
    GameObject currentTarget;
    bool alreadyHurt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Hurt")){
            if (!alreadyHurt){
                alreadyHurt = true;
                AudioHandlerScript.PlayHitAtPoint(transform.position, 2);
            }
            targetOverlay.SetActive(false);
            if (spawnedSpikeball != null){
                Destroy(spawnedSpikeball);
            }
            if (mode != 0){
                hits++;
            }
            if (hits + otherTwin.GetHits() > maxHits){
                endCutscene.SetActive(true);
            }
            mode = 0;
            return;
        }

        alreadyHurt = false;

        if (mode == 0){
            targetOverlay.SetActive(false);
            if (IsLeft()){
                targetX = Random.Range(GetTargetX(),transform.position.x);
            } else {
                targetX = Random.Range(transform.position.x, GetTargetX());
            }
            mode = 1;
        } else if (mode == 1){
            currentDelay = 0f;
            GetComponent<Animator>().SetFloat("velocityX", 1f);
            if (IsLeft()){
                GetComponent<Rigidbody2D>().velocity = Vector3.right*moveSpeed*-1f;
                if (transform.position.x < targetX || IsBetween(player.transform.position.x, transform.position.x, targetX)){
                    GetComponent<SpriteRenderer>().flipX = !IsLeft();
                    mode = 2;
                }
            } else {
                GetComponent<Rigidbody2D>().velocity = Vector3.right*moveSpeed;
                if (transform.position.x > targetX || IsBetween(player.transform.position.x, transform.position.x, targetX)){
                    GetComponent<SpriteRenderer>().flipX = !IsLeft();
                    mode = 2;
                }
            }
        } else if (mode == 2){
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Animator>().SetFloat("velocityX", 0f);
            currentDelay += Time.deltaTime;
            if (currentDelay > waitTime){
                currentDelay = 0f;
                GetComponent<Animator>().SetTrigger("attack");
                mode = 3;
            }
        } else if (mode == 3){
            float nTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
            float throwTime = 30f/70f;
            if (nTime < throwTime){
                currentTarget = player.gameObject;
                if (player.IsInvisible()){
                    currentTarget = otherTwin.gameObject;
                }
            } else {
                if (spawnedSpikeball == null){
                    spawnedSpikeball = Instantiate<GameObject>(spikeballPrefab);
                    spawnedSpikeball.GetComponent<DealDamageScript>().playerOwned = currentTarget != player.gameObject;
                    spawnedSpikeball.transform.position = transform.position + Vector3.up*0.75f;
                    AudioHandlerScript.PlayClipAtPoint("MenuScroll", "MenuScroll", 1f, spawnedSpikeball.transform.position);
                } else {
                    spawnedSpikeball.transform.position += Vector3.up*1.5f*Time.deltaTime;
                }
            }
            if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack")){
                mode = 4;
            }
            targetOverlay.SetActive(true);
        } else if (mode == 4) {
            targetOverlay.SetActive(true);
            if (currentTarget == null){
                Destroy(spawnedSpikeball);
            }
            if (spawnedSpikeball == null){
                targetOverlay.SetActive(false);
                mode = 0;
            }
            if (spawnedSpikeball != null && currentTarget != null){
                spawnedSpikeball.transform.position += (currentTarget.transform.position-spawnedSpikeball.transform.position).normalized*5f*Time.deltaTime;
            }
        }

        if (currentTarget != null){
            targetOverlay.transform.position = currentTarget.transform.position;
            if (IsLeft()){
                targetOverlay.transform.Rotate(0f, 0f, Time.deltaTime*150f);
            } else {
                targetOverlay.transform.Rotate(0f, 0f, Time.deltaTime*-150f);
            }
        }
        if (spawnedSpikeball != null){
            if (IsLeft()){
                targetOverlay.transform.Rotate(0f, 0f, Time.deltaTime*150f);
            } else {
                targetOverlay.transform.Rotate(0f, 0f, Time.deltaTime*-150f);
            }
        }
    }

    public bool IsBetween(float testValue, float bound1, float bound2)
    {
        return (testValue >= Mathf.Min(bound1,bound2) && testValue <= Mathf.Max(bound1,bound2));
    }

    bool IsLeft(){
        return GetComponent<SpriteRenderer>().flipX;
    }

    float Midpoint(){
        return (otherTwin.transform.position.x + transform.position.x)/2f;
    }

    float GetTargetX(){
        if (IsLeft()){
            if (Midpoint() < transform.position.x){
                return Midpoint();
            } else {
                return minX;
            }
        } else {
            if (Midpoint() > transform.position.x){
                return Midpoint();
            } else {
                return maxX;
            }
        }
        //return 0f;
    }

    public int GetHits(){
        return hits;
    }
}
