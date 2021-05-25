using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class CarController : NetworkBehaviour
{
    PlayerActions controls;
    public Rigidbody theRB;
    public Transform cameraTransform;

    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 180f, gravityForce = 10f, dragOnGround = 3f, boost = 5f;

    private float speedInput, turnInput, auxSpeed;

    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;

    public ParticleSystem[] dustTrail;
    public float maxEmission = 25f;
    private float emissionRate;


    // Start is called before the first frame update

    void Awake()
    {
        controls = new PlayerActions();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        if(IsLocalPlayer)
        {
            controls.PlayerControls.Boost.performed += _ => Boost();
            controls.PlayerControls.Boost.canceled += _ => CancelBoost();
            controls.PlayerControls.Movement.performed += _ => Move();
            controls.PlayerControls.Movement.canceled += _ => CancelMove();
            theRB.transform.parent = null;
        }
        else
        {
            cameraTransform.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsLocalPlayer) return;
        speedInput = 0f;
        Move();

        transform.position = theRB.transform.position;
    }
    void Move()
    {
        //Debug.Log("move");
        speedInput = Axis.y * forwardAccel * 1000f;

        turnInput = Axis.x;

        if(grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Axis.y, 0f));
        }
    }
    void CancelMove()
    {
        speedInput = Axis.y * reverseAccel * 1000f;
    }

    void Boost()
    {
        auxSpeed = forwardAccel;
        forwardAccel += boost;
    }

    void CancelBoost()
    {
        forwardAccel = auxSpeed;
    }

    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        if(Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        emissionRate = 0;

        if(grounded)
        {
            theRB.drag = dragOnGround;
            if(Mathf.Abs(speedInput) > 0)
            {
                theRB.AddForce(transform.forward * speedInput);

                emissionRate = maxEmission;
            }
        } else
        {
            theRB.drag = 0.1f;

            theRB.AddForce(Vector3.up * -gravityForce * 100f);
        }

        foreach(ParticleSystem part in dustTrail)
        {
            var emissionModule = part.emission;

            emissionModule.rateOverTime = emissionRate;
        }
    }

    Vector2 Axis => controls.PlayerControls.Movement.ReadValue<Vector2>();
}
