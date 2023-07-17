using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private const string MASTERVOLUME = "MasterVolumeParameter";
    private const string SFXVOLUME = "SFXVolumeParameter";
    private const string BGMVOLUME = "BGMVolumeParameter";
    public GameObject TitleScreenObject;
    public GameObject OptionsMenuObject;
    public GameObject PauseMenuObject;
    public GameObject GameOverObject;

    public AudioMixer audioMixer;
    public Slider mainVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMasterVolumeChange(float value)
    {
        // Start with the slider value (assuming our slider runs from 0 to 1)
        float newVolume = ConvertToDecibel(value);

        // Set the volume to the new volume setting
        audioMixer.SetFloat(MASTERVOLUME, newVolume);
    }

    public void OnSFXVolumeChange(float value)
    {
        // Start with the slider value (assuming our slider runs from 0 to 1)
        float newVolume = ConvertToDecibel(value);

        // Set the volume to the new volume setting
        audioMixer.SetFloat(SFXVOLUME, newVolume);
    }

    public void OnBGMVolumeChange(float value)
    {
        // Start with the slider value (assuming our slider runs from 0 to 1)
        float newVolume = ConvertToDecibel(value);

        // Set the volume to the new volume setting
        audioMixer.SetFloat(BGMVOLUME, newVolume);
    }

    private float ConvertToDecibel(float value)
    {
        float newVolume = value;
        if (newVolume <= 0)
        {
            // If we are at zero, set our volume to the lowest value
            newVolume = -80;
        }
        else
        {
            // We are >0, so start by finding the log10 value 
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20db range (instead of 0-1 db)
            newVolume = newVolume * 20;
        }

        return newVolume;
    }

    public void HideTitleScreenUI()
    {
        TitleScreenObject.SetActive(false);
    }

    public void ShowTitleScreenUI()
    {
        TitleScreenObject.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        PauseMenuObject.SetActive(true);
    }

    public void HidePauseMenu()
    {
        PauseMenuObject.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        OptionsMenuObject.SetActive(true);
    }

    public void HideOptionsMenu()
    {
        OptionsMenuObject.SetActive(false);
    }

    public void ShowGameOver()
    {

    }

    public void HideGameOver()
    {

    }

    public void HandleGameStateChanged(GameState previousState, GameState newState)
    {
        switch (previousState)
        {
            case GameState.TitleState:
                HideTitleScreenUI();
                break;
            case GameState.OptionsState:
                HideOptionsMenu();
                break;
            case GameState.GameplayState:
                break;
            case GameState.GameOverState:
                HideGameOver();
                break;
            case GameState.Credits:
                // Hide Credits
                break;
            case GameState.Pause:
                HidePauseMenu();
                break;
        }
        switch (newState)
        {
            case GameState.TitleState:
                ShowTitleScreenUI();
                break;
            case GameState.OptionsState:
                ShowOptionsMenu();
                break;
            case GameState.GameplayState:
                break;
            case GameState.GameOverState:
                ShowGameOver();
                break;
            case GameState.Credits:
                break;
            case GameState.Pause:
                ShowPauseMenu();
                break;
        }
    }
}
