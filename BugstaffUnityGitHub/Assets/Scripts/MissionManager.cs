using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class MissionManager : MonoBehaviour
{
    public static int missionNumber = 0;
    public static int missionIndex = 0;
    public static string[] midpointRooms = {"Midpoint1", "Midpoint2", "Midpoint3", "Midpoint4"};
    public static string[] missionReturn = {"Palace4", "Midpoint2", "Midpoint3", "Midpoint4"};
    public static string[] charmNames = {"Spice", "Aroma", "Flash", "Palm"};
    public static string[] charmTutorials = {"[Press Down Arrow to use the Spice Charm.]", "[Press Up mid-air to use the Aroma Charm.]", "Flash", "Palm"};
    public static Vector2[] startPoints = {new Vector2(-1.4f,7.5f), Vector2.zero, Vector2.zero, Vector2.zero};
    public static bool[] startDirections = {true, true, false, false};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string GetMidpointScene(){
        return midpointRooms[missionNumber];
    }

    public static string GetMissionReturn(){
        return missionReturn[missionIndex];
    }

    public static string GetCharmName(){
        return charmNames[missionIndex];
    }

    public static string GetCharmTutorial(){
        return charmTutorials[missionIndex];
    }

    public static void UpdatePlayerGadgets(){
        if (missionIndex == 0){
            PlayerController.hasPepperSpray = true;
        } else if (missionIndex == 1){
            PlayerController.hasExtraJumps = true;
        } else if (missionIndex == 2){
            PlayerController.hasInvisibility = true;
        } else if (missionIndex == 3){
            PlayerController.hasBackJump = true;
        }
    }

    public static float GetMidpointX(){
        return startPoints[missionNumber].x;
    }

    public static float GetMidpointY(){
        return startPoints[missionNumber].y;
    }

    public static bool GetMidpointFlip(){
        return startDirections[missionNumber];
    }
}
