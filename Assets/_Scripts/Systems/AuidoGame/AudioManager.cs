using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Volume")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ApplyVolume();
    }

    public void ApplyVolume()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        ApplyVolume();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        ApplyVolume();
    }
}
