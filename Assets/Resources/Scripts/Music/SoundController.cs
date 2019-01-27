using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{

    public static SoundController instance;
    // We use 2 bgm audio clip to be able to transition between both
    public AudioSource BGM1;
    public AudioSource BGM2;
    public int BGMPointer = 1;


    // We use 2 bgs audio clip to be able to transition between both
    public AudioSource BGS1;
    public AudioSource BGS2;
    public int BGSPointer = 1;

    public AudioMixerSnapshot BGM1On;
    public AudioMixerSnapshot BGM2On;
    public AudioMixerSnapshot BGMOff;

    public AudioMixerSnapshot BGS1On;
    public AudioMixerSnapshot BGS2On;
    public AudioMixerSnapshot BGSOff;

    // Using several AudioSource for SE to be able to play several sounds at the same time
    public AudioSource SE1;
    public AudioSource SE2;
    public AudioSource SE3;
    public int SEPointer = 1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    /*------------------------------------------
    
                BGM CONTROL
    ------------------------------------------- */

    /*
    Ensure smooth transition when changing background music
    transitionTime in seconds
    volume value between 0 and 1
    Usage:
    StartCoroutine(SoundController.instance.playBackgroundMusic("BgmDungeon", 0.5f, 2));
     */
    public IEnumerator playBackgroundMusic(string musicFile, float volume, float transitionTime)
    {
        if (musicFile != getCurrentBGMClip())
        {
            // Creating AudioClip from the musicFile path
            string bgmPath = "Audio/BGM/" + musicFile;
            AudioClip musicToLoad = Resources.Load<AudioClip>(bgmPath);
            yield return null;

            /*
            If the first BGM is currently playing set the second BGM and transition between both
            Otherwise do the opposite
             */
            if (BGMPointer == 1)
            {
                BGM2.clip = musicToLoad;
                BGM2.Play();
                BGM2.volume = volume;
                BGM2On.TransitionTo(transitionTime);
                BGMPointer = 2;
            }
            else
            {
                BGM1.clip = musicToLoad;
                BGM1.Play();
                BGM1.volume = volume;
                BGM1On.TransitionTo(transitionTime);
                BGMPointer = 1;
            }
            yield return new WaitForSeconds(transitionTime);
            if (BGMPointer == 1)
            {
                BGM2.Stop();
            }
            else
            {
                BGM1.Stop();
            }
        }
    }

    /*
    transitionTime in seconds
    Usage:
    StartCoroutine(SoundController.instance.stopBackgroundMusic(2));
     */
    public IEnumerator stopBackgroundMusic(float transitionTime)
    {
        BGMOff.TransitionTo(transitionTime);
        yield return new WaitForSeconds(transitionTime);
        if (BGMPointer == 1)
        {
            BGM1.Stop();
        }
        else
        {
            BGM2.Stop();
        }
    }

    // Return the BGM clip actually playing as a String
    public string getCurrentBGMClip()
    {
        string clipName = null;
        if (BGMPointer == 1)
        {
            if (BGM1.isPlaying)
            {
                clipName = BGM1.clip.name;
            }
        }
        else
        {
            if (BGM2.isPlaying)
            {
                clipName = BGM2.clip.name;
            }
        }
        return clipName;
    }

    public float getCurrentBGMAudioSourceVolume()
    {
        float volume = 0;
        if (BGMPointer == 1)
        {
            if (BGM1.isPlaying)
            {
                volume = BGM1.volume;
            }
        }
        else
        {
            if (BGM2.isPlaying)
            {
                volume = BGM2.volume;
            }
        }
        return volume;
    }

    /*
    set the volume of the Audio Source (Not in the AudioMixer !!).
    volume value between 0 and 1
    transitionTime in seconds
     */
    public IEnumerator setBGMVolumeTo(float volume, float transitionTime)
    {
        float initVolume = getCurrentBGMAudioSourceVolume();
        float deltaVolume;
        // Each frame, we will calculate a deltaVolume so that the volume decreases smoothly
        for (float i = 0; i < transitionTime; i += Time.deltaTime)
        {
            deltaVolume = initVolume + (volume - initVolume) * i / transitionTime;
            if (BGMPointer == 1)
            {
                BGM1.volume = deltaVolume;
            }
            else
            {
                BGM2.volume = deltaVolume;
            }
            yield return null;
            if (BGMPointer == 1)
            {
                BGM1.volume = volume;
            }
            else
            {
                BGM2.volume = volume;
            }
        }
    }


    /*------------------------------------------
    
                BGS CONTROL
    ------------------------------------------- */

    /*
    Ensure smooth transition when changing background sound
    transitionTime in seconds
    volume value between 0 and 1
    Usage:
    StartCoroutine(SoundController.instance.playBackgroundSound("BgsDungeon", 0.5f, 2));
     */
    public IEnumerator playBackgroundSound(string soundFile, float volume, float transitionTime)
    {
        if (soundFile != getCurrentBGSClip())
        {
            // Creating AudioClip from the soundFile path
            string bgsPath = "Audio/BGS/" + soundFile;
            AudioClip soundToLoad = Resources.Load<AudioClip>(bgsPath);
            yield return null;

            /*
            If the first BGS is currently playing set the second BGS and transition between both
            Otherwise do the opposite
             */
            if (BGSPointer == 1)
            {
                BGS2.clip = soundToLoad;
                BGS2.Play();
                BGS2.volume = volume;
                BGS2On.TransitionTo(transitionTime);
                BGSPointer = 2;
            }
            else
            {
                BGS1.clip = soundToLoad;
                BGS1.Play();
                BGS1.volume = volume;
                BGS1On.TransitionTo(transitionTime);
                BGSPointer = 1;
            }
            yield return new WaitForSeconds(transitionTime);
            if (BGSPointer == 1)
            {
                BGS2.Stop();
            }
            else
            {
                BGS1.Stop();
            }
        }
    }

    /*
    transitionTime in seconds
    Usage:
    StartCoroutine(SoundController.instance.stopBackgroundSound(2));
     */
    public IEnumerator stopBackgroundSound(float transitionTime)
    {
        BGSOff.TransitionTo(transitionTime);
        yield return new WaitForSeconds(transitionTime);
        if (BGSPointer == 1)
        {
            BGS1.Stop();
        }
        else
        {
            BGS2.Stop();
        }
    }

    // Return the BGS clip actually playing as a String
    public string getCurrentBGSClip()
    {
        string clipName = null;
        if (BGSPointer == 1)
        {
            if (BGS1.isPlaying)
            {
                clipName = BGS1.clip.name;
            }
        }
        else
        {
            if (BGS2.isPlaying)
            {
                clipName = BGS2.clip.name;
            }
        }
        return clipName;
    }

    public float getCurrentBGSAudioSourceVolume()
    {
        float volume = 0;
        if (BGSPointer == 1)
        {
            if (BGS1.isPlaying)
            {
                volume = BGS1.volume;
            }
        }
        else
        {
            if (BGS2.isPlaying)
            {
                volume = BGS2.volume;
            }
        }
        return volume;
    }

    /*
    set the volume of the Audio Source (Not in the AudioMixer !!).
    volume value between 0 and 1
    transitionTime in seconds
     */
    public IEnumerator setBGSVolumeTo(float volume, float transitionTime)
    {
        float initVolume = getCurrentBGSAudioSourceVolume();
        float deltaVolume;
        // Each frame, we will calculate a deltaVolume so that the volume decreases smoothly
        for (float i = 0; i < transitionTime; i += Time.deltaTime)
        {
            deltaVolume = initVolume + (volume - initVolume) * i / transitionTime;
            if (BGSPointer == 1)
            {
                BGS1.volume = deltaVolume;
            }
            else
            {
                BGS2.volume = deltaVolume;
            }
            yield return null;
            if (BGSPointer == 1)
            {
                BGS1.volume = volume;
            }
            else
            {
                BGS2.volume = volume;
            }
        }
    }

    /*
    This function is used to play a Sound Effect
     */
    public IEnumerator playSE(string soundFile, float volume)
    {
        string sePath = "Audio/SE/" + soundFile;
        AudioClip soundToPlay = Resources.Load<AudioClip>(sePath);
        yield return null;
        Debug.Log(SEPointer);
        switch (SEPointer)
        {
            case 1:
                SE1.clip = soundToPlay;
                SE1.Play();
                SEPointer = 2;
                break;
            case 2:
                SE2.clip = soundToPlay;
                SE2.Play();
                SEPointer = 3;
                break;
            case 3:
                SE2.clip = soundToPlay;
                SE2.Play();
                SEPointer = 1;
                break;
            default:
                SE1.clip = soundToPlay;
                SE1.Play();
                SEPointer = 2;
                break;
        }
    }
}
