using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Platformer.Mechanics;

public class MissionManagerScript : MonoBehaviour
{
    public FadeOutScript whiteScreen;
    public GameObject[] folders;
    public GameObject[] missionNames;
    public float selectedHeight;
    public float selectDelay;
    public Text passcodeText;
    public TextboxScript.TextBlock[] textToSend1;
    int index = 0;
    float[] startingHeights;
    bool canPressVert;
    float delay;
    float counter;
    TextboxScript tbs;
    bool alreadySent;
    // Start is called before the first frame update
    void Start()
    {
        startingHeights = new float[folders.Length];
        for (int i = 0; i < folders.Length; i++){
            startingHeights[i] = folders[i].transform.position.y;
        }
        canPressVert = false;
        while (MissionManager.IsMissionBeaten(index)){
            index++;
        }
        tbs = FindObjectOfType<TextboxScript>();
        alreadySent = false;
        passcodeText.text = "-Passcode-\n" + PasscodeHandler.GetCurrentPasscode();

        if (MissionManager.missionNumber == 3){
            MissionManager.missionIndex = 3;
            Health.currentHP = Health.maxHP;
            SceneManager.LoadScene(MissionManager.GetMissionStart());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tbs == null){
            tbs = FindObjectOfType<TextboxScript>();
        }

        if (Input.GetAxis("Vertical") == 0f){
            canPressVert = true;
        }
        if (canPressVert && Input.GetAxis("Vertical") > 0){
            AudioHandlerScript.PlaySound("MenuScroll", 1f);
            canPressVert = false;
            do {
                index--;
                if (index < 0){
                    index = folders.Length-1;
                }
            } while (MissionManager.IsMissionBeaten(index));
        }
        if (canPressVert && Input.GetAxis("Vertical") < 0){
            AudioHandlerScript.PlaySound("MenuScroll", 1f);
            canPressVert = false;
            do {
                index++;
                if (index >= folders.Length){
                    index = 0;
                }
            } while (MissionManager.IsMissionBeaten(index));
        }

        counter += Time.deltaTime;
        if (!alreadySent && MissionManager.missionNumber == 0 && counter > 0.25f){
            alreadySent = true;
            foreach (TextboxScript.TextBlock textBlock in textToSend1){
                tbs.AddTextBlock(textBlock);
            }
        }

        if (!tbs.IsEmpty()){
            counter = 0.26f;
        }

        for (int i = 0; i < folders.Length; i++){
            if (MissionManager.IsMissionBeaten(i)){
                folders[i].GetComponent<SpriteRenderer>().enabled = false;
            } else {
                folders[i].GetComponent<SpriteRenderer>().enabled = true;
            }

            if (index == i){
                folders[i].transform.position = new Vector3(folders[i].transform.position.x, selectedHeight, folders[i].transform.position.z);
            } else {
                folders[i].transform.position = new Vector3(folders[i].transform.position.x, startingHeights[i], folders[i].transform.position.z);
            }
        }

        //reminder: remove index == 0 once more content is in
        if (Input.GetButtonDown("Fire1") && delay == 0f && counter > 0.4f){
            AudioHandlerScript.PlaySound("MenuSelect", 1f);
            MissionManager.missionIndex = index;
            missionNames[index].SetActive(true);
            delay += Time.deltaTime;
        } else if (delay > 0f){
            Health.currentHP = Health.maxHP;
            whiteScreen.fadeOut = false;
            delay += Time.deltaTime;
            if (delay > selectDelay){
                SceneManager.LoadScene(MissionManager.GetMissionStart());
            }
        }
    }
}
