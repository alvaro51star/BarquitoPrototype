using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxSpeedForward = 20f;
    [SerializeField] private float maxSpeedBackward = 5f;
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
        if (playerInput[0] != 0)
        {
            m_boatManager.LowerGas();
        }

        #region Movimiento

        rb.AddForce(playerInput[0] * speed * transform.forward, ForceMode.Force);

        if (playerInput[0] > 0)
        {
            rb.AddForceAtPosition(-playerInput[1] * steerForce * transform.right, motorPosition.position, ForceMode.Force);
            
            if (rb.velocity.magnitude >= maxSpeedForward)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedForward);
            }
        }
        else if (playerInput[0] < 0)
        {
            rb.AddForceAtPosition(playerInput[1] * steerForce * transform.right, motorPosition.position, ForceMode.Force);

            if (rb.velocity.magnitude >= maxSpeedBackward)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedBackward);
            }
        }
        else
        {
            rb.AddForceAtPosition(-playerInput[1] * steerForce * transform.right, motorPosition.position, ForceMode.Force);
        }

        #endregion

        //rb.AddTorque(playerInput[1] * steerForce * Vector3.up, ForceMode.Force);

        //Deja de rotar cuando deja de pulsar el boton
        if (playerInput[1] == 0)
        {
            rb.angularVelocity -= rb.angularVelocity * Time.fixedDeltaTime;
        }
        //Frena cuando deja de pulsar el boton
        if (playerInput[0] == 0)
        {
            rb.velocity -= rb.velocity * Time.fixedDeltaTime;
            //rb.angularVelocity -= rb.angularVelocity * Time.fixedDeltaTime;
        }

        if (rb.angularVelocity.magnitude >= maxAngleSpeed)
        {
            rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, maxAngleSpeed);
        }

        rb.velocity *= m_slow;
    }

    public float GetForwardAngleRadar()
    {
        return transform.eulerAngles.y;
    }
}
