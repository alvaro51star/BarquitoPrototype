using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float steerForce = 3f;
    [SerializeField] private float maxAngleSpeed = 5f;
    //[SerializeField] private float steerDirection;
    [SerializeField] private Transform motorPosition;
    public float m_slow;
    private BoatManager m_boatManager;

    Vector2 playerInput;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_boatManager = GetComponent<BoatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
    }

    private void FixedUpdate()
    {
        if (playerInput[0] > 0)
        {
            m_boatManager.LowerGas();
            rb.AddForce(playerInput[0] * speed * transform.forward, ForceMode.Force);
            rb.AddForceAtPosition(-playerInput[1] * steerForce * transform.right, motorPosition.position, ForceMode.Force);
        }

        
        //rb.AddTorque(playerInput[1] * steerForce * Vector3.up, ForceMode.Force);

        //Deja de rotar cuando deja de pulsar el boton
        if (playerInput[1] == 0)
        {
            rb.angularVelocity -= rb.angularVelocity * Time.fixedDeltaTime;
        }
        //Frena cuando deja de pulsar el boton
        if (playerInput[0] <= 0)
        {
            rb.velocity -= rb.velocity * Time.fixedDeltaTime;
            rb.angularVelocity -= rb.angularVelocity * Time.fixedDeltaTime;
        }

        if (rb.angularVelocity.magnitude >= maxAngleSpeed)
        {
            rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, maxAngleSpeed);
        }

        if (rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        rb.velocity *= m_slow;
    }
}
