using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Platformer.Mechanics;

public class MainMenuScript : MonoBehaviour
{
    public string levelToLoad;
    public FadeOutScript creditsPage;
    public FadeOutScript whiteScreen;
    public GameObject passcodeCanvas;
    public Vector3[] positions;
    int index;
    bool canPressVert;
    bool passcodeMenu;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        canPressVert = false;
        passcodeMenu = false;
        MissionManager.completed = 0;
        MissionManager.missionNumber = 0;
        MissionManager.missionIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        whiteScreen.GetComponent<SpriteRenderer>().sortingOrder = -1;

        if (passcodeMenu){
            if (whiteScreen.GetComponent<SpriteRenderer>().color.a >= 1f){
                whiteScreen.fadeOut = true;
                passcodeCanvas.SetActive(true);
            }
            return;
        }

        if (whiteScreen.fadeOut){
            passcodeCanvas.SetActive(false);
        }

        if (Input.GetAxis("Vertical") == 0f){
            canPressVert = true;
        }
        if (canPressVert && Input.GetAxis("Vertical") > 0){
            AudioHandlerScript.PlaySound("MenuScroll", 1f);
            canPressVert = false;
            index--;
            if (index < 0){
                index = positions.Length-1;
            }
        }
        if (canPressVert && Input.GetAxis("Vertical") < 0){
            AudioHandlerScript.PlaySound("MenuScroll", 1f);
            canPressVert = false;
            index++;
            if (index >= positions.Length){
                index = 0;
            }
        }

        transform.position = positions[index];
        transform.Rotate(0f, 0f, Time.deltaTime*-150f);

        if (Input.GetButtonDown("Fire1") && creditsPage.fadeOut){
            AudioHandlerScript.PlaySound("MenuSelect", 1f);
            if (index == 0){
                Health.ResetHearts();
                PlayerController.ResetPowerups();
                PasscodeHandler.hardcore = false;
                SceneManager.LoadScene(levelToLoad);
            } else if (index == 1){
                whiteScreen.fadeOut = false;
                passcodeMenu = true;
            } else if (index == 2){
                NightModeToggle.nightMode = !NightModeToggle.nightMode;
            } else if (index == 3){
                creditsPage.fadeOut = false;
            }
        } else if (Input.GetButtonDown("Fire1") && !creditsPage.fadeOut){
            AudioHandlerScript.PlaySound("MenuSelect", 1f);
            creditsPage.fadeOut = true;
        }
    }

    public void PasscodeMenuReturn(){
        passcodeMenu = false;
    }
}
