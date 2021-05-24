using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class Player : NetworkBehaviour
{
    PlayerActions controls;
    [SerializeField] float velocidadMaxima = 20f;
    [SerializeField] float boost = 30f;
    float velocidadBase = 0f;
    float velocidadTotal = 0f;
     Rigidbody rb;

    float rotacion;

    void Awake()
    {
        controls = new PlayerActions();
        rb = GetComponent<Rigidbody>();
        //velocidadTotal = velocidadMaxima;
    }
    void Start()
    {
        if(IsLocalPlayer)
        {
            controls.PlayerControls.Boost.performed += _ => Boost();
            controls.PlayerControls.Boost.canceled += _ => CancelBoost();
            controls.PlayerControls.Movement.performed += _ => Move();
        }
    }


    void Move()
    {
        
        if(velocidadTotal < velocidadMaxima)
        {
            velocidadTotal += velocidadMaxima/100 * velocidadMaxima/100;
        }
        Debug.Log(velocidadTotal);
        rotacion += Axis.x;
        rb.rotation = Quaternion.Euler(rb.rotation.x, rotacion, rb.rotation.z);
        rb.position +=  Direction * velocidadTotal * Time.deltaTime;
    }

    void Boost()
    {
        Debug.Log("va muy rapido nmms");
        velocidadTotal = boost;
    }

    void CancelBoost()
    {
        velocidadTotal = velocidadMaxima;
    }

    void Update()
    {
        //if(!NetworkManager.Singleton.IsHost) return;
        if(!IsLocalPlayer) return;
            Move();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    Vector2 Axis => controls.PlayerControls.Movement.ReadValue<Vector2>();
    Vector3 MovementAxis => new Vector3(0, 0f, Axis.y);
    Vector3 Direction => rb.rotation * MovementAxis;

}
