using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = AudioManager.instance.musicVolume;
        sfxSlider.value = AudioManager.instance.sfxVolume;

        musicSlider.onValueChanged.AddListener(AudioManager.instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.instance.SetSFXVolume);
    }
}