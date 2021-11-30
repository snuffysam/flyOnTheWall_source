using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class MissionManager : MonoBehaviour
{
    public static int missionNumber = 0;
    public static int missionIndex = 0;
    public static string[] midpointRooms = {"Midpoint1", "Midpoint2", "Midpoint3", "Midpoint4"};
    public static Vector2[] startPoints = {new Vector2(-1.4f,7.5f), new Vector2(-8.77f, -0.28f), new Vector2(-5f, 0.82f), new Vector2(-8.77f, -0.34f)};
    public static bool[] startDirections = {true, false, false, false};
    public static string[] missionStart = {"Palace1", "Airship1", "Casino1", "PalmGemRoom"};
    public static string[] missionReturn = {"Palace4", "Airship3", "Casino4", "Midpoint4"};
    public static string[] charmNames = {"Spice", "Aroma", "Flash", "Palm"};
    public static string[] charmTutorials = {"[Press Down Arrow to use the Spice Charm.]", "[Press Up mid-air to use the Aroma Charm.]", "[Stand still to use the Flash Charm.]", "Palm"};
    public static Vector2[] returnPoints = {new Vector2(9.89f,-0.51f), new Vector2(-1.38f, 0f), new Vector2(-1.31f, 5.15f), Vector2.zero};
    public static bool[] returnDirections = {true, false, true, false};
    public static int completed;
    public static bool beatPalmMission;
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

    public static float GetReturnX(){
        return returnPoints[missionIndex].x;
    }

    public static float GetReturnY(){
        return returnPoints[missionIndex].y;
    }

    public static bool GetReturnFlip(){
        return returnDirections[missionIndex];
    }

    public static string GetMissionStart(){
        return missionStart[missionIndex];
    }

    public static bool IsMissionBeaten(int index){
        byte completedByte = (byte)(completed);
        return (completedByte & (1 << index)) != 0;
    }

    public static void CompleteMission(){
        if (!IsMissionBeaten(missionIndex)){
            completed += (int)(Mathf.Pow(2,missionIndex));
            missionNumber++;
        }
    }
}
