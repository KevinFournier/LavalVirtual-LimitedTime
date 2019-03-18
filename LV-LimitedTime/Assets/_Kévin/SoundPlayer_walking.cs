using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer_walking : SoundPlayer
{

	private void Start() {
		

		Collider col = GetComponent<Collider>();
		if(col != null) {
			col.isTrigger = true;
		}
	}

	

	private void OnTriggerEnter(Collider other) {

		if (other.tag == "LeftFoot") {
			playSound(1);
		}else if (other.tag == "RightFoot") {
			playSound(2);
		}

	}
}
