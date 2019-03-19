using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : SoundPlayer
{
    public Texture2D map;

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
        if (!other.CompareTag(Tags.Ground))
            return;

        var x = transform.position.x / 500f * 512f;
        var y = transform.position.z / 500f * 512f;

        Color zoneColor = map.GetPixel(Mathf.RoundToInt(x), Mathf.RoundToInt(y));

        print(zoneColor);
        if (zoneColor.r > zoneColor.g)
            PlaySound(1);
        else
            PlaySound(0);
    }
}
