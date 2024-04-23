using DG.Tweening;
using UnityEngine;

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
    public string DeathWish;
    public DeathColor DeathColor;
    private DeathColor deathColorInstance;

    public void SetDeathColorInstance(DeathColor deathColor)
    {
        deathColorInstance = deathColor;
    }
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
        DeathWish = randomHexColor;

        // Le reste de tes animations reste inchangé
        shakePositionTween = transform.DOShakePosition(30f, 1f, 15);
        shakeRotationTween = transform.DOShakeRotation(30f, 15f, 15);

        float randomMoveDuration = Random.Range(20f, 40f);

        // Animation de déplacement du cube vers le centre de la carte avec la durée aléatoire
        moveTween = transform.DOMove(centerOfMap, randomMoveDuration);
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
        if (other.CompareTag("Monolithe"))
        {
            // Récupère le script DeathColor du monolithe
            DeathColor monolitheDeathColor = other.GetComponent<DeathColor>();
            if (monolitheDeathColor != null)
            {
                // Passe la référence à ce script oui2 au monolithe
                monolitheDeathColor.ballonScript = this;

                // Assigne la référence à l'instance de DeathColor dans oui2
                SetDeathColorInstance(monolitheDeathColor);
            }

            Invoke("Dela1", 0.5f);
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
            Invoke("Dela2", 3f);
        }
    }

    void Dela1()
    {
        if (deathColorInstance != null)
        {
            // Appeler la méthode AutomnColor de l'instance de DeathColor
            deathColorInstance.AutomnColor();
        }
        Destroy(gameObject);
    }
    void Dela2()
    {
        Destroy(gameObject);
    }

}
