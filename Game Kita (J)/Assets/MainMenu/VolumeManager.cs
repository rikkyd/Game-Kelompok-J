using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    
    [SerializeField] AudioSource musicSource; 
    [SerializeField] AudioSource[] sfxSources; 


    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadMusicVolume();
        }
        else
        {
            LoadMusicVolume();
        }

        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
            LoadSFXVolume();
        }
        else
        {
            LoadSFXVolume();
        }
    }

    public void ChangeMusicVolume()
    {
        if (musicSource != null)
        {
            musicSource.volume = musicVolumeSlider.value;
        }
        SaveMusicVolume();
    }

    public void ChangeSFXVolume()
    {
        foreach (AudioSource source in sfxSources)
        {
            source.volume = sfxVolumeSlider.value;
        }
        SaveSFXVolume();
    }

    private void LoadMusicVolume()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void LoadSFXVolume()
    {
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
    }

    private void SaveSFXVolume()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxVolumeSlider.value);
    }
}