using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Airship5_Cut : MonoBehaviour
{
    public GameObject missionCompleteText;
    public AudioSource musicPlayer;
    public AudioClip bossClip;
    public GameObject[] cannons;
    public float[] cannonTimes;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    int mode;
    TextboxScript tbs;
    float timer;
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
                musicPlayer.clip = bossClip;
                musicPlayer.pitch = AudioHandlerScript.bossMult;
                musicPlayer.Play();
                mode = 3;
            }
        } else if (mode == 3){
            timer += Time.deltaTime;
            for (int i = 0; i < cannons.Length && i < cannonTimes.Length; i++){
                if (cannons[i] != null && timer > cannonTimes[i]){
                    cannons[i].SetActive(true);
                }
            }
            if (timer > cannonTimes[cannonTimes.Length-1]+4f){
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                musicPlayer.Stop();
                mode = 4;
            }
        } else if (mode == 4){
            if (tbs.IsEmpty()){
                missionCompleteText.SetActive(true);
                MissionManager.CompleteMission();
                timer = 0f;
                mode = 5;
            }
        } else if (mode == 5){
            timer += Time.deltaTime;
            if (timer > 5f){
                SceneManager.LoadScene("MissionSelect");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (mode == 0){
            AudioHandlerScript.PlayClipAtPoint("ComputerBeeping", "ComputerBeepingBugvision", 1f, other.transform.position);
            mode = 1;
        }
    }
}
