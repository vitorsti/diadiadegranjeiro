using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDelay : MonoBehaviour
{
    Animator myAnimator;
    float waitSeconds;
    // Start is called before the first frame update
    void Awake()
    {

        myAnimator = GetComponent<Animator>();
        myAnimator.enabled = false;
        //waitSeconds = Random.value;
        waitSeconds = Random.Range(0.5f, 2f);
    }

    void Start()
    {
        StartCoroutine(AnimatorStartAfterSeconds());
    }

    IEnumerator AnimatorStartAfterSeconds()
    {

        yield return new WaitForSeconds(waitSeconds);
        myAnimator.speed = waitSeconds;
        myAnimator.enabled = true;
    }

}
