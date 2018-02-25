
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
    None
}

public class SoundManager : MonoBehaviour
{

    public static SoundManager main;

    [SerializeField]
    private List<GameSound> sounds = new List<GameSound>();

    private bool sfxMuted = false;

    [SerializeField]
    private bool musicMuted = false;
    public bool MusicMuted { get { return musicMuted; } }

    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource windSource;

    void Awake()
    {
        main = this;
    }

    private void Start()
    {
        if (musicSource != null)
        {
            if (musicMuted)
            {
                musicSource.Pause();
            }
            else
            {
                musicSource.Play();
            }
        }
        if (windSource != null)
        {
            if (sfxMuted)
            {
                windSource.Pause();
            }
            else
            {
                windSource.Play();
            }
        }
    }

    public void PlaySound(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    gameSound.sound.Play();
                }
            }
        }
    }

    public void ToggleSfx()
    {
        sfxMuted = !sfxMuted;
    }

    public bool ToggleMusic()
    {
        musicMuted = !musicMuted;
        if (musicMuted)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
        }
        return musicMuted;
    }
}

[System.Serializable]
public class GameSound : System.Object
{
    public SoundType soundType;
    public AudioSource sound;
}
