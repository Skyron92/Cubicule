using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandEvent : MonoBehaviour
{
    [SerializeField] private GameObject butterfly;
    [SerializeField] private GameObject butterflyPrefab;

    [SerializeField] private InputActionReference gripInputActionReference;
    private InputAction GripInputAction => gripInputActionReference.action;

    private void Awake()
    {
        GripInputAction.Enable();
        GripInputAction.canceled += context => ReleaseButterfly();
    }

    private void ReleaseButterfly()
    {
        butterfly.SetActive(false);
        Instantiate(butterflyPrefab);
    }

    public void GrabButterfly() {
        butterfly.SetActive(true);
    }
}