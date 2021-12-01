using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public ButtonActivateScript target;
    public bool onlyActivateOnce;
    public Sprite deactivateSprite;
    public Sprite activateSprite;
    public bool destroyBug;
    bool alreadyActivated;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((!alreadyActivated || !onlyActivateOnce) && GetComponentInChildren<BugShotScript>() != null){
            alreadyActivated = true;
            if (destroyBug){
                BugShotScript[] bss = GetComponentsInChildren<BugShotScript>();
                for (int i = 0; i < bss.Length; i++){
                    Destroy(bss[i].gameObject);
                }
                alreadyActivated = false;
            }
            AudioHandlerScript.PlayClipAtPoint("ButtonClick", "ButtonClick", 1f, transform.position);
            target.ExecuteButton();
        }
        if (alreadyActivated){
            GetComponent<SpriteRenderer>().sprite = activateSprite;
        } else {
            GetComponent<SpriteRenderer>().sprite = deactivateSprite;
        }
    }
}
