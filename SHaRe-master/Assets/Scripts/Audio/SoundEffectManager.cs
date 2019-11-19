using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{

    public static SoundEffectManager Instance = null;


    [SerializeField] private AudioClip clip_CoffeeMachine;
    [SerializeField] private AudioClip clip_MenuClose;
    [SerializeField] private AudioClip clip_MenuOpen;
    [SerializeField] private AudioClip clip_OrderComplete;
    [SerializeField] private AudioClip[] clips_orderReceived;
    [SerializeField] private AudioClip clip_sauceSqueeze;
    [SerializeField] private AudioClip clip_subtaskComplete;
    [SerializeField] private AudioClip clip_pouring;
    [SerializeField] private AudioClip[] clips_CupPut;
    [SerializeField] private AudioClip[] clips_CupLift;
    [SerializeField] private AudioClip clip_WhippedCreamSpray;
    [SerializeField] private AudioClip clip_Ambient;

    private AudioSource[] myAudioSources;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayAmbient();
        }
            

        myAudioSources = new AudioSource[6];

        for (int i = 0; i < myAudioSources.Length; i++)
        {
            GameObject audioSource = new GameObject("audioSource_" + i);
            Utils.AddAudioListener(audioSource, false, 1.0f, false);
            audioSource.transform.parent = transform;
            myAudioSources[i] = audioSource.GetComponent<AudioSource>();
        }

        

    }

    #region Play_Sounds
    public void PlayOpenMenu()
    {
        PlaySound(clip_MenuOpen);   
    }

    public void PlayCloseMenu()
    {
        PlaySound(clip_MenuClose);
    }

    public void PlayCoffeeMachine()
    {
        PlayLoopedSound(clip_CoffeeMachine);
    }

    public void PlaySplash()
    {
        PlayLoopedSound(clip_pouring);
    }

    public void PlaySauceSqueezing()
    {

        PlayLoopedSound(clip_sauceSqueeze);
    }

    public void PlayOrderComplete()
    {
        PlayRandomSound(clips_orderReceived);
    }

    public void PlaySubtaskComplete()
    {
        PlaySound(clip_subtaskComplete);
    }

    
    public void PlayPutCup()
    {
        PlayRandomSound(clips_CupPut);
    }

    public void PlayLiftCup()
    {
        PlayRandomSound(clips_CupLift);
    }

    public void PlayWhippedCreamSpray()
    {
        PlayLoopedSound(clip_WhippedCreamSpray);
    }

    public void PlayAmbient()
    {
        PlaySound(clip_Ambient);
    }

    #endregion

    #region Stop_Sounds
    public void StopOpenMenu()
    {
        StopSound(clip_MenuOpen);
    }

    public void StopCloseMenu()
    {
        StopSound(clip_MenuClose);
    }

    public void StopCoffeeMachine()
    {
        StopSound(clip_CoffeeMachine);
    }

    public void StopSplash()
    {
        StopSound(clip_pouring);
    }

    public void StopSauceSqueezing()
    {
        StopSound(clip_sauceSqueeze);
    }

    public void StopOrderComplete()
    {
        StopRandomSound(clips_orderReceived);
    }

    public void StopSubtaskComplete()
    {
        StopSound(clip_subtaskComplete);
    }


    public void StopPutCup()
    {
        StopRandomSound(clips_CupPut);
    }

    public void StopLiftCup()
    {
        StopRandomSound(clips_CupLift);
    }

    public void StopWhippedCreamSpray()
    {
        StopSound(clip_WhippedCreamSpray);
    }

    public void StopAmbient()
    {
        StopSound(clip_Ambient);
    }

    #endregion

    #region Utilities_Functions

    private void PlaySound(AudioClip clip)
    {
        bool found = false;
        if(myAudioSources != null)
        {
            foreach (AudioSource aS in myAudioSources)
            {
                if (aS != null && aS.playOnAwake == true)
                {
                    continue;
                }
                else
                {
                    aS.loop = false;
                    Utils.PlaySound(aS, clip);
                    found = true;
                }
            }
        }
        
        if (found == false)
        {
            Debug.Log("All audio sources are busy");
        }
    }

    private void PlayLoopedSound(AudioClip clip)
    {
        bool found = false;
        foreach (AudioSource aS in myAudioSources)
        {
            if (aS != null && aS.playOnAwake == true)
            {
                continue;
            }
            else
            {
                aS.loop = true;
                Utils.PlaySound(aS, clip);
                found = true;
            }
        }
        if (found == false)
        {
            Debug.Log("All audio sources are busy");
        }
    }

    private void PlayRandomSound(AudioClip[] clips)
    {
        bool found = false;
        foreach (AudioSource aS in myAudioSources)
        {
            if (aS != null && aS.playOnAwake == true)
            {
                continue;
            }
            else
            {
                Utils.PlayRandomSound(aS, clips);
                found = true;
            }
        }
        if (found == false)
        {
            Debug.Log("All audio sources are busy");
        }
    }

    private void StopSound(AudioClip clip)
    {
        bool found = false;
        foreach(AudioSource aS in myAudioSources)
        {
            if (aS != null && aS.clip == clip)
            {
                aS.Stop();
                aS.loop = false;
                found = true;
            }
        }
        if (found == false)
        {
            Debug.Log("Clip " + clip.name + " not playing");
        }
    }


    private void StopRandomSound(AudioClip[] clips)
    {
        bool found = false;
        foreach (AudioClip clip in clips)
        {
            foreach (AudioSource aS in myAudioSources)
            {
                if (aS != null && aS.clip == clip)
                {
                    aS.Stop();
                    found = true;
                }
            }
        }
        if (found == false)
        {
            Debug.Log("Clips not playing");
        }
    }

    #endregion

}
