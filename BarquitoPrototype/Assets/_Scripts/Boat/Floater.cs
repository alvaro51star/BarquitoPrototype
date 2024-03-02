using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float depthBeforeSubmerge = 1f;
    [SerializeField] private float displacementAmount = 3f;

    private void FixedUpdate()
    {
        if (transform.position.y < 0f)
        {
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerge) * displacementAmount;
            rb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), ForceMode.Acceleration);    
        }
    }
}
