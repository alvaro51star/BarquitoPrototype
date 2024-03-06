using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIceBehaviour : MonoBehaviour
{
    //Variables
    [SerializeField] BoatMovement m_boatMovement;
    [SerializeField] BoatManager m_boatManager;
    [SerializeField] private float m_secondsToCollider;
    private IceBehaviour m_currentIce;

    private void OnTriggerEnter(Collider other)
    {
        m_currentIce = other.GetComponent<IceBehaviour>();
        if (m_currentIce != null)
        {
            m_currentIce.BreakIce();
            CalculateSlow(other.attachedRigidbody);
            m_currentIce.ApagarCollider();
            m_currentIce = null;
            StartCoroutine(ReleaseIce());
        }
    }

    private IEnumerator ReleaseIce()
    {
        yield return new WaitForSeconds(m_secondsToCollider);
        m_boatMovement.m_slow = 1;
    }

    private void CalculateSlow(Rigidbody ice)
    {
        float slow = gameObject.GetComponent<Rigidbody>().mass / ice.mass;
        Debug.Log("slow: " + slow);
        m_boatMovement.m_slow = slow;
        m_boatManager.Desgaste(ice.mass / gameObject.GetComponent<Rigidbody>().mass);
    }
}
