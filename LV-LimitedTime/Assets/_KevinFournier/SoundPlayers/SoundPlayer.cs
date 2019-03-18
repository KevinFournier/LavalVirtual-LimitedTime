using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{

	public List<AudioClip> AudioClips;

	public bool PlayAtStart = false;

    [Header("Gestion des loops")]
	public bool Loop = false;
    [Space]
    public bool LoopWithDelay = false;
    public bool LoopRandom = false;
    public Vector2 LoopDelay = Vector2.zero;

    public int NbOfRepetition = -1;

	[HideInInspector]
	protected AudioSource Source;

	private void Start()
    {
		Init();
	}

    /// <summary>
    /// Initialise le SoundPlayer en fonction des paramètres insérés dans Unity.
    /// </summary>
	protected virtual void Init()
    {
        if (LoopWithDelay)
            Loop = false;

		Source = GetComponent<AudioSource>();
		Source.playOnAwake = false;
		Source.loop = Loop;
        Source.spatialBlend = 1.0f;
        Source.Stop();

        if (PlayAtStart && LoopWithDelay)
        {
            PlayRandomSound();
            RepeatSoundWithDelay(LoopDelay, LoopRandom, NbOfRepetition);
        }
        else if (PlayAtStart)
            PlayRandomSound();
	}

    /// <summary>
    /// Indique si le SoundPlayer actuel peux jouer un son.
    /// </summary>
    /// <returns>True si on peut jouer un son, sinon : False.</returns>
    protected bool CanPlaySound()
        => !Source.isPlaying && AudioClips != null && AudioClips.Count != 0;

    protected IEnumerator RepeatSound(Vector2 delayRange, bool randomSound = false, int nbMax = -1, int count = 1)
    {

        // Attend que le son soit fini de jouer.
        yield return new WaitWhile(() => Source.isPlaying);

        var delay
            = Random.Range(
                Mathf.Min(delayRange.x, delayRange.y),
                Mathf.Max(delayRange.x, delayRange.y));

        yield return new WaitForSecondsRealtime(delay);

        if (randomSound)
            PlayRandomSound();
        else
            Source.Play();

        if (nbMax == -1 || nbMax > 0 && count < nbMax)
            StartCoroutine(RepeatSound(delayRange, randomSound, nbMax, ++count));

    }

    #region Public Methods
    /// <summary>
    /// Joue le premier son de la liste d'audioclip.
    /// </summary>
    public void PlaySound()
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
	public void PlayRandomSound()
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

    public Coroutine RepeatSoundWithDelay(Vector2 delayRange = default, bool randomSound = false, int nbMax = -1)
    {
        if (delayRange == default)
            delayRange = Vector2.zero;

        return StartCoroutine(RepeatSound(delayRange, randomSound, nbMax));
    }
    #endregion

}
