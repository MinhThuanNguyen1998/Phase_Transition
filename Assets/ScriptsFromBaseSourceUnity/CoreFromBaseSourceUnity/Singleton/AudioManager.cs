using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum SFXType
{
    ButtonClick,
    StickObject
}
[Serializable] 
public struct SoundSFXData
{
    public SFXType Key;
    public AudioClip Sound;
}
public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;   // Nhạc nền
    [SerializeField] private AudioSource sfxSource;     // Hiệu ứng (SFX)
    [SerializeField] private AudioSource narrationSource;

    [Header("Clips")]
    [SerializeField] private List<AudioClip> narrationClips;
    [SerializeField] private List<SoundSFXData> soundsFXes;
    private readonly Dictionary<SFXType, AudioClip> SoundSFXMap = new();

}
