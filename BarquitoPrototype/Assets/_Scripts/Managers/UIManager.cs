using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Variables
    [Header("Desgaste")]
    [SerializeField] private TextMeshProUGUI m_desgasteText;
    [SerializeField] private Image m_desgasteImg;

    public void DesgasteFillImage(float actualState, float maxState)
    {
        m_desgasteImg.fillAmount = actualState / maxState;
    }
}
