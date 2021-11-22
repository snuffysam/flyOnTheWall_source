using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Platformer.Mechanics;

public class DoorHelper : MonoBehaviour
{
    public float xPosLoad;
    public float yPosLoad;
    public bool loadDirection;
    public string loadedScene;
    private int counter = 3;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals(loadedScene)){
        PlayerController[] pcs = FindObjectsOfType<PlayerController>();
        foreach (PlayerController pc in pcs){
            pc.transform.position = new Vector3(xPosLoad, yPosLoad, 0f);
            pc.GetComponent<SpriteRenderer>().flipX = loadDirection;
        }
        counter--;
        if (counter <= 0){
            Destroy(this.gameObject);
        }
        }
    }
}
