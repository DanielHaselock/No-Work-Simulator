using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioManager : MonoBehaviour
{
    [Header("Musics")]
    [SerializeField] private AudioClip MrLucky;
    [SerializeField] private AudioClip MrSatisfied;
    [SerializeField] private AudioClip SummerBreezes;


    [Header("Event/Mission")]
    [SerializeField] private AudioClip PhoneRinging;
    [SerializeField] private AudioClip PhonePickUp;
    [SerializeField] private AudioClip McSnoring;

    public AudioClip Music1 { get => MrLucky; }
    public AudioClip Music2 { get => MrSatisfied; }
    public AudioClip Music3 { get => SummerBreezes; }
    public AudioClip Phone1 { get => PhoneRinging; }
    public AudioClip Phone2 { get => PhonePickUp; }
    public AudioClip Sleep { get => McSnoring; }

 

    public static AudioSource audioAmbient;
    public static AudioClip clip;

    public bool canPlay;

    void Start()
    {
        audioAmbient = GetComponent<AudioSource>();
        StartCoroutine(ambientMusic());
        canPlay = true;

    }

    
    public void PlayAudio()
    {
        if(canPlay)
        {
            canPlay = false;
            audioAmbient.PlayOneShot(clip);
            StartCoroutine(Reset());
        }
           
    }

    public void SetClip(string clip2)
    {
        if(clip2 == "Music1")
        {
            clip = Music1;
        }
        if (clip2 == "Music2")
        {
            clip = Music2;
        }
        if (clip2 == "Music3")
        {
            clip = Music3;
        }
        if (clip2 == "Phone1")
        {
            clip = Phone1;
        }
        if (clip2 == "Phone2")
        {
            clip = Phone2;
        }
        if (clip2 == "Sleep")
        {
            clip = Sleep;
        }
    }

    IEnumerator ambientMusic()
    {
        clip = Music1;
        PlayAudio();
        yield return new WaitForSeconds(clip.length);
        clip = Music2;
        PlayAudio();
        yield return new WaitForSeconds(clip.length);
        clip = Music3;
        PlayAudio();
        yield return new WaitForSeconds(clip.length);
        StartCoroutine(ambientMusic());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2f);
        canPlay = true;
    }
}



