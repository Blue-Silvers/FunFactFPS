using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] TextMeshProUGUI musicTxt, soundTxt;

    [Header("Graphics")]
    [SerializeField] private TMPro.TMP_Dropdown DropdownGraphics;
    [SerializeField] private TMPro.TMP_Dropdown DropdownResolution;

    Resolution[] resolutions;


    public void Start()
    {
        //Audio sliders
        audioMixer.GetFloat("Music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;
        int musicValue = (int)musicValueForSlider + 80;
        soundTxt.text = musicValue.ToString() + " %";

        audioMixer.GetFloat("Sound", out float soundValueForSlider);
        soundSlider.value = soundValueForSlider;
        int soundValue = (int)musicValueForSlider + 80;
        soundTxt.text = soundValue.ToString() + " %";

        //Resolution settings dropdown
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        DropdownResolution.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        DropdownResolution.AddOptions(options);
        DropdownResolution.value = currentResolutionIndex;
        DropdownResolution.RefreshShownValue();


        //Quality settings dropdown
        DropdownGraphics.ClearOptions();
        List<string> optionsQ = new List<string>();

        int currentQualityIndex = QualitySettings.GetQualityLevel();
        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            string optionQ = QualitySettings.names[i];
            optionsQ.Add(optionQ);
        }

        DropdownGraphics.AddOptions(optionsQ);
        DropdownGraphics.value = currentQualityIndex;
        DropdownGraphics.RefreshShownValue();


        //full screen
        Screen.fullScreen = true;
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        int volumeValue = (int)volume + 80;
        musicTxt.text = volumeValue.ToString() + " %";
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("Sound", volume);
        int soundValue = (int)volume + 80;
        soundTxt.text = soundValue.ToString() + " %";
    }

    public void SetQuality(int qualityIndex)
    {
        qualityIndex = DropdownGraphics.value;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

}
