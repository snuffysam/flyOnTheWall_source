using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightModeToggle : MonoBehaviour
{
    public static bool nightMode = false;
    bool prevActivated;

    int count = 5;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = nightMode;
        prevActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("NightMode") && !SceneManager.GetActiveScene().name.Equals("TitleScreen")){
            nightMode = !nightMode;
        }

        GetComponent<MeshRenderer>().enabled = nightMode;

        if (!prevActivated){
            prevActivated = true;
            SpriteRenderer[] cs = FindObjectsOfType<SpriteRenderer>();
            foreach (SpriteRenderer spr in cs){
                if (spr.GetComponent<ConcealerScript>() == null){
                    spr.sortingOrder--;
                }
            }
        }
        prevActivated = nightMode;

        if (count > 0){
            count--;
            //delete audio listeners
            AudioListener[] als = FindObjectsOfType<AudioListener>();
            for (int i = 0; i < als.Length; i++){
                als[i].enabled = true;
                //Destroy(als[i]);
            }
            //delete audio listeners
            /*AudioSource[] aus = FindObjectsOfType<AudioSource>();
            for (int i = 0; i < aus.Length; i++){
                aus[i].enabled = false;
                Destroy(aus[i]);
            }*/
        }
    }
}
