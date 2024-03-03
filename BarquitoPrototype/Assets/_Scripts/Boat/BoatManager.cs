using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    //Variables
    [SerializeField] private float m_desgaste;

    public void Desgaste(float iceMassPorcentage)
    {
        m_desgaste -= iceMassPorcentage;
    }
}
