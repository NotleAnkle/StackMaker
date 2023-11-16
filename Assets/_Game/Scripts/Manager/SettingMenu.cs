using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Toggle mute;
    public void SetVolume(float volume)
    {
        SoundManager.instance.SetVolume(volume);
        
        if (volume < 0.1f)
        {
            mute.isOn = true;
        }
        else
        {
            mute.isOn = false;
        }
    }

    public void Mute()
    {
        if (mute.isOn)
        {
            slider.value = 0f;
        }
        else
        {
            slider.value = 1f;
        }
    }
}
