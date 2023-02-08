using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume_Slider : MonoBehaviour
{
    [SerializeField] string _volumeParameter = "MasterVolume";
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider _slider;
    [SerializeField] float _multiplier = 30f;
    [SerializeField] float sliderValue = 1;

    private void Awake()
    {
        _slider.onValueChanged.AddListener(SliderValueChanged);
        sliderValue = PlayerPrefs.GetFloat(_volumeParameter, _slider.value);
        _slider.value = sliderValue;
    }

    private void onDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _slider.value);
    }

    private void SliderValueChanged(float value)
    {
        _mixer.SetFloat(_volumeParameter, SliderToMixer(value));
        PlayerPrefs.SetFloat(_volumeParameter, _slider.value);
    }

    private float SliderToMixer(float x)
    {
        return Mathf.Log10(x) * _multiplier;
    }
}