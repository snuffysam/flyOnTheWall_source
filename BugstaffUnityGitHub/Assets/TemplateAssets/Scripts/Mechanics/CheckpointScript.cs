using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Platformer.Mechanics;

public class CheckpointScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController player = col.GetComponent<PlayerController>();
        if (player != null){
            player.SetCheckpoint(SceneManager.GetActiveScene().name, transform.position.x, transform.position.y, player.GetComponent<SpriteRenderer>().flipX);
        }
    }
}
