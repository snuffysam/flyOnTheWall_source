using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasscodeMenu : MonoBehaviour
{
    public GameObject[] bugIcons;
    public Text typeText;
    public GameObject incorrectLabel;
    public MainMenuScript menuScript;
    public FadeOutScript whiteScreen;
    int index;
    bool canPressVert;
    string currentPasscode;
    bool passcodeMenu;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        currentPasscode = "";
    }

    // Update is called once per frame
    void Update()
    {
        whiteScreen.GetComponent<SpriteRenderer>().sortingOrder = -1;

        if (passcodeMenu){
            if (whiteScreen.GetComponent<SpriteRenderer>().color.a >= 1f){
                whiteScreen.fadeOut = true;
                passcodeMenu = false;
                index = 0;
                menuScript.PasscodeMenuReturn();
            }
            return;
        }

        for (int i = 0; i < bugIcons.Length; i++){
            if (i == index){
                bugIcons[i].SetActive(true);
            } else {
                bugIcons[i].SetActive(false);
            }
            bugIcons[i].transform.Rotate(0f, 0f, Time.deltaTime*-150f);
        }

        if (Input.GetAxis("Vertical") == 0f){
            canPressVert = true;
        }
        if (canPressVert && Input.GetAxis("Vertical") > 0){
            AudioHandlerScript.PlaySound("MenuScroll", 1f);
            canPressVert = false;
            index--;
            if (index < 0){
                index = bugIcons.Length-1;
            }
        }
        if (canPressVert && Input.GetAxis("Vertical") < 0){
            AudioHandlerScript.PlaySound("MenuScroll", 1f);
            canPressVert = false;
            index++;
            if (index >= bugIcons.Length){
                index = 0;
            }
        }

        typeText.text = currentPasscode + "|";

        if (index == 0){
            if (Input.GetKeyDown(KeyCode.Backspace) && currentPasscode.Length > 0) {
                AudioHandlerScript.PlaySound("MenuScroll", 1f);
                currentPasscode = currentPasscode.Substring(0,currentPasscode.Length-1);
                incorrectLabel.SetActive(false);
            }
        } else if (index == 1){
            if (Input.GetButtonDown("Fire1")){
                AudioHandlerScript.PlaySound("MenuSelect", 1f);
                if (!PasscodeHandler.LoadFromPasscode(currentPasscode)){
                    incorrectLabel.SetActive(true);
                }
            }
        } else if (index == 2){
            if (Input.GetButtonDown("Fire1")){
                AudioHandlerScript.PlaySound("MenuSelect", 1f);
                incorrectLabel.SetActive(false);
                whiteScreen.fadeOut = false;
                passcodeMenu = true;
            }
        }
    }

    void OnGUI(){
        if (index == 0){
            Event e = Event.current;
            if (currentPasscode.Length < 13 && e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1 && char.IsLetter(e.keyCode.ToString()[0])) {
                AudioHandlerScript.PlaySound("MenuScroll", 1f);
                currentPasscode += char.ToUpper(e.keyCode.ToString()[0]);
                incorrectLabel.SetActive(false);
            }
        }
    }
}
