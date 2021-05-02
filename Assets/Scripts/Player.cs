using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    PlayerActions controls;
    [SerializeField] float velocidad = 5f;
    [SerializeField] Rigidbody rb;

    void Awake()
    {
        controls = new PlayerActions();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        controls.PlayerControls.Boost.performed += _ => Boost();
        controls.PlayerControls.Movement.performed += _ => Move();
    }

    void Move()
    {
        //Debug.Log("a nmms nos movemos");
        rb.position += MovementAxis * velocidad * Time.deltaTime;
        
    }

    void Boost()
    {
        Debug.Log("va muy rapido nmms");
    }

    void Update()
    {
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
    Vector3 MovementAxis => new Vector3(Axis.x, 0f, Axis.y);
    Vector3 Direction => rb.rotation * MovementAxis;

}
