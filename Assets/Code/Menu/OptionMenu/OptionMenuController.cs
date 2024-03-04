using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using UnityEngine.UI;

public class OptionMenuController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sliderMusic;
    public Slider sliderSFX;
    void Start()
    {
        sliderMusic.value = GameController.Instance.getVolumeMusic();
        sliderSFX.value = GameController.Instance.getVolumeSFX();

        audioMixer.SetFloat("Volume_Music", GameController.Instance.getVolumeMusic());
        audioMixer.SetFloat("Volume_SoundFX", GameController.Instance.getVolumeSFX());
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("Volume_Music", volume);
        GameController.Instance.setVolumeMusic(volume);
    }

    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("Volume_SoundFX", volume);
        GameController.Instance.setVolumeSFX(volume);
    }
}
