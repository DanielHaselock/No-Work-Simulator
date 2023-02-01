using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PranksterAudioManager : MonoBehaviour
{
    [Header("Prankster")]
    [SerializeField] private AudioClip SplashLaughFull;

    public AudioClip OhNo { get => SplashLaughFull; }

    public static AudioSource audioPrank;
    public static AudioClip clip;

    void Start()
    {
        audioPrank = GetComponent<AudioSource>();
    }


    public static void PlayAudio()
    {
        audioPrank.PlayOneShot(clip);
    }
}
