using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Variables
    public static UIManager instance;
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

    public void DesgasteFillImage(float actualState, float maxState)
    {
        float resultado = actualState / maxState;
        m_desgasteImg.fillAmount = resultado;
        Debug.Log("img fill: " + resultado);
    }
}
