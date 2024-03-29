using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Variables
    public static UIManager instance;
    [Header("Testing:")]
    [SerializeField] private bool mouseLimited;

    [Header("UI Menus:")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endMenu;

    [Header("Desgaste")]
    [SerializeField] private TextMeshProUGUI m_desgasteText;
    [SerializeField] private Image m_desgasteImg;

    [Header("Gasolina")]
    [SerializeField] private Image m_gasolinaImg;

    [Header("Radio")]
    [SerializeField] private GameObject m_RadioMenu;

    private List<Image> radarInstances;
    private float _prevPlayerFwd = 0;

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RadioMenu();
        }
    }

    //HUD barras
    public void DesgasteFillImage(float actualState, float maxState)
    {
        float resultado = actualState / maxState;
        m_desgasteImg.fillAmount = resultado;
    }

    public void GasolinaFillImage(float actualState, float maxState)
    {
        float resultado = actualState / maxState;
        m_gasolinaImg.fillAmount = resultado;
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

    //end menu
    public void ActivateEndMenu()
    {
        endMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void LoadSceneMainMenu()
    {
        SceneManager.LoadScene(0);//funcionara solo si esta en la scene 0
    }

    //radio buttons on RadioManager
    public void RadioMenu()
    {
        if (m_RadioMenu.activeSelf)
        {
            m_RadioMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            m_RadioMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
