using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    private TextMeshProUGUI on_off;
    private bool isFullScreen;
    void Start()
    {
        on_off = GameObject.Find("on_off_button").GetComponent<TextMeshProUGUI>();
        isFullScreen = false;
    }
    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("volume", volume);
    }

    public void SetFullScreen()
    {
        on_off.text = on_off.text == "ON" ? "OFF" : "ON";
        isFullScreen = isFullScreen ? false : true;
        Screen.fullScreen = isFullScreen;
    }
}
