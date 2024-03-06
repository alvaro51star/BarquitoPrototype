using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    //Variables
    public static DialogueManager instance;

    private Story m_currentStory;
    private bool m_dialogueIsPlaying;

    [SerializeField] private float m_timeToChangeDialogue;
    [SerializeField] private TextAsset m_dialogoInicio;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI m_dialogeText;
    [SerializeField] private GameObject m_dialogePanel;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
            instance = this;
    }

    [ContextMenu("Start Dialogue")]
    public void EnterDialogueMode()
    {
        m_currentStory = new Story(m_dialogoInicio.text);
        m_dialogueIsPlaying = true;
        DialogueSwitchMode(true);
        ContinueDialogue();
    }

    public void ExitDialogueMode()
    {
        m_dialogueIsPlaying = false;
        DialogueSwitchMode(false);
        DialogueChangeText("");
    }

    private void ContinueDialogue()
    {
        if (m_currentStory.canContinue)
        {
            DialogueChangeText(m_currentStory.Continue());
            StartCoroutine(CoroutineAdvanceDialogue());
        }
        else
            ExitDialogueMode();
    }
    public IEnumerator CoroutineAdvanceDialogue()
    {
        yield return new WaitForSeconds(m_timeToChangeDialogue);
        ContinueDialogue();

    }

    private void DialogueSwitchMode(bool mode)
    {
        m_dialogePanel.SetActive(mode);
    }

    private void DialogueChangeText(string text)
    {
        m_dialogeText.text = text;
    }

}
