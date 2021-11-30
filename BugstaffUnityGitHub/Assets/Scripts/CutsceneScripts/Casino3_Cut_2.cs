using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class Casino3_Cut_2 : MonoBehaviour
{
    public Casino3_Cut drinks;
    public PlayerController player;
    public GameObject hercules;
    public GameObject door;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    public TextboxScript.TextBlock[] textToSend3;
    int mode;
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
        if (mode == 1){
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            mode = 2;
        } else if (mode == 2){
            if (tbs.IsEmpty()){
                mode = 3;
            }
        } else if (mode == 3){
            player.controlEnabled = false;
            bool playerDone = false;
            bool herculesDone = false;
            if (player.transform.position.x < 0f){
                player.SetMove(new Vector2(1f, 0f));
                player.GetComponent<SpriteRenderer>().flipX = false;
            } else {
                playerDone = true;
                player.SetMove(new Vector2(0f, 0f));
                player.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (hercules.transform.position.x < -0.5f){
                hercules.transform.position += Vector3.right*1.5f*Time.deltaTime;
                hercules.GetComponent<SpriteRenderer>().flipX = false;
                hercules.GetComponent<Animator>().SetFloat("velocityX", 1f);
            } else {
                herculesDone = true;
                hercules.GetComponent<SpriteRenderer>().flipX = false;
                hercules.GetComponent<Animator>().SetFloat("velocityX", 0f);
            }

            if (playerDone && herculesDone){
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 4;
            }
        } else if (mode == 4){
            if (tbs.IsEmpty()){
                foreach (TextboxScript.TextBlock textBlock in textToSend3){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 5;
            }
        } else if (mode == 5){
            if (hercules.transform.position.x > -2.2f){
                hercules.transform.position -= Vector3.right*1f*Time.deltaTime;
                hercules.GetComponent<SpriteRenderer>().flipX = true;
                hercules.GetComponent<Animator>().SetFloat("velocityX", 1f);
            } else {
                hercules.GetComponent<SpriteRenderer>().flipX = true;
                hercules.GetComponent<Animator>().SetFloat("velocityX", 0f);
            }
            if (tbs.IsEmpty()){
                door.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (mode == 0 && drinks.HasDrinks()){
            mode = 1;
        }
    }
}
