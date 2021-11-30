using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finale_Cut_2 : MonoBehaviour
{
    public GameObject art1;
    public GameObject art2;
    public GameObject art3;
    public GameObject sun;
    public GameObject moon;
    public GameObject greySquare;
    public GameObject whiteSquare;
    public AudioSource musicPlayer;
    public TextboxScript.TextBlock[] textToSend1;
    public TextboxScript.TextBlock[] textToSend2;
    public TextboxScript.TextBlock[] textToSend3;
    public TextboxScript.TextBlock[] textToSend4;
    int mode;
    TextboxScript tbs;
    float delay;
    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        tbs = FindObjectOfType<TextboxScript>();
        if (PasscodeHandler.hardcore){
            textToSend1[4] = new TextboxScript.TextBlock{speakerName = "Oruma", text = "What, because I'm going to be looking up at you? Alena Fliegenmann, the girl in the bikini... that's a bit of a joke, wouldn't you say?", emphasis = false};
        }
        musicPlayer.pitch = AudioHandlerScript.bossMult2;
    }

    // Update is called once per frame
    void Update()
    {
        art1.GetComponent<SpriteRenderer>().sortingOrder = -1;
        art2.GetComponent<SpriteRenderer>().sortingOrder = -1;
        art3.GetComponent<SpriteRenderer>().sortingOrder = -1;
        sun.GetComponent<SpriteRenderer>().sortingOrder = -1;
        moon.GetComponent<SpriteRenderer>().sortingOrder = -1;
        greySquare.GetComponent<SpriteRenderer>().sortingOrder = -1;
        whiteSquare.GetComponent<SpriteRenderer>().sortingOrder = -1;
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
                foreach (TextboxScript.TextBlock textBlock in textToSend2){
                    tbs.AddTextBlock(textBlock);
                }
                art1.SetActive(false);
                art2.SetActive(true);
                sun.SetActive(true);
                moon.SetActive(true);
                greySquare.SetActive(true);
                mode = 2;
            }
        } else if (mode == 2){
            if (tbs.IsEmpty()){
                mode = 3;
                delay = 0f;
            }
        } else if (mode == 3){
            art2.transform.position += (Vector3.right*1.5f+Vector3.up*-1f).normalized*Time.deltaTime*1.5f;
            moon.transform.position += Vector3.right*-1f*Time.deltaTime*0.05f;

            delay += Time.deltaTime;
            art2.transform.eulerAngles = new Vector3(0f, 0f, -delay*1.5f);
            sun.transform.eulerAngles = new Vector3(0f, 0f, -delay*1.5f);

            greySquare.GetComponent<FadeOutScript>().enabled = true;

            if (moon.transform.position.x < sun.transform.position.x){
                foreach (TextboxScript.TextBlock textBlock in textToSend3){
                    tbs.AddTextBlock(textBlock);
                }
                art1.SetActive(true);
                art2.SetActive(false);
                sun.SetActive(false);
                moon.SetActive(false);
                delay = 0f;
                mode = 4;
            }
        } else if (mode == 4){
            if (tbs.IsEmpty()){
                art1.SetActive(false);
                art3.SetActive(true);
                delay += Time.deltaTime;
                if (delay > 1f){
                    delay = 0f;
                    mode = 5;
                    art3.GetComponent<OscillateScript>().enabled = false;
                    art3.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                    greySquare.GetComponent<FadeOutScript>().fadeOut = true;
                    greySquare.GetComponent<FadeOutScript>().fadeSpeed = 0.3f;
                    musicPlayer.Stop();
                }
            }
        } else if (mode == 5){
            delay += Time.deltaTime;
            if (delay > 4f){
                foreach (TextboxScript.TextBlock textBlock in textToSend4){
                    tbs.AddTextBlock(textBlock);
                }
                mode = 6;
            }
        } else if (mode == 6){
            if (tbs.IsEmpty()){
                SceneManager.LoadScene("Finale5");
            }
        }
    }
}
