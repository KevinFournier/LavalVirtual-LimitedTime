using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Foot { Left, Right }

public class WalkSound : SoundPlayer
{
    public Texture2D map;
    public Foot Foot;

	private void Start()
    {
        Init();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tags.Ground))
            return;

        var x = transform.position.x / 500f * 512f;
        var y = transform.position.z / 500f * 512f;

        Color zoneColor = map.GetPixel(Mathf.RoundToInt(x), Mathf.RoundToInt(y));

        if (zoneColor.r > zoneColor.g)
            PlaySound(1);
        else
            PlaySound(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(Tags.Ground))
            return;

        // TODO: Générer gameObject de Seb.
        print(collision.contacts[0].point);
    }
}
