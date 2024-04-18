using UnityEngine;
using DG.Tweening;

public class oui2 : MonoBehaviour
{
    Renderer cubeRenderer;
    Material cubeMaterial;
    Vector3 centerOfMap;
    bool isCollided = false;
    Tween emissionTween;
    Tween colorTween;
    Tween shakePositionTween;
    Tween shakeRotationTween;
    Tween moveTween;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeMaterial = cubeRenderer.material;

        centerOfMap = CalculateCenterOfMap();

        StartAnimations();
    }

    void StartAnimations()
    {
        emissionTween = DOTween.To(() => cubeMaterial.GetColor("_EmissionColor"), color => cubeMaterial.SetColor("_EmissionColor", color), HexToColor("#F08315"), 30f)
           .SetEase(Ease.InOutQuad)
           .SetLoops(-1, LoopType.Yoyo);

        colorTween = cubeRenderer.material.DOColor(HexToColor("#F08315"), 30f);

        shakePositionTween = transform.DOShakePosition(30f, 0.5f, 10);
        shakeRotationTween = transform.DOShakeRotation(30f, 10f, 10);

        moveTween = transform.DOMove(centerOfMap, 30f)
            .OnComplete(CheckPosition);
    }

    Vector3 CalculateCenterOfMap()
    {
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        foreach (Renderer renderer in FindObjectsOfType<Renderer>())
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds.center;
    }

    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    void CheckPosition()
    {
        Debug.Log("Les cubes sont arrivés au centre de la carte !");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SphereCollision"))
        {
            if (!isCollided)
            {
                isCollided = true;
                float randomDelay = Random.Range(0.5f, 1.5f);
                Invoke("StopAnimations", randomDelay);
            }
        }
    }

    void StopAnimations()
    {
        // Arrête les tweens associés aux animations
        emissionTween.Kill();
        colorTween.Kill();
        shakePositionTween.Kill();
        shakeRotationTween.Kill();
        moveTween.Kill();

        Debug.Log("Animations stopped.");
    }
}