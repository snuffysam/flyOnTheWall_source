using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightModeToggle : MonoBehaviour
{
    public static bool nightMode = false;
    bool prevActivated;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = nightMode;
        prevActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("NightMode")){
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
    }
}
