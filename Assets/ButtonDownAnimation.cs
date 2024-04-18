using DG.Tweening;
using UnityEngine;

public class ButtonDownAnimation : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float duration;

    public void MoveDown() {
        transform.DOMoveY(distance, duration);
    }
}
