using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platformer.Mechanics;

public class TextboxScript : MonoBehaviour
{
    public GameObject dialogueName, dialogueText, emphasizedText;
    Queue<TextBlock> blocks;
    int mode = 0;
    float letterTimer = 0f;
    float maxLetterTimer = 1f/30f;
    float shakeTimer = 0f;
    float maxShakeTimer = 1f/50f;
    float totalShakeTimer = 0f;
    float totalShakeMaxTimer = 0.33f;
    Vector2 startPos;
    private PlayerController playerController;
    bool resetControls = false;
    // Start is called before the first frame update
    void Start()
    {
        blocks = new Queue<TextBlock>();
        startPos = GetComponent<RectTransform>().anchoredPosition;
        playerController = FindObjectOfType<PlayerController>();

        //AddTextBlock(new TextBlock{speakerName = "Alena", text = "I can't believe I ate the whole thing.", emphasis = false});
        //AddTextBlock(new TextBlock{speakerName = "Alena", text = "Seriously?!", emphasis = true});
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 0){
            if (GetComponent<RectTransform>().localScale.x > 0f){
                Vector3 tmp = GetComponent<RectTransform>().localScale;
                tmp.x = tmp.x-Time.deltaTime*3f;
                GetComponent<RectTransform>().localScale = tmp;
            } else {
                if (!resetControls && playerController != null){
                    resetControls = true;
                    playerController.controlEnabled = true;
                }
                Vector3 tmp = GetComponent<RectTransform>().localScale;
                tmp.x = 0f;
                GetComponent<RectTransform>().localScale = tmp;
            }
            dialogueName.SetActive(false);
            dialogueText.SetActive(false);
            emphasizedText.SetActive(false);
            if (blocks.Count > 0){
                mode = 1;
            }
        } else if (mode == 1){
            if (playerController != null){
                playerController.controlEnabled = false;
            }
            if (GetComponent<RectTransform>().localScale.x < 1f){
                Vector3 tmp = GetComponent<RectTransform>().localScale;
                tmp.x = tmp.x+Time.deltaTime*3f;
                GetComponent<RectTransform>().localScale = tmp;
            } else {
                Vector3 tmp = GetComponent<RectTransform>().localScale;
                tmp.x = 1f;
                GetComponent<RectTransform>().localScale = tmp;
                bool emphasize = blocks.Peek().emphasis;
                if (emphasize){
                    mode = 3;
                    dialogueName.SetActive(true);
                    emphasizedText.SetActive(true);
                    dialogueText.SetActive(false);
                    dialogueName.GetComponent<Text>().text = blocks.Peek().speakerName;
                    emphasizedText.GetComponent<Text>().text = blocks.Peek().text;
                    shakeTimer = 0f;
                    totalShakeTimer = 0f;
                } else {
                    mode = 2;
                    dialogueName.SetActive(true);
                    dialogueText.SetActive(true);
                    emphasizedText.SetActive(false);
                    dialogueText.GetComponent<Text>().text = "";
                    dialogueName.GetComponent<Text>().text = blocks.Peek().speakerName;
                    letterTimer = 0f;
                }
            }
        } else if (mode == 2){
            letterTimer += Time.deltaTime;
            string str = blocks.Peek().text;
            if (letterTimer > maxLetterTimer){
                dialogueText.GetComponent<Text>().text += str[dialogueText.GetComponent<Text>().text.Length];
                letterTimer = 0f;
            }
            if (Input.GetButtonDown("Fire1")){
                dialogueText.GetComponent<Text>().text = str;
            }
            if (dialogueText.GetComponent<Text>().text.Length >= str.Length){
                mode = 4;
            }
        } else if (mode == 3){
            shakeTimer += Time.deltaTime;
            totalShakeTimer += Time.deltaTime;
            if (shakeTimer > maxShakeTimer){
                shakeTimer = 0f;
                GetComponent<RectTransform>().anchoredPosition = startPos + new Vector2(UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(-30f, 30f));
            }
            if (Input.GetButtonDown("Fire1") || totalShakeTimer > totalShakeMaxTimer){
                GetComponent<RectTransform>().anchoredPosition = startPos;
                mode = 4;
            }
        } else if (mode == 4){
            if (Input.GetButtonDown("Fire1")){
                blocks.Dequeue();
                if (blocks.Count > 0){
                    mode = 1;
                } else {
                    mode = 0;
                    resetControls = false;
                }
            }
        }
    }

    public bool IsEmpty(){
        return blocks.Count <= 0;
    }

    public void AddTextBlock(TextBlock tb){
        blocks.Enqueue(tb);
    }

    [Serializable]
    public struct TextBlock
    {
        public string speakerName;
        public string text;
        public bool emphasis;
    }
}
