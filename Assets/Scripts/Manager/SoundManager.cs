using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClipDef> audioClipDefs;

    public enum Sound {
        Walk, Car, DeliverComplete, DeliverFail, Click, Chop, Oven, PickUp
    }

    private Dictionary<Sound, float> soundIntervalDict = new Dictionary<Sound, float>();
    private Dictionary<Sound, float> soundTimerDict = new Dictionary<Sound, float>();

    private void Awake() {
        Instance = this;

        audioClipDefs.ForEach(def => {
            if (def.interval > 0f) {
                soundIntervalDict[def.Sound] = def.interval;
                soundTimerDict[def.Sound] = 0f;
            }
        });
    }

    public void PlaySound(Sound sound) {
        if (CanPlaySound(sound)) {
            AudioClip audioClip = audioClipDefs.Find(def => def.Sound == sound)?.AudioClip;
            if (audioClip != null) {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }

    private bool CanPlaySound(Sound sound) {
        if (soundTimerDict.ContainsKey(sound)) {
            bool canPlay = soundTimerDict[sound] + soundIntervalDict[sound] < Time.time;
            if (canPlay) {
                soundTimerDict[sound] = Time.time;
            }
            return canPlay;
        } else {
            return true;
        }
    }
}

[Serializable]
public class AudioClipDef {
    public AudioClip AudioClip;
    public SoundManager.Sound Sound;
    public float interval;
}
