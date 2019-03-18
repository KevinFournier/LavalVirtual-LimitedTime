using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveTrackerManager : MonoBehaviour {

	[Header("Vive Devices")]
	[ContextMenuItem("AutoConfig", "AutoConfig")]

	public List<Transform> ViveTrackersList = new List<Transform>();

	public bool AllowAutoDetection = true;

	// Use this for initialization
	void Start() {
		Invoke("stopAllowDetection", 5);

		if (AllowAutoDetection)
			AutoSetTrackersID();
	}

	void AutoSetTrackersID() {


		//print("ViveTrackersList.Count = " + ViveTrackersList.Count);
		
		if (ViveTrackersList.Count > 0) {
			uint index = 0;
			var error = ETrackedPropertyError.TrackedProp_Success;

			int ViveTrackersListIndex = 0;
			// Extraction des trackers connectés et de leurs ID
			for (uint i = 0 ; i < 12 ; i++) {
				index = i;
				System.Text.StringBuilder result = new System.Text.StringBuilder((int)64);
				OpenVR.System.GetStringTrackedDeviceProperty(index, ETrackedDeviceProperty.Prop_RenderModelName_String, result, 64, ref error);

				//print(result.ToString() + " + index: " + index);

				if (result.ToString().Contains("tracker")) {
					//print("---TRACKER--- : ViveTrackersListIndex : " + ViveTrackersListIndex);
					ViveTrackersList[ViveTrackersListIndex].GetComponent<SteamVR_TrackedObject>().index = (SteamVR_TrackedObject.EIndex)index;
					ViveTrackersListIndex++;
				}
			}
		}
	}

	void stopAllowDetection() {
		AllowAutoDetection = false;
	}
}
