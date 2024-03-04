using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Variables
    public static UIManager instance;
    [Header("Testing:")]
    [SerializeField] private bool mouseLimited;

    [Header("UI Gameobjects:")]
    [SerializeField] private GameObject pauseMenu;

    [Header("Desgaste")]
    [SerializeField] private TextMeshProUGUI m_desgasteText;
    [SerializeField] private Image m_desgasteImg;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        if (mouseLimited)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void DesgasteFillImage(float actualState, float maxState)
    {
        float resultado = actualState / maxState;
        m_desgasteImg.fillAmount = resultado;
        Debug.Log("img fill: " + resultado);
    }

    //pause menu
    public void Resume()
    {
        pauseMenu.SetActive(false);

        if (mouseLimited)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Time.timeScale = 1f;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        if (mouseLimited)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
