using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapColorNightMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NightModeToggle.nightMode){
            GetComponent<SpriteRenderer>().color = Color.white;
        } else {
            GetComponent<SpriteRenderer>().color = Color.black;
        }
        GetComponent<SpriteRenderer>().color = Color.black;
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        Debug.Log(transform.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, -8f);
    }
}
