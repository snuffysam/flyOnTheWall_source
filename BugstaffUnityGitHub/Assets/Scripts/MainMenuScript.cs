using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string levelToLoad;
    public FadeOutScript creditsPage;
    public Vector3[] positions;
    int index;
    bool canPressVert;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        canPressVert = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") == 0f){
            canPressVert = true;
        }
        if (canPressVert && Input.GetAxis("Vertical") > 0){
            canPressVert = false;
            index--;
            if (index < 0){
                index = positions.Length-1;
            }
        }
        if (canPressVert && Input.GetAxis("Vertical") < 0){
            canPressVert = false;
            index++;
            if (index >= positions.Length){
                index = 0;
            }
        }

        transform.position = positions[index];
        transform.Rotate(0f, 0f, Time.deltaTime*-150f);

        if (Input.GetButtonDown("Fire1") && creditsPage.fadeOut){
            if (index == 0){
                SceneManager.LoadScene(levelToLoad);
            } else if (index == 1){

            } else if (index == 2){
                NightModeToggle.nightMode = !NightModeToggle.nightMode;
            } else if (index == 3){
                creditsPage.fadeOut = false;
            }
        } else if (Input.GetButtonDown("Fire1") && !creditsPage.fadeOut){
            creditsPage.fadeOut = true;
        }
    }
}
