﻿using UnityEngine;
using UnityEditor;
using System.Collections;

public class WaterPlatformController : MonoBehaviour
{
    public GameObject myPlatform;
    public int nbOfSteps;
    public string axisToAnimate; // x, y, z
    int axisId;
    int currStep = 0;
    public int forwardOrBackward = 1; // 1 = forward, -1 = backward
    CharacterController myPlayer;

    // Animations
    Animator anim;
    Animation myAnimation;
    AnimationCurve curve;
    AnimationClip clip;
    bool animationIsEnded = true;

    private void Start()
    {
        myPlayer = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
        myAnimation = myPlatform.GetComponent<Animation>();
        anim = myPlatform.GetComponent<Animator>();
        switch (axisToAnimate)
        {
            case "x":
                axisId = 0;
                break;
            case "y":
                axisId = 1;
                break;
            case "z":
                axisId = 2;
                break;
            default:
                axisId = 0;
                break;
        }

    }

    private void Update()
    {
        if (myAnimation.isPlaying)
        {
            animationIsEnded = false;
        }
        else
        {
            if (!animationIsEnded)
            {
                animationEnd();
            }
            animationIsEnded = true;
        }
    }
    void changeAnimation()
    {
        // Block player
        myPlayer.enabled = false;

        if (currStep >= nbOfSteps)
        {
            currStep = 0;
            forwardOrBackward *= -1;
        }
        if (currStep < nbOfSteps)
        {
            Debug.Log("Play:" + currStep);

            // Create custom animation
            AnimationClip clip = new AnimationClip();
            clip.name = "Anim";
            //clip = myAnimation.clip;
            clip.legacy = true;

            Keyframe[] keys;
            keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, myPlatform.transform.position[axisId]);
            keys[1] = new Keyframe(1.0f, myPlatform.transform.position[axisId] + (1.0f * forwardOrBackward));
            curve = new AnimationCurve(keys);
            clip.SetCurve("", typeof(Transform), "localPosition." + axisToAnimate, curve);
            Debug.Log("localPosition." + axisToAnimate);

            // Add event when animation end
            // new event created 
            /*
            AnimationEvent evt;
            evt = new AnimationEvent();
            //AnimationEvent[] listOfEvts = new AnimationEvent[1];
            //listOfEvts[0] = evt;

            evt.time = clip.length/2; // because animation lasts 1s
            evt.functionName = "animationEnd";
            clip.AddEvent(evt);
            */
            //clip.events = listOfEvts;
            //AnimationUtility.SetAnimationEvents(clip, listOfEvts);

            myAnimation.AddClip(clip, clip.name);
            //Debug.Log(myAnimation["Anim"].clip.events.Length);
            // Play custom animation
            myAnimation.Play(clip.name);
            //yield return new WaitForSeconds(1.0f);
            //animationIsEnded = false;
            //wait();
            //animationEnd();
        }

        ++currStep;
    }

    void animationEnd()
    {
        Debug.Log("animation end");
        // free player
        myPlayer.enabled = true;
        // allow new water
        //animationIsEnded = true;
    }

    public void resetPosition()
    {
        Debug.Log("reset");

        // Create reset animation (one frame)
        AnimationClip clip = new AnimationClip();
        //clip = myAnimation.clip;
        clip.legacy = true;

        Keyframe[] keys;
        keys = new Keyframe[1];
        keys[0] = new Keyframe(0.0f, 0.0f);
        curve = new AnimationCurve(keys);
        clip.SetCurve("", typeof(Transform), "localPosition." + axisToAnimate, curve);

        // Play custom animation
        myAnimation.AddClip(clip, clip.name);
        myAnimation.Play(clip.name);

        currStep = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water") && animationIsEnded)
        {
            changeAnimation();
            Destroy(other.gameObject);
            
        }
    }
}
