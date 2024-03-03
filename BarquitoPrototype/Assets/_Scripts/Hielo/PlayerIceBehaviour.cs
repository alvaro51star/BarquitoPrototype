using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIceBehaviour : MonoBehaviour
{
    //Variables
    [SerializeField] BoatMovement m_boatMovement;
    [SerializeField] BoatManager m_boatManager;

    private void OnTriggerEnter(Collider other)
    {
        IceBehaviour iceBehaviour = other.GetComponent<IceBehaviour>();
        if (iceBehaviour != null)
        {
            iceBehaviour.BreakIce();
            CalculateSlow(other.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IceBehaviour iceBehaviour = other.GetComponent<IceBehaviour>();
        if (iceBehaviour != null)
        {
            m_boatMovement.m_slow = 1;
        }
    }

    private void CalculateSlow(Rigidbody ice)
    {
        float slow = gameObject.GetComponent<Rigidbody>().mass / ice.mass;
        Debug.Log("slow: " + slow);
        m_boatMovement.m_slow = slow;
        m_boatManager.Desgaste(slow);
    }
}
