using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject m_callOn;
    [SerializeField] private GameObject m_songOn;

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
    private void OnEnable()
    {
        EventManager.StartDialogue += StartCall;
        EventManager.EndDialogue += EndCall;
    }
    private void OnDisable()
    {
        EventManager.StartDialogue -= StartCall;
        EventManager.EndDialogue -= EndCall;
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
        m_songOn.SetActive(true);
        m_callAudioSource.Play();
        DialogueManager.instance.EnterDialogueMode();
    }

    public void EndCall()
    {
        m_musicAudioSource.volume = m_musicVolume;
        m_songOn.SetActive(false);
        m_callAudioSource.Stop();
    }

    public void NextSong()
    {
        m_songClip++;
        if(m_songClip >= m_clipList.Count)
        {
            m_songClip = 0;
        }
        PlaySong();
    }

    public void PreviousSong()
    {
        m_songClip--;
        if(m_songClip < 0)
        {
            m_songClip = m_clipList.Count - 1;
        }
        PlaySong();
    }

    private void PlaySong()
    {
        Debug.Log(m_songClip);
        m_musicAudioSource.clip = m_clipList[m_songClip];
        m_musicAudioSource.Play();
    }
}
