using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Seb_Steam_VR_v2_2_Input : MonoBehaviour {

	[Header("Assignation Automatique")]
	public bool AutoAssign = true;
	public SteamVR_Input_Sources InputSource;

	[Header("Inputs")]
	public bool Menu;
	public bool MenuUp;
	public bool MenuDown;
	[Space(10)]
	public bool Trigger;
	public bool TriggerUp;
	public bool TriggerDown;
	public float Squeeze;
	[Space(10)]
	public bool TouchPad;
	public bool TouchPadUp;
	public bool TouchPadDown;
	[Space(10)]
	public bool TouchClic;
	public bool TouchClicUp;
	public bool TouchClicDown;
	[Space(10)]
	public bool Grip;
	public bool GripUp;
	public bool GripDown;
	[Space(10)]
	public Vector2 TouchPos;

	// Use this for initialization
	void Start () {

		if (AutoAssign && GetComponent<SteamVR_Behaviour_Pose>()) {
			InputSource = GetComponent<SteamVR_Behaviour_Pose>().inputSource;
		}
	}
	
	// Update is called once per frame
	void Update () {

		MenuDown = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Menu").GetStateDown(InputSource);
		Menu = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Menu").GetState(InputSource);
		MenuUp = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Menu").GetStateUp(InputSource);

		TriggerDown = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch").GetStateDown(InputSource);
		Trigger = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch").GetState(InputSource);
		TriggerUp = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch").GetStateUp(InputSource);

		TouchPadDown = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("TouchPad").GetStateDown(InputSource);
		TouchPad = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("TouchPad").GetState(InputSource);
		TouchPadUp = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("TouchPad").GetStateUp(InputSource);

		TouchClicDown = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport").GetStateDown(InputSource);
		TouchClic = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport").GetState(InputSource);
		TouchClicUp = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport").GetStateUp(InputSource);

		GripDown = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip").GetStateDown(InputSource);
		Grip = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip").GetState(InputSource);
		GripUp = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip").GetStateUp(InputSource);



		Squeeze = SteamVR_Input.GetSingleAction("Squeeze").GetAxis(InputSource);
		//Squeeze = (float)System.Math.Round(Squeeze , 2);
		if(Squeeze <= 0.05f) { Squeeze = 0; }

		TouchPos = SteamVR_Input.GetVector2Action("TouchPos").GetAxis(InputSource);
		//TouchPos = new Vector2 ((float)System.Math.Round(TouchPos.x , 2) , (float)System.Math.Round(TouchPos.y , 2) );

		/*
		if (Menu) print("Menu");
		if (MenuUp) print("MenuUp");
		if (MenuDown) print("MenuDown");
		if (Trigger) print("Trigger");
		if (TriggerUp) print("TriggerUp");
		if (TriggerDown) print("TriggerDown");
		if (TouchPad) print("TouchPad");
		if (TouchPadUp) print("TouchPadUp");
		if (TouchPadDown) print("TouchPadDown");
		if (TouchClic) print("TouchClic");
		if (TouchClicUp) print("TouchClicUp");
		if (TouchClicDown) print("TouchClicDown");
		if (Grip) print("Grip");
		if (GripUp) print("GripUp");
		if (GripDown) print("GripDown");
		*/



	}
	/*
	void print(string info) {
		Debug.Log(info);
	}
	*/
}
