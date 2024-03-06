using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class ArponBehaviour : MonoBehaviour
{
    public List<Transform> nearIcebergs;

    [SerializeField] private Transform arponHead;
    [SerializeField] private Transform initialTransform;

    [SerializeField] private Transform forwardDirection;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private BoatMovement boatMovement;
    [SerializeField] private Transform shootPoint;
    private bool isArponActive = false;

    [SerializeField] private float atractionForce = 5f;
    private Transform nearestIceberg;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        nearestIceberg = NearestIceberg();
        if (nearestIceberg == null)
        {
            //Debug.Log("Null Iceberg");
            arponHead.LookAt(initialTransform.position);
            lineRenderer.SetPosition(1, shootPoint.position);
            isArponActive = false;
        }
        else
        {
            arponHead.LookAt(new Vector3(nearestIceberg.position.x, arponHead.position.y, nearestIceberg.position.z));
        }

        //Toggle Arpon
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleArpon(nearestIceberg);
        }

        if (!isArponActive)
        {
            lineRenderer.SetPosition(1, shootPoint.position);
        }

        lineRenderer.SetPosition(0, shootPoint.position);
    }

    private void FixedUpdate()
    {
        if (isArponActive)
        {
            Vector3 force = nearestIceberg.position - boatMovement.transform.position;
            force.Normalize();
            //Debug.Log("Fuerza: " + force);
            //Debug.Log("Posicion Player: " + boatMovement.transform.position);
            force *= atractionForce;
            boatMovement.AddForceToMovement(new Vector3(force.x, 0, force.z));
        }   
    }



    private void ToggleArpon(Transform nearestIceberg)
    {
        if (!nearestIceberg)
        {
            return;
        }

        isArponActive = !isArponActive;
        if (isArponActive)
        {
            lineRenderer.SetPosition(1, NearestIceberg().position);
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.SetPosition(1, shootPoint.position);
        }
    }

    private Transform NearestIceberg()
    {
        if (nearIcebergs.Count <= 0)
        {
            return null;
        }

        Transform nearestIceberg = nearIcebergs[0];
        float currentMinDistance = Vector3.Distance(transform.position, nearestIceberg.position);
        foreach (Transform iceberg in nearIcebergs)
        {
            float distanceToIceberg = Vector3.Distance(transform.position, iceberg.position);
            if (distanceToIceberg < currentMinDistance)
            {
                currentMinDistance = distanceToIceberg;
                nearestIceberg = iceberg;
            }
        }
        return nearestIceberg;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Iceberg"))
        {
            nearIcebergs.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Iceberg"))
        {
            nearIcebergs.Remove(other.transform);
        }
    }
}
