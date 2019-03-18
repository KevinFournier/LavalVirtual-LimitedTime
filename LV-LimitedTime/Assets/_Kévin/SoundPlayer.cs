using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour {

	public enum SoundsType {
		Oiseaux, Arbres, Gravier, Herbe
	}

	public SoundsType SoundType;
	public List<AudioClip> AudioClips;
	public bool PlayAtStart = false;
	public bool isLoop = false;
	public float Delay;

	[HideInInspector]
	public AudioSource AS;

	public void Start() {
		init();

		if (PlayAtStart) {
			playSound();
		}
	}

	public void init() {
		AS = GetComponent<AudioSource>();
		AS.playOnAwake = false;
		AS.loop = isLoop;
	}

	public void playSound() {
		if (!AS.isPlaying) {
			int max = AudioClips.Count;
			AudioClip clip = AudioClips[Random.Range(0, AudioClips.Count)];
			if (clip != null) {
				AS.clip = AudioClips[Random.Range(0, AudioClips.Count)];
				AS.Play();
			}
		}
	}

	public void playSound(int num) {
		if (!AS.isPlaying && num < AudioClips.Count) {
			AudioClip clip = AudioClips[num];
			if (clip != null) {
				AS.clip = AudioClips[num];
				AS.Play();
			}
		}
	}

}
