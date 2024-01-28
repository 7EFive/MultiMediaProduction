using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer mixer;
    public Slider slider;
    private const float _multipier = 20f;
    private float _volumeValue;

    public void Awake()
    {
        slider.onValueChanged.AddListener(SlideValueChanged);
    }

    private void SlideValueChanged(float value)
    {
        _volumeValue = Mathf.Log10(value) * _multipier;
        mixer.SetFloat(volumeParameter, _volumeValue);
    }
    void Start()
    {
        _volumeValue = PlayerPrefs.GetFloat(volumeParameter, Mathf.Log10(slider.value) 
            * _multipier);
        slider.value = Mathf.Pow(10f, _volumeValue / _multipier);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, _volumeValue);
    }

}
