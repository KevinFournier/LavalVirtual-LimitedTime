using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{

	public enum SounsType {
		Oiseaux, Arbres
	}

	public List<AudioClip> AudioClips;
	public bool isLoop = false;
	public float Delay;

	public AudioSource AS;

	public void Start() {
		init();
	}

	/*
	private void OnTriggerEnter(Collider other) {
		Invoke("playSound", Delay);
	}
	*/

	public void playSound() {
		if (!AS.isPlaying) {
			AS.clip = AudioClips[Random.Range(0, AudioClips.Count)];
			AS.Play();
		}
	}

	public void init() {
		AS = GetComponent<AudioSource>();
		AS.playOnAwake = false;
		AS.loop = isLoop;
	}

}
