using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionVolume : MonoBehaviour
{
    public AudioMixer masterMixer;

    public Slider audioSlider;

    public void AudioControl()
    {
        float sound = audioSlider.value;
        if (sound == -40f) masterMixer.SetFloat("BGM", -80);
        else masterMixer.SetFloat("BGM", sound);
    }

    public void SFXAudioControl()
    {
        float sound = audioSlider.value;
        if (sound == -40f) masterMixer.SetFloat("SFX", -80);
        else masterMixer.SetFloat("SFX", sound);
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}