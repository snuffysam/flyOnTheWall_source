using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanHurt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Hurt(){
        GetComponent<Animator>().SetTrigger("hurt");
        //AudioHandlerScript.PlayClipAtPoint("DoorClosing", "DoorClosing", 0.5f, transform.position);
    }
}
