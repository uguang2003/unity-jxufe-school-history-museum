using UnityEngine;
using System.Collections;

using Whilefun.FPEKit;
using System;

//
// DemoIdolTrapScript
//
// Copyright 2021 While Fun Games
// http://whilefun.com
//
public class SKPLIdolTrapScript : FPEGenericSaveableGameObject {

	[Header("Custom Idol Items")]
	public Material lightOn;
	public Material lightOff;
	public AudioClip stoneScrape;
	public AudioClip trapStartSound;
	public AudioClip trapReleaseSound;

	private GameObject trapTriggerPlate = null;
	private GameObject trapBars = null;
	private Vector3 signPosition = Vector3.zero;
	private Vector3 plateDownPosition = Vector3.zero;
	private Vector3 releasedBarsPosition = Vector3.zero;
	private Vector3 barsLockedPosition = Vector3.zero;

    private enum eTrapState
    {
        IDLE = 0,
        PLATE_MOVING,
        SIGN_LAUGH,
        BARS_RELEASE,
        COMPLETE
    }
    private eTrapState currentTrapState = eTrapState.IDLE;
    private float trapStateCountdown = 0.0f;

	void Awake(){


		trapTriggerPlate = GameObject.Find("TrapTriggerPlate");
		trapBars = GameObject.Find("TrapBars");

        plateDownPosition = trapTriggerPlate.transform.position;
        plateDownPosition.y -= 0.1f;

		barsLockedPosition = trapBars.transform.position;
		releasedBarsPosition = trapBars.transform.position;
		releasedBarsPosition.y -= 3.0f;

		trapBars.transform.position = releasedBarsPosition;

	}

    // This update function handles the base Update call, and does some other fancy custom state and event stuff for the idol
    void Update()
    {


        if (currentTrapState == eTrapState.PLATE_MOVING)
        {

            trapTriggerPlate.transform.position = Vector3.Lerp(trapTriggerPlate.transform.position, plateDownPosition, 2f * Time.deltaTime);
            trapStateCountdown -= Time.deltaTime;

            if (trapStateCountdown <= 0.0f)
            {
                MoveToState(eTrapState.SIGN_LAUGH);
            }

        }
        else if (currentTrapState == eTrapState.SIGN_LAUGH)
        {
            trapStateCountdown -= Time.deltaTime;

            if (trapStateCountdown <= 0.0f)
            {
                MoveToState(eTrapState.BARS_RELEASE);
            }

        }
        //else if (currentTrapState == eTrapState.BARS_RELEASE)
        //{

        //    trapBars.transform.position = Vector3.Lerp(trapBars.transform.position, releasedBarsPosition, 2f * Time.deltaTime);
            
        //    trapStateCountdown -= Time.deltaTime;

        //    if (trapStateCountdown <= 0.0f)
        //    {
        //        MoveToState(eTrapState.COMPLETE);
        //    }

        //}

	}
	
    public void idolPickedUp()
    {

        // When the idol is picked up, let's set off a trap
        if (currentTrapState == eTrapState.IDLE)
        {
            MoveToState(eTrapState.PLATE_MOVING);

        }

    }

    private void setBoxColliderState(bool collidersEnabled)
    {

        BoxCollider[] childColliders = trapBars.GetComponentsInChildren<BoxCollider>();

        foreach(BoxCollider bc in childColliders)
        {
            bc.enabled = collidersEnabled;
        }

    }

    public void idolReturnEvent()
    {
        gameObject.GetComponent<FPEInteractablePickupScript>().interactionString = "将物品归位了，专题厅开启";
    }

    private void MoveToState(eTrapState state)
    {

        currentTrapState = state;

        switch (state)
        {

            case eTrapState.IDLE:
                break;

            case eTrapState.PLATE_MOVING:
                trapBars.transform.position = barsLockedPosition;
                //trapBars.GetComponent<BoxCollider>().enabled = true;
                setBoxColliderState(true);
                trapStateCountdown = 2.5f;
                trapTriggerPlate.GetComponent<AudioSource>().clip = stoneScrape;
                trapTriggerPlate.GetComponent<AudioSource>().Play();
                break;

            case eTrapState.SIGN_LAUGH:
                trapBars.transform.position = barsLockedPosition;
                //trapBars.GetComponent<BoxCollider>().enabled = true;
                setBoxColliderState(true);
                trapTriggerPlate.transform.position = plateDownPosition;
                trapStateCountdown = 3.0f;
                trapTriggerPlate.GetComponent<AudioSource>().clip = trapStartSound;
                trapTriggerPlate.GetComponent<AudioSource>().Play();
                break;

            case eTrapState.BARS_RELEASE:
                trapBars.transform.position = barsLockedPosition;
                //trapBars.GetComponent<BoxCollider>().enabled = true;
                setBoxColliderState(true);
                trapStateCountdown = 2.0f;
                trapTriggerPlate.GetComponent<AudioSource>().clip = trapReleaseSound;
                trapTriggerPlate.GetComponent<AudioSource>().Play();
                break;

            case eTrapState.COMPLETE:
                //trapBars.GetComponent<BoxCollider>().enabled = false;
                setBoxColliderState(false);
                trapBars.transform.position = releasedBarsPosition;
                break;

            default:
                Debug.LogError("DemoIdolScript.MoveToState():: Given bad state '"+ state + "'.");
                break;

        }

    }

    public override FPEGenericObjectSaveData getSaveGameData()
    {
        return new FPEGenericObjectSaveData(gameObject.name, (int)currentTrapState, trapStateCountdown, false);
    }

    public override void restoreSaveGameData(FPEGenericObjectSaveData data)
    {
        MoveToState((eTrapState)data.SavedInt);
    }

}
