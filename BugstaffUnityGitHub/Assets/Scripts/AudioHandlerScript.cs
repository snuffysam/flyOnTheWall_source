using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandlerScript : MonoBehaviour
{
    public AudioItem[] clips;
    public static Dictionary<string, AudioClip> clipsRef;
    public static List<GameObject> currentClips;
    public static int maxClips = 15;
    public static float sfxVolume = 0.5f;
    public static GameObject dialoguePlayer;
    public static float bossMult = 1.189f;
    public static float bossMult2 = 1.335f;
    // Start is called before the first frame update
    void Start()
    {
        if (clipsRef == null){
            clipsRef = new Dictionary<string, AudioClip>();
            foreach (AudioItem ai in clips){
                clipsRef.Add(ai.key, ai.audio);
            }
            currentClips = new List<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string key, float volume){
        if (!clipsRef.ContainsKey(key)){
            return;
        }
        if (key.Contains("Computer")){
            Debug.Log("found key: " + key);
        }

        float vol = volume*sfxVolume;
        if (vol <= 0f){
            return;
        }
        if (vol > 1f){
            vol = 1f;
        }

        for (int i = 0; i < currentClips.Count; i++){
            if (currentClips[i] != null && !currentClips[i].GetComponent<AudioSource>().isPlaying){
                Destroy(currentClips[i]);
                currentClips[i] = null;
            }
            if (currentClips[i] == null){
                currentClips.RemoveAt(i);
                i--;
            }
        }
        while (currentClips.Count > maxClips-1){
            Destroy(currentClips[0]);
            currentClips.RemoveAt(0);
        }

        GameObject go = new GameObject();
        AudioSource audioSource = go.AddComponent<AudioSource>();
        if (key.Contains("Computer")){
            Debug.Log("playing key: " + key);
        }
        audioSource.PlayOneShot(clipsRef[key],vol);
    }

    public static void PlayDialogue(string speaker, bool emphasis){
        if (dialoguePlayer == null){
            dialoguePlayer = new GameObject();
            dialoguePlayer.AddComponent<AudioSource>();
        }
        AudioSource audioSource = dialoguePlayer.GetComponent<AudioSource>();
        if (emphasis){
            audioSource.volume = sfxVolume*1.75f;
        } else {
            audioSource.volume = sfxVolume*0.85f;
        }
        audioSource.loop = true;

        if (speaker.Equals("Alena")){
            audioSource.clip = clipsRef["AlenaDialogue"];
        } else if (speaker.Contains("Boss") || speaker.Equals("Oruma") || speaker.Equals("Emittens") || speaker.Equals("Mixologist") || speaker.Equals("Computer") || speaker.Length < 2){
            audioSource.clip = clipsRef["BigBossDialogue"];
        } else if (speaker.Contains("Guard") || speaker.Equals("Arva") || speaker.Equals("War Advisor") || speaker.Equals("Hercules")){
            audioSource.clip = clipsRef["Enemy1Dialogue"];
        } else if (speaker.Equals("Silque") || speaker.Equals("Blatella") || speaker.Equals("Angry Patron")){
            audioSource.clip = clipsRef["Enemy3Dialogue"];
        } else if (speaker.Equals("Driver") || speaker.Equals("Intel Advisor") || speaker.Equals("Perri") || speaker.Equals("Red Admiral") || speaker.Equals("Vanessa")){
            audioSource.clip = clipsRef["Enemy4Dialogue"];
        } else {
            audioSource.clip = clipsRef["Enemy2Dialogue"];
        }

        audioSource.Play();
    }

    public static void PlayFootstep(Vector3 location){
        int n = UnityEngine.Random.Range(1,9);
        
        FollowScript minListen = GetNearestListener(location);
        float volMult = GetVolumeScale(minListen, location);
        if (volMult <= 0f){
            return;
        }

        string str = "EnemyFootsteps";
        if (minListen.go.GetComponent<BugShotScript>() != null){
            str += "Bugvision";
            volMult *= 0.7f;
        }
        str += "" + n;

        PlaySound(str, volMult);
    }

    public static void PlayClipAtPoint(string clipNormal, string clipBugvision, float volume, Vector3 location){
        if (clipNormal.Contains("Computer")){
            Debug.Log(clipNormal + " looking for volume");
        }

        FollowScript minListen = GetNearestListener(location);
        float volMult = GetVolumeScale(minListen, location);
        if (volMult <= 0f){
            return;
        }

        string str = clipNormal;
        if (minListen.go.GetComponent<BugShotScript>() != null){
            str = clipBugvision;
        }

        if (str.Contains("Computer")){
            Debug.Log(str + " about to play");
        }

        PlaySound(str, volMult*volume);
    }

    public static FollowScript GetNearestListener(Vector3 location){
        FollowScript[] listeners = FindObjectsOfType<FollowScript>();
        if (listeners.Length <= 0){
            return null;
        }
        float minDist = 99999f;
        FollowScript minListen = null;
        foreach (FollowScript fs in listeners){
            float mag = (location-fs.transform.position).magnitude/Mathf.Abs(fs.transform.localScale.x);
            if (mag < minDist){
                minDist = mag;
                minListen = fs;
            }
        }
        return minListen;
    }

    public static float GetVolumeScale(Vector3 location){
        return GetVolumeScale(GetNearestListener(location), location);
    }

    public static float GetVolumeScale(FollowScript minListen, Vector3 location){

        if (minListen == null){
            return 0f;
        }

        float minDist = (location-minListen.transform.position).magnitude;
        float mRange = Mathf.Abs(minListen.transform.localScale.x*2f);
        float volMult = (mRange-minDist)/mRange;
        if (minDist > mRange){
            volMult = 0f;
        } else if (minDist < 0f){
            volMult = 1f;
        }
        return volMult;
    }

    public static void StopDialogue(){
        if (dialoguePlayer != null){
            dialoguePlayer.GetComponent<AudioSource>().Stop();
        }
    }

    [Serializable]
    public struct AudioItem
    {
        public string key;
        public AudioClip audio;
    }
}
