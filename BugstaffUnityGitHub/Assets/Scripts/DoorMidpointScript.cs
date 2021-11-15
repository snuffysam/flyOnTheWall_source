using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMidpointScript : DoorScript
{
    // Start is called before the first frame update
    void Start()
    {
        sceneToLoad = MissionManager.GetMidpointScene();
        xPosLoad = MissionManager.GetMidpointX();
        yPosLoad = MissionManager.GetMidpointY();
        loadDirection = MissionManager.GetMidpointFlip();
    }
}
