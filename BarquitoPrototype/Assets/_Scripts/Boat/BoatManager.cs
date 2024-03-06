using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    //Variables
    private BoatMovement m_boatMovement;

    [Header("Desgaste")]
    [SerializeField] private float m_currentBienestarBarco;
    [SerializeField] private float m_maxBienestarBarco;
    [SerializeField] private float m_desgasteMultiplier;
    [SerializeField] private int m_secondsToFirstHit;
    private bool m_primerGolpe = true;
    private IEnumerator m_primerGolpeCorutina;

    [Header("Gasolina")]
    [SerializeField] private float m_currentGasolina;
    [SerializeField] private float m_maxGasolina;
    [SerializeField] private float m_consumoGasolina;

    [Header("Tags")]
    [SerializeField] private string m_repostarTag = "";


    private void Start()
    {
        m_boatMovement = GetComponent<BoatMovement>();
        LlenarGasolina();
        m_primerGolpeCorutina = PrimerGolpeCorutina();
        ArreglarBarco();
    }

    public void Desgaste(float iceMassPorcentage)
    {
        if (m_primerGolpe)
        {
            m_currentBienestarBarco -= iceMassPorcentage * m_desgasteMultiplier;
            ChangePrimerGolpe(false);
        }
        else
        {
            m_currentBienestarBarco -= iceMassPorcentage * (m_desgasteMultiplier / 2);
            StopCoroutine(m_primerGolpeCorutina);
            m_primerGolpeCorutina = null;
        }
        m_primerGolpeCorutina = PrimerGolpeCorutina();
        StartCoroutine(m_primerGolpeCorutina);
        UIManager.instance.DesgasteFillImage(m_currentBienestarBarco, m_maxBienestarBarco);
    }

    private IEnumerator PrimerGolpeCorutina()
    {
        yield return new WaitForSeconds(m_secondsToFirstHit);
        ChangePrimerGolpe(true);

    }

    private void ChangePrimerGolpe(bool mode)
    {

        m_primerGolpe = mode;
    }

    public void ArreglarBarco()
    {
        m_currentBienestarBarco = m_maxBienestarBarco;
        UIManager.instance.DesgasteFillImage(m_currentBienestarBarco, m_maxBienestarBarco);
        m_boatMovement.canMove = true;
        
    }


    public void LowerGas()
    {
        m_currentGasolina -= m_consumoGasolina;
        m_currentGasolina = Mathf.Clamp(m_currentGasolina, 0, m_maxGasolina);
        UIManager.instance.GasolinaFillImage(m_currentGasolina, m_maxGasolina);
        if (m_currentGasolina <= 0)
        {
            m_boatMovement.canMove = false;
        }
    }

    private void LlenarGasolina()
    {
        m_currentGasolina = m_maxGasolina;
        UIManager.instance.GasolinaFillImage(m_currentGasolina, m_maxGasolina);
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if (tag == m_repostarTag)
        {
            ArreglarBarco();
            LlenarGasolina();
        }
        else if (tag == "DialogueTrigger")
        {
            EventManager.StartDialogue?.Invoke();
            other.gameObject.SetActive(false);
        }
    }
}
