using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public MusicManager musicManager;   

   //private UnityEngine.UI.Slider musicSlider;

    // Use this for initialization
    void Start()
    {
        //musicSlider = GameObject.Find("SFX_Slider").GetComponent<UnityEngine.UI.Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckEscapeKeystroke();
    }

    void CheckEscapeKeystroke()
    {
        if (Input.GetKeyDown("escape"))
        {
            gameManager.TogglePauseMenu();
        }
    }

    //-----------------------------------------------------------
    // Game Options Function Definitions
    //public void OptionSliderUpdate(float val) { ... }
    //void SetCustomSettings(bool val) { ... }
    //void WriteSettingsToInputText(GameSettings settings) { ... }

    //-----------------------------------------------------------
    // Music Settings Function Definitions
    public void SFXSliderUpdate(float volume)
    {
        musicManager.SetSFXVolume(volume);
    }

    public void MusicSliderUpdate(float volume)
    {
        musicManager.SetBackgroundVolume(volume);
    }

    //public void MusicToggle(bool val)
    //{
    //    _musicSlider.interactable = val;
    //    MM.SetVolume(val ? _musicSlider.value : 0f);
    //}
}