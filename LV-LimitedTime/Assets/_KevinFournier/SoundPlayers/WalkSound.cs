using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : SoundPlayer
{

	private void Start()
    {
        Init();

		Collider col = GetComponent<Collider>();
		if(col != null) {
			col.isTrigger = true;
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Ground))
            PlaySound((int)other.GetComponent<Ground>().Type);
    }
}
