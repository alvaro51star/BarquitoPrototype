using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    //Variables
    [SerializeField] private float m_currentBienestarBarco;
    [SerializeField] private float m_maxBienestarBarco;
    [SerializeField] private float m_desgasteMultiplier;

    private void Start()
    {
        m_currentBienestarBarco = m_maxBienestarBarco;
    }

    public void Desgaste(float iceMassPorcentage)
    {
        m_currentBienestarBarco -= iceMassPorcentage * m_desgasteMultiplier;
        UIManager.instance.DesgasteFillImage(m_currentBienestarBarco, m_maxBienestarBarco);
    }
}
