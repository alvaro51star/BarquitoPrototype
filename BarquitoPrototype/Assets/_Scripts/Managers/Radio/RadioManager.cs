using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    [Header("AudioSources:")]
    [SerializeField] private AudioSource m_musicAudioSource;
    [SerializeField] private AudioSource m_callAudioSource;
    [SerializeField] private float m_musicInCallVolume;

    [Header("Song list:")]
    [SerializeField] private List<AudioClip> m_clipList;

    private int m_songClip = 0;
    private float m_musicVolume;

    private void Start()
    {
        m_musicVolume = m_musicAudioSource.volume;
    }
    public void PlayStopMusic()
    {
        if(m_musicAudioSource.isPlaying)
        {
            m_musicAudioSource.Stop();
        }
        else
        {
            PlaySong();
        }
    }

    public void StartCall()
    {
        m_musicAudioSource.volume = m_musicInCallVolume;
        m_callAudioSource.Play();
    }

    public void EndCall()
    {
        m_musicAudioSource.volume = m_musicVolume;
        m_callAudioSource.Stop();
    }

    public void NextSong()
    {
        m_songClip++;
        PlaySong();
    }

    public void LastSong()
    {
        m_songClip--;
        PlaySong();
    }

    private void PlaySong()
    {
        m_musicAudioSource.clip = m_clipList[m_songClip];
        m_callAudioSource.Play();
    }
}
