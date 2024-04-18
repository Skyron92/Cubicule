using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimManager : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private InputActionReference iaRef;
    private static readonly int Grip = Animator.StringToHash("Grip");

    private InputAction HandInputAction => iaRef.action;
    // Start is called before the first frame update
    void Awake()
    {
        HandInputAction.Enable();
        HandInputAction.started += context => animator.SetFloat(Grip, 1); 
        HandInputAction.canceled += context => animator.SetFloat(Grip, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

internal class SerializedAttribute : Attribute
{
}
