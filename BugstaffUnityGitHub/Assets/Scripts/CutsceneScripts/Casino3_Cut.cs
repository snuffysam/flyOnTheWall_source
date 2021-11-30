using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class Casino3_Cut : ButtonActivateScript
{
    public PlayerController player;
    public SpriteRenderer mixologist;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    public TextboxScript.TextBlock[] textToSend3;
    public TextboxScript.TextBlock[] textToSend4;
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
            mixologist.flipX = false;
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            mode = 2;
        } else if (mode == 3){
            foreach (TextboxScript.TextBlock textBlock in textToSend2){
                tbs.AddTextBlock(textBlock);
            }
            mode = 4;
        } else if (mode == 4){
            if (tbs.IsEmpty()){
                //player.GetComponent<Health>().Decrement();
                //player.PlayerHit();
                foreach (TextboxScript.TextBlock textBlock in textToSend3){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 5;
            }
        } else if (mode == 6){
            foreach (TextboxScript.TextBlock textBlock in textToSend4){
                tbs.AddTextBlock(textBlock);
            }
            mode = 2;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (mode == 0){
            mode = 1;
        }
        if (mode == 5){
            mode = 6;
        }
    }

    public bool HasDrinks(){
        return mode == 2;
    }

    public override void ExecuteButton(){
        base.ExecuteButton();
        if (mode == 2){
            mode = 3;
        }
    }
}
