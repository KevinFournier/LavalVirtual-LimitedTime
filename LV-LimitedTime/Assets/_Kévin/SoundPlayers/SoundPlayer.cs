using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{

	public List<AudioClip> AudioClips;
	public bool PlayAtStart = false;
	public bool isLoop = false;

	[HideInInspector]
	public AudioSource Source;

	private void Start()
    {
		Init();
	}

    /// <summary>
    /// Initialise le SoundPlayer en fonction des paramètres insérés dans Unity.
    /// </summary>
	protected virtual void Init()
    {
		Source = GetComponent<AudioSource>();
		Source.playOnAwake = false;
		Source.loop = isLoop;

        if (PlayAtStart)
            PlayRandomSound();
	}

    /// <summary>
    /// Indique si le SoundPlayer actuel peux jouer un son.
    /// </summary>
    /// <returns>True si on peut jouer un son, sinon : False.</returns>
    protected bool CanPlaySound()
        => !Source.isPlaying && AudioClips != null && AudioClips.Count != 0;

    #region Public Methods
    /// <summary>
    /// Joue le premier son de la liste d'audioclip.
    /// </summary>
    public virtual void PlaySound()
    {
        if (!CanPlaySound())
            return;

		AudioClip clip = AudioClips[0];
        if (clip != null)
        {
            Source.clip = clip;
            Source.Play();
        }

    }

    /// <summary>
    /// Joue un son aléatoire de la list d'audioclips.
    /// </summary>
	public virtual void PlayRandomSound()
    {
        if (!CanPlaySound())
            return;

		AudioClip clip = AudioClips[Random.Range(0, AudioClips.Count)];
        if (clip != null)
        {
            Source.clip = clip;
            Source.Play();
        }
	}

    /// <summary>
    /// Joue le son demandé en paramètre.
    /// </summary>
    /// <param name="index">Index du son à jouer</param>
	public void PlaySound(int index)
    {
        if (!CanPlaySound() && AudioClips.Count > index)
            return;

        if (index < 0)
            index = 0;

		AudioClip clip = AudioClips[index];
        if (clip != null)
        {
            Source.clip = clip;
            Source.Play();
        }
	}
    #endregion
}
