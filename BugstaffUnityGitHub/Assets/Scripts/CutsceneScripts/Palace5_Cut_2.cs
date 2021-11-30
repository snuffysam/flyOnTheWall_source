using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palace5_Cut_2 : MonoBehaviour
{
    public GameObject morrie;
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
            mode = 2;
            musicPlayer.Stop();
        } else if (mode == 2){
            if (tbs.IsEmpty()){
                morrie.SetActive(true);
                musicPlayer.clip = bossClip;
                musicPlayer.pitch = AudioHandlerScript.bossMult;
                musicPlayer.Play();
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
