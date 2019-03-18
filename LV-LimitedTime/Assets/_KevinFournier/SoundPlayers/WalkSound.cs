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
        print(other);

        if (other.CompareTag(Tags.Ground))
        {
            print(other.GetComponent<Ground>().Type);
            PlaySound((int)other.GetComponent<Ground>().Type);
        }
    }
}
