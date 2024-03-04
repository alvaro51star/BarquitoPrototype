using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    //Variables
    [Header("Desgaste")]
    [SerializeField] private float m_currentBienestarBarco;
    [SerializeField] private float m_maxBienestarBarco;
    [SerializeField] private float m_desgasteMultiplier;
    [SerializeField] private int m_secondsToFirstHit;
    private bool m_primerGolpe = true;
    private IEnumerator m_primerGolpeCorutina;

    [Header("Tags")]
    [SerializeField] private string m_repostarTag = "";

    private void Start()
    {
        ArreglarBarco();
        m_primerGolpeCorutina = PrimerGolpeCorutina();
    }

    public void Desgaste(float iceMassPorcentage)
    {
        if (m_primerGolpe)
        {
            m_currentBienestarBarco -= iceMassPorcentage * m_desgasteMultiplier;
            m_primerGolpe = false;
            Debug.Log("hacesesto?");
        }
        else
        {
            m_currentBienestarBarco -= iceMassPorcentage * (m_desgasteMultiplier / 2);
            StopCoroutine(m_primerGolpeCorutina);
        }
        StartCoroutine(m_primerGolpeCorutina);
        UIManager.instance.DesgasteFillImage(m_currentBienestarBarco, m_maxBienestarBarco);
    }

    private IEnumerator PrimerGolpeCorutina()
    {
        yield return new WaitForSeconds(m_secondsToFirstHit);
        Debug.Log("primer golpe true");
        m_primerGolpe = true;

    }

    public void ArreglarBarco()
    {
        m_currentBienestarBarco = m_maxBienestarBarco;
        UIManager.instance.DesgasteFillImage(m_currentBienestarBarco, m_maxBienestarBarco);
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if (tag == m_repostarTag)
        {
            ArreglarBarco();
        }
    }
}
