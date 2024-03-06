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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform nearestIceberg = NearestIceberg();
        if (nearestIceberg == null)
        {
            Debug.Log("Null Iceberg");
            arponHead.LookAt(initialTransform.position);
        }
        else
        {
            arponHead.LookAt(new Vector3(nearestIceberg.position.x, transform.position.y, nearestIceberg.position.z));
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
