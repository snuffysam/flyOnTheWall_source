using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class DealDamageScript : MonoBehaviour
{
    public int damageToDeal;
    public float maxLifetime;
    public bool destroyOnHit;
    public bool playerOwned;
    float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > maxLifetime){
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        EnemyCanHurt ech = collision.gameObject.GetComponent<EnemyCanHurt>();
        if (!playerOwned && player != null && !player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player-Hurt") && !player.GetComponent<Animator>().GetBool("dead"))
        {
            for (int i = 0; i < damageToDeal; i++){
                player.GetComponent<Health>().Decrement();
            }
            player.PlayerHit();
        } else if (ech != null && playerOwned){
            ech.Hurt();
        }
        if (destroyOnHit){
            if (playerOwned){
                Debug.Log(collision.gameObject.name);
            }
            Destroy(this.gameObject);
        }
    }
}
