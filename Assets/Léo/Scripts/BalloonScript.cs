using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    void Start() {
        transform.DOMove(target.position, 10f);
    }
    
}
