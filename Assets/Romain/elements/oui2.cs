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
        // Tableau des codes hexadécimaux des couleurs possibles
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

        // Choisir aléatoirement un code hexadécimal parmi le tableau
        string randomHexColor = hexColors[Random.Range(0, hexColors.Length)];

        // Animation de clignotement de l'émission avec la couleur aléatoire
        emissionTween = DOTween.To(() => cubeMaterial.GetColor("_EmissionColor"), color => cubeMaterial.SetColor("_EmissionColor", color), HexToColor(randomHexColor), 30f)
           .SetEase(Ease.InOutQuad)
           .SetLoops(-1, LoopType.Yoyo);

        // Animation de changement de couleur du cube avec la couleur aléatoire
        colorTween = cubeRenderer.material.DOColor(HexToColor(randomHexColor), 30f);

        // Le reste de tes animations reste inchangé
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
        if (other.gameObject.CompareTag("Ground"))
        {
            //Destroy(gameObject);
        }
        if (other.CompareTag("SphereCollision"))
        {
            if (!isCollided)
            {
                isCollided = true;
                float randomDelay = Random.Range(2.5f, 8.5f);
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

        // Active la gravité pour que le cube tombe
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
        }

        Debug.Log("Animations stopped.");
    }
}
