using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        // D�marrez le tweener pour faire grandir l'objet
        transform.DOScale(Vector3.one, 9f); // Vector3.one repr�sente le scale (1, 1, 1)
    }
}
