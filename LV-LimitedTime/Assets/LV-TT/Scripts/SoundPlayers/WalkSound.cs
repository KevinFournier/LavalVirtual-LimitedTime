using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Foot { Left, Right }

public class WalkSound : SoundPlayer
{
    public Texture2D map;
    public Foot Foot;
    public GameObject Footprint;

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

        GameObject f = Instantiate(
                Footprint,
                new Vector3(
                    collision.contacts[0].point.x,
                    collision.contacts[0].point.y + 0.1f,
                    collision.contacts[0].point.z),
                Quaternion.identity);

        if (Foot == Foot.Right)
            f.transform.localScale
                = new Vector3(
                    f.transform.localScale.x * -1,
                    f.transform.localScale.y,
                    f.transform.localScale.z);

        Destroy(f, 3f);

    }

    public override void PlaySound()
    {
        if (AudioClips != null && AudioClips.Count != 0)
        {


            AudioClip clip = AudioClips[0];
            if (clip != null)
            {
                Source.clip = clip;
                Source.Play();
            }
        }
    }

}