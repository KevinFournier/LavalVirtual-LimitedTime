using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tags
{
    public const string Ground = "Ground";
}

public enum Mode { Normal, Blind }



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Mode Mode;

	public Seb_Steam_VR_v2_2_Input inputLeft, inputRight;


	LayerMask everythingCullingMask = -1;
	LayerMask ParticulesStuffCullingMask = (1 << 10);


    public WindZone WindZone;

    private void Awake()
    {
        Instance = this;
    }

	private void Update() {
		if(inputLeft.GripUp || inputRight.GripUp) {
			changeMode();
		}
	}


	void changeMode() {
		if(Mode == Mode.Normal) {
			Camera.main.cullingMask = ParticulesStuffCullingMask;
			Camera.main.clearFlags = CameraClearFlags.SolidColor;
			Mode = Mode.Blind;
		}
		else {
			Camera.main.cullingMask = everythingCullingMask;
			Camera.main.clearFlags = CameraClearFlags.Skybox;
			Mode = Mode.Normal;
		}
	}


}