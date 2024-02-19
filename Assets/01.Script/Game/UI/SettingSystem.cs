using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField]
    private Slider BGM_Slider;
    [SerializeField]
    private Slider VFX_Slider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BGM_Value")) PlayerPrefs.SetFloat("BGM_Value", 1.0f);
        if (!PlayerPrefs.HasKey("VFX_Value")) PlayerPrefs.SetFloat("VFX_Value", 1.0f);

        BGM_Slider.value = PlayerPrefs.GetFloat("BGM_Value");
        VFX_Slider.value = PlayerPrefs.GetFloat("VFX_Value");

        BGM_Slider.onValueChanged.AddListener(delegate { OnSliderValueChanged_BGM(); });
        VFX_Slider.onValueChanged.AddListener(delegate { OnSliderValueChanged_VFX(); });
    }

    private void OnEnable()
    {
        SoundManager.Instance.PauseEffect();
        GameManager.Instance.IngameUIManager.isGameClickDisabled = true;
    }

    private void OnDisable()
    {
        SoundManager.Instance.UnPauseEffect();
        GameManager.Instance.IngameUIManager.isGameClickDisabled = false;
    }

    void OnSliderValueChanged_BGM()
    {
        GameManager.Instance.BGM_Value = BGM_Slider.value;
        PlayerPrefs.SetFloat("BGM_Value", BGM_Slider.value);
        Debug.Log("BGM_Value : " + BGM_Slider.value);
    }

    void OnSliderValueChanged_VFX()
    {
        GameManager.Instance.VFX_Value = VFX_Slider.value;
        PlayerPrefs.SetFloat("VFX_Value", VFX_Slider.value);
        Debug.Log("VFX_Value : " + VFX_Slider.value);
    }
}
