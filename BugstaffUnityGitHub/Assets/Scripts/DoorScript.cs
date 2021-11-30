using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Platformer.Mechanics;

public class DoorScript : MonoBehaviour
{
    public string sceneToLoad;
    public float xPosLoad;
    public float yPosLoad;
    public bool loadDirection;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")){
            //Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<Collider2D>());
            GameObject go = new GameObject();
            go.AddComponent<DoorHelper>();
            go.GetComponent<DoorHelper>().xPosLoad = xPosLoad;
            go.GetComponent<DoorHelper>().yPosLoad = yPosLoad;
            go.GetComponent<DoorHelper>().loadDirection = loadDirection;
            go.GetComponent<DoorHelper>().loadedScene = sceneToLoad;
            AudioHandlerScript.PlayClipAtPoint("DoorClosing", "DoorClosing", 1f, transform.position);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
