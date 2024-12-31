using UnityEngine;

using Whilefun.FPEKit;

//
// DemoIdolScript
//
// Copyright 2021 While Fun Games
// http://whilefun.com
//
public class SKPLIdolScript : MonoBehaviour {

    private SKPLIdolTrapScript theTrap;
    private bool pickedUpOnce = false;

	void Awake()
    {
        theTrap = GameObject.FindObjectOfType<SKPLIdolTrapScript>();
	}
    
    public void idolPickupEvent()
    {
        if (!pickedUpOnce && theTrap)
        {
            pickedUpOnce = true;
            theTrap.idolPickedUp();
        }
    }

    public void idolReturnEvent()
    {
        gameObject.GetComponent<FPEInteractablePickupScript>().interactionString = "将物品归位了，专题厅开启";
    }
    
}
