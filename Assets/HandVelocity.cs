using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandVelocity : MonoBehaviour
{
    [SerializeField] private InputActionReference velocityInputActionReference;

    private InputAction VelocityIA => velocityInputActionReference.action;
    private Vector3 Value => VelocityIA.ReadValue<Vector3>();

    private void Awake()
    {
        VelocityIA.Enable();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ballon"))
        {
            var rb = other.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Value;
        }
    }
}
