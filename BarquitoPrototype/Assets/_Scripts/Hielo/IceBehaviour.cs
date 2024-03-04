using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animation))]
public class IceBehaviour : MonoBehaviour
{
    //Variables
    [SerializeField] private GameObject m_intactIce, m_brokenIce;
    private Animation m_iceAnimation;

    private void Start()
    {
        m_iceAnimation = GetComponent<Animation>();
    }

    public void BreakIce()
    {
        m_intactIce.SetActive(false);
        m_brokenIce.SetActive(false);
        //m_iceAnimation.Play();
        //gameObject.SetActive(false);
    }
}
