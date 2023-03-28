using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManagerInstance;
    public Sound[] sounds;
    [SerializeField] private Slider bgmSlider, sfxSlider;
    

    // Start is called before the first frame update
    private void Awake()
    {
        if (audioManagerInstance == null)
        {
            audioManagerInstance= this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    void Start()
    {
        if(bgmSlider!=null && sfxSlider!= null)
        {
            bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume", 1f);
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);
        }

        Play("BackgroundMusic");
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.audioType == 0)
        {
            s.source.loop = true;
        }
        else
        {   
            s.source.loop = false;
        }
        s.source.Play();
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.clip);
    }

    public void SetBGMVolume()
    {
        foreach (Sound s in sounds)
        {
            if (s.audioType == Sound.TypeAudio.BGM)
            {
                s.volume = bgmSlider.value;
                PlayerPrefs.SetFloat("bgmVolume", bgmSlider.value);
            }

            PlayerPrefs.Save();
            s.source.volume = s.volume;
        }
    }

    public void SetSFXVolume()
    {
        foreach (Sound s in sounds)
        {
            if (s.audioType == Sound.TypeAudio.SFX)
            {
                s.volume = sfxSlider.value;
                PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
            }

            PlayerPrefs.Save();
            s.source.volume = s.volume;
        }
    }
}
