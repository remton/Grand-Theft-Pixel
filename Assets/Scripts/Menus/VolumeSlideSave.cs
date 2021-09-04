using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlideSave : MonoBehaviour {

    public Slider VolumeSlider;

    private void Start()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("MVolume");
    }
}
