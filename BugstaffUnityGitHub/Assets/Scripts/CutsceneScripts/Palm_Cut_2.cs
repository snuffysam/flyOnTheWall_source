using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class Palm_Cut_2 : MonoBehaviour
{
    public PlayerController player;
    public PalmBossScript bossScript;
    public AudioSource musicPlayer;
    public AudioClip bossClip;
    public TextboxScript.TextBlock[] textToSend1;
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
            musicPlayer.clip = bossClip;
            musicPlayer.pitch = AudioHandlerScript.bossMult2;
            musicPlayer.Play();
            bossScript.Reveal();
            mode = 2;
        } else if (mode == 2){
            if (tbs.IsEmpty()){
                bossScript.StartBattle();
                mode = 3;
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
