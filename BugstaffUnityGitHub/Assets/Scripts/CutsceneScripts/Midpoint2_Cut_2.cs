using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class Midpoint2_Cut_2 : MonoBehaviour
{
    public GameObject knife;
    public PlayerController player;
    public GameObject knifePrefab;
    public GameObject cutscene3;
    public float knifeSpawnDelay;
    public float knifeTotalTime;
    public AudioSource musicPlayer;
    public AudioClip bossClip;
    public AudioClip ambientClip;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    public TextboxScript.TextBlock[] textToSend3;
    int mode;
    TextboxScript tbs;
    float spawnDelay;
    float totalTime;
    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        tbs = FindObjectOfType<TextboxScript>();
        spawnDelay = knifeSpawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (tbs == null){
            tbs = FindObjectOfType<TextboxScript>();
        }

        if (mode == 4){
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            mode = 5;
        } else if (mode == 5){
            if (tbs.IsEmpty()){
                knife.SetActive(true);
                knife.transform.position = new Vector3(player.transform.position.x+0.5f, 0.75f, 0f);
                mode = 6;
            }
        } else if (mode == 6){
            knife.transform.position += Vector3.up*-13f*Time.deltaTime;
            if (knife.transform.position.y < -1.2f){
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 7;
            }
        } else if (mode == 7){
            if (tbs.IsEmpty()){
                mode = 8;
                musicPlayer.clip = bossClip;
                musicPlayer.pitch = AudioHandlerScript.bossMult;
                musicPlayer.Play();
            }
        } else if (mode == 8){
            spawnDelay += Time.deltaTime;
            totalTime += Time.deltaTime;

            if (spawnDelay > knifeSpawnDelay && totalTime < knifeTotalTime-knifeSpawnDelay){
                spawnDelay = 0f;
                GameObject go = Instantiate<GameObject>(knifePrefab);
                go.transform.position = new Vector3(Random.Range(-9.3f, 0.2f), 2f, 0f);
                go.GetComponent<Rigidbody2D>().velocity = (player.transform.position-go.transform.position).normalized*2.5f;
                //spawn knife
            }
            if (totalTime > knifeTotalTime){
                foreach (TextboxScript.TextBlock textBlock in textToSend3){
                    tbs.AddTextBlock(textBlock);
                }
                cutscene3.SetActive(true);
                musicPlayer.clip = ambientClip;
                musicPlayer.pitch = 1f;
                musicPlayer.Play();
                mode = 9;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (mode < 4){
            Debug.Log("mode! " + mode);
            mode++;
        }
    }
}
