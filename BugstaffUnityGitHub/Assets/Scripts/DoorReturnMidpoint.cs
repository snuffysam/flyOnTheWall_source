using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorReturnMidpoint : DoorScript
{
    // Start is called before the first frame update
    void Start()
    {
        sceneToLoad = MissionManager.GetMissionReturn();
        xPosLoad = MissionManager.GetReturnX();
        yPosLoad = MissionManager.GetReturnY();
        loadDirection = MissionManager.GetReturnFlip();
    }
}
