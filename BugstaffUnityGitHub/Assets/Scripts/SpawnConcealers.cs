using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConcealers : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public GameObject concealerPrefab;
    float scale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        for (float i = minX; i < maxX; i += scale){
            for (float k = minY; k < maxY; k += scale){
                GameObject go = Instantiate<GameObject>(concealerPrefab);
                go.transform.position = new Vector3(i,k,0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
