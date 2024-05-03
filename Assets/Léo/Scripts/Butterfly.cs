using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Butterfly : MonoBehaviour
{
    public void OnSelectEnter(object sender, SelectEnterEventArgs e) {
        var handRef = e.interactorObject.transform.GetComponent<HandEvent>();
        handRef.GrabButterfly();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hand")) Destroy(gameObject);
    }
}