using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finale_Cut_4 : MonoBehaviour
{
    public GameObject theEndText;
    float delay;
    bool sentText;
    TextboxScript tbs;
    public TextboxScript.TextBlock[] textToSend1;
    bool loadTime;
    // Start is called before the first frame update
    void Start()
    {
        tbs = FindObjectOfType<TextboxScript>();
        sentText = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (tbs == null){
            tbs = FindObjectOfType<TextboxScript>();
        }
        delay += Time.deltaTime;
        if (delay > 3f){
            theEndText.SetActive(true);
            if (tbs.IsEmpty() && sentText){
                delay = 0f;
                loadTime = true;
            }
            if (!sentText && Input.GetButtonDown("Fire1")){
                sentText = true;
                foreach (TextboxScript.TextBlock textBlock in textToSend1){
                    tbs.AddTextBlock(textBlock);
                }
            }
        }
        if (loadTime && delay > 1f){
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
