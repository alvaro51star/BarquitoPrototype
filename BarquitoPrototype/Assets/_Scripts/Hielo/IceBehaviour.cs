using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animation))]
public class IceBehaviour : MonoBehaviour
{
    //Variables
    [SerializeField] private GameObject m_intactIce, m_brokenIce;
    private Animation m_iceAnimation;
    private Collider m_collider;

    private void Start()
    {
        m_iceAnimation = GetComponent<Animation>();
        m_collider = GetComponent<Collider>();
    }

    public void BreakIce()
    {
        m_intactIce.SetActive(false);
        m_brokenIce.SetActive(false);
        //m_collider.enabled = false;
        //m_iceAnimation.Play();
        //gameObject.SetActive(false);
    }
}
