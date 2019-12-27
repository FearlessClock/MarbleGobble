using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    
    private void Awake() {
    }
    public void PlayEffect(AudioClip clip){
        source.PlayOneShot(clip, source.volume);
    }
}
