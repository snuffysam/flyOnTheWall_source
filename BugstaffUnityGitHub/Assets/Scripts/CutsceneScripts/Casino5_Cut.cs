using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casino5_Cut : MonoBehaviour
{
    public GameObject hercules;
    public GameObject cricketCutscene;
    public GameObject mantisCutscene;
    public GameObject cricketBoss;
    public GameObject mantisBoss;
    public GameObject player;
    public AudioSource musicPlayer;
    public AudioClip bossClip;
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
                hercules.SetActive(true);
                mantisCutscene.SetActive(true);
                cricketCutscene.SetActive(true);
                hercules.GetComponent<Animator>().SetTrigger("tiedUp");
                player.GetComponent<SpriteRenderer>().flipX = true;
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 3;
            }
        } else if (mode == 3){
            if (tbs.IsEmpty()){
                mantisCutscene.SetActive(false);
                cricketCutscene.SetActive(false);
                mantisBoss.SetActive(true);
                cricketBoss.SetActive(true);
                musicPlayer.clip = bossClip;
                musicPlayer.pitch = AudioHandlerScript.bossMult;
                musicPlayer.Play();
                mode = 4;
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
