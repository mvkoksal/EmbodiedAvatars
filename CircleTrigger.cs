using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTrigger : MonoBehaviour
{
    public GameObject mirror;
    public GameObject canvas;

    public float secondsToWait = 10.0f;
    private bool mirrorUsed = false;

    // Show the mirror for a certain amount of time, then switch to the DSCanvas
    private IEnumerator MirrorWaitTime()
    {
        if (!mirrorUsed)
        {
            mirror.SetActive(true);
            yield return new WaitForSeconds(secondsToWait);
            mirror.SetActive(false);
            canvas.SetActive(true);
            mirrorUsed = true;
        }
    }

    // Detect when the avatar stands on the circle trigger
    private void OnTriggerEnter(Collider other)
    {
        if ((other.name == "LeftFoot") || (other.name == "RightFoot"))
        {
            StartCoroutine(MirrorWaitTime());
        }
    }

    // Turn off the mirror when the avatar steps off the circle trigger
    private void OnTriggerExit(Collider other)
    {
        mirror.SetActive(false);
    }
}
