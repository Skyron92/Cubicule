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
        Debug.Log("start");
        StartAnimations();
    }

    void StartAnimations()
    {
        // Tableau des codes hexad�cimaux des couleurs possibles
        string[] hexColors = new string[]
        {
        "#FF602D", 
        "#FFA62D",
        "#FFDA2D",
        "#F3FF2D",
        "#800000",
        "#ff7f00",
        "#960018"
        };

        // Choisir al�atoirement un code hexad�cimal parmi le tableau
        string randomHexColor = hexColors[Random.Range(0, hexColors.Length)];

        // Animation de clignotement de l'�mission avec la couleur al�atoire
        emissionTween = DOTween.To(() => cubeMaterial.GetColor("_EmissionColor"), color => cubeMaterial.SetColor("_EmissionColor", color), HexToColor(randomHexColor), 30f)
           .SetEase(Ease.InOutQuad)
           .SetLoops(-1, LoopType.Yoyo);

        // Animation de changement de couleur du cube avec la couleur al�atoire
        colorTween = cubeRenderer.material.DOColor(HexToColor(randomHexColor), 30f);

        // Le reste de tes animations reste inchang�
        shakePositionTween = transform.DOShakePosition(30f, 1f, 15);
        shakeRotationTween = transform.DOShakeRotation(30f, 15f, 15);

        float randomMoveDuration = Random.Range(20f, 40f);

        // Animation de d�placement du cube vers le centre de la carte avec la dur�e al�atoire
        moveTween = transform.DOMove(centerOfMap, randomMoveDuration)
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
        Debug.Log("Les cubes sont arriv�s au centre de la carte !");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("SphereCollision"))
        {
            if (!isCollided)
            {
                isCollided = true;
                float randomDelay = Random.Range(4.5f, 10f);
                Invoke("StopAnimations", randomDelay);
            }
        }
    }

    void StopAnimations()
    {
        // Arr�te les tweens associ�s aux animations
        emissionTween.Kill();
        colorTween.Kill();
        shakePositionTween.Kill();
        shakeRotationTween.Kill();
        moveTween.Kill();

        // Active la gravit� pour que le cube tombe
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
        }

        Debug.Log("Animations stopped.");
    }
}
