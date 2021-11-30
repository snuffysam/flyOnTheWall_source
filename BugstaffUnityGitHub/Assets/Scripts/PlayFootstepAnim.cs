using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class allows an audio clip to be played during an animation state.
/// </summary>
public class PlayFootstepAnim : StateMachineBehaviour
{
    /// <summary>
    /// The point in normalized time where the clip should play.
    /// </summary>
    public float t = 0.5f;
    /// <summary>
    /// If greater than zero, the normalized time will be (normalizedTime % modulus).
    /// This is used to repeat the audio clip when the animation state loops.
    /// </summary>
    public float modulus = 0f;

    /// <summary>
    /// The audio clip to be played.
    /// </summary>
    public string clip;
    public string clipBugvision;
    public float volume = 1f;
    float last_t = -1f;
    bool played = false;
    float tracker = 0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        played = false;
        tracker = 0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var nt = stateInfo.normalizedTime;
        if (last_t >= 0f){
            tracker += nt-last_t;
        }
        if (nt < modulus || tracker < 0f){
            tracker = nt;
        }
        while (modulus > 0f && tracker > modulus){
            played = false;
            tracker -= modulus;
        }
        if (tracker >= t && !played){
            played = true;
            if (clip.Length < 2){
                AudioHandlerScript.PlayFootstep(animator.transform.position);
            } else {
                AudioHandlerScript.PlayClipAtPoint(clip, clipBugvision, volume, animator.transform.position);
            }
        }
        last_t = nt;
    }
}
