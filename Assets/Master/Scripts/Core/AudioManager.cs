using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;
public enum SoundType
{
    Button,
    Popup,
    Beep,
    Pumping
    
}
public class AudioMainManager : SingletonMain<AudioMainManager>
{
    [Header("Audio Effects")]
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_AudioPopupClip;
    [SerializeField] private AudioClip m_AudioButtonClip;
    [SerializeField] private AudioClip m_AudioBeepClip;
    [SerializeField] private AudioClip m_AudioPumpingClip;
   
    private Dictionary<SoundType, AudioClip> m_SoundMap;
    private void Awake()
    {
        m_SoundMap = new Dictionary<SoundType, AudioClip>
        {
            { SoundType.Button, m_AudioButtonClip },
            { SoundType.Popup, m_AudioPopupClip },
            { SoundType.Beep,m_AudioBeepClip },
            { SoundType.Pumping,m_AudioPumpingClip },
        };
    }
    public void PlayOnShot(SoundType soundType)
    {
        if (m_SoundMap.TryGetValue(soundType, out var clip) && clip != null)
            m_AudioSource.PlayOneShot(clip);
        else
            Debug.LogWarning($"AudioManager: AudioClip for {soundType} is not assigned.");
    }
    public void PlayLoop(SoundType soundType)
    {
        if (m_SoundMap.TryGetValue(soundType, out var clip) && clip != null && m_AudioSource != null)
        {
            m_AudioSource.loop = true;
            m_AudioSource.clip = clip;
            m_AudioSource.Play();
        }
    }
    public void StopSound(SoundType type) => m_AudioSource?.Stop();
    public void PlayAudioIntroduction(AudioClip clip)
    {
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
    }
    public void StopLoop()
    {
        if (m_AudioSource != null)
        {
            m_AudioSource.Stop();
            m_AudioSource.loop = false;
            m_AudioSource.clip = null;
        }
    }
}
