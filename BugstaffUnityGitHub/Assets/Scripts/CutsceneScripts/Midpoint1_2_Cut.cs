using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class Midpoint1_2_Cut : MonoBehaviour
{
    int mode;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    public GameObject door;
    TextboxScript tbs;
    //public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        tbs = FindObjectOfType<TextboxScript>();
        for (int i = 0; i < textToSend1.Length; i++){
            TextboxScript.TextBlock tb = textToSend1[i];
            string str = tb.text.Replace("[a]", MissionManager.GetCharmName());
            textToSend1[i] = new TextboxScript.TextBlock{speakerName = tb.speakerName, text = str, emphasis = tb.emphasis};
        }
        for (int i = 0; i < textToSend2.Length; i++){
            TextboxScript.TextBlock tb = textToSend2[i];
            string str = tb.text.Replace("[b]", MissionManager.GetCharmTutorial());
            textToSend2[i] = new TextboxScript.TextBlock{speakerName = tb.speakerName, text = str, emphasis = tb.emphasis};
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tbs == null){
            tbs = FindObjectOfType<TextboxScript>();
        }
        if (mode == 0){
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
            mode = 1;
        } else if (mode == 1){
            if (tbs.IsEmpty()){
                BugShotScript[] bss = FindObjectsOfType<BugShotScript>();
                for (int i = 0; i < bss.Length; i++){
                    Destroy(bss[i].gameObject);
                }
                GetComponent<SpriteRenderer>().enabled = false;
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 2;
            }
        } else if (mode == 2){
            if (tbs.IsEmpty()){
                MissionManager.UpdatePlayerGadgets();
                door.SetActive(true);
                mode = 3;
            }
        }
    }
}
