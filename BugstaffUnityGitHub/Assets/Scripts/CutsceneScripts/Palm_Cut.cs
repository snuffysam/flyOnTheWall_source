using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palm_Cut : MonoBehaviour
{
    public GameObject door;
    public TextboxScript.TextBlock[] textToSend1;
    int mode;
    TextboxScript tbs;
    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        tbs = FindObjectOfType<TextboxScript>();
        textToSend1[textToSend1.Length-1] = new TextboxScript.TextBlock{speakerName = "", text = "[ Passcode : " + PasscodeHandler.GetCurrentPasscode() + " ]", emphasis = false};
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
                MissionManager.UpdatePlayerGadgets();
                door.SetActive(true);
                mode = 3;
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
