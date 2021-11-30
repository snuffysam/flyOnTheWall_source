using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    bool fired;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pos = -9.5f;
        if (transform.position.x < pos && !fired){
            transform.position += Vector3.right*Time.deltaTime;
        } else if (transform.position.x >= pos && !fired){
            GameObject go = Instantiate<GameObject>(bulletPrefab);
            go.transform.position = transform.position + Vector3.right;
            AudioHandlerScript.PlayClipAtPoint("DoorOpening", "DoorOpening", 3f, go.transform.position);
            fired = true;
        } else if (transform.position.x > pos - 1f && fired) {
            transform.position += Vector3.right*Time.deltaTime*-0.75f;
        } else if (transform.position.x <= pos - 1f && fired){
            Destroy(this.gameObject);
        }
    }
}
