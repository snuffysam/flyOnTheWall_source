using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class Midpoint2_Cut : MonoBehaviour
{
    public GameObject knife;
    public PlayerController player;
    public GameObject cutscene2;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
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
                knife.SetActive(true);
                mode = 3;
            }
        } else if (mode == 3){
            player.controlEnabled = false;
            knife.transform.position += Vector3.right*-10f*Time.deltaTime;
            if (knife.transform.position.x < -9.4f){
                mode = 4;
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
            }
        } else if (mode == 4){
            if (tbs.IsEmpty()){
                cutscene2.SetActive(true);
                mode = 5;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (mode == 0){
            mode = 1;
        }
    }
}
