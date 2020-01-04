using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Script to control a mixer for simple sound on off functionality
public class SoundMixer : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer = null;
    [SerializeField] private PlayerPrefIntVariable soundToggleStatePP = null;
    private bool soundToggleState = false;
    [SerializeField] private Image soundButtonToggleImage = null;
    [SerializeField] private Sprite soundOnSprite = null;
    [SerializeField] private Sprite soundOffSprite = null;

    [SerializeField] private AudioMixerSnapshot soundOnSnapshot = null;
    [SerializeField] private AudioMixerSnapshot soundOffSnapshot = null;
    [SerializeField] private float toggleSpeed = 0.5f;

    private void Start()
    {
        soundToggleState = soundToggleStatePP.GetLatestValue() == 1;
        SetMixer();

    }
    private void SetMixer()
    {
        if (soundToggleState)
        {
            soundOnSnapshot.TransitionTo(toggleSpeed);
            soundButtonToggleImage.sprite = soundOnSprite;
        }
        else
        {
            soundOffSnapshot.TransitionTo(toggleSpeed);
            soundButtonToggleImage.sprite = soundOffSprite;
        }
        soundToggleStatePP.SetValue(soundToggleState?1:0);
    }
    public void ToggleSound()
    {
        soundToggleState = !soundToggleState;
        SetMixer();
    }

}
