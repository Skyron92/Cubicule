using DG.Tweening;
using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    [SerializeField] private Transform target1, target2;
    [SerializeField] private float duration1, duration2;
    
    void Awake() {
        transform.DOMove(target1.position, duration1).SetEase(Ease.OutSine).onComplete += () => transform.DOMove(target2.position, duration2).SetEase(Ease.InSine);
    }
    
}
