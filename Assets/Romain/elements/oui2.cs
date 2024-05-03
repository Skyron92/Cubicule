using DG.Tweening;
using UnityEngine;

public class oui2 : MonoBehaviour
{
    Renderer cubeRenderer;
    Material cubeMaterial;
    Material particleMaterial;
    ParticleSystem ParticleSystem;
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
    public GameObject particleEffectPrefab;
    private bool _isGrab;

    public void SetDeathColorInstance(DeathColor deathColor)
    {
        deathColorInstance = deathColor;
    }
    void Start()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        cubeRenderer = GetComponent<Renderer>();
        cubeMaterial = cubeRenderer.material;

        centerOfMap = CalculateCenterOfMap();
        StartAnimations();
    }

    void StartAnimations()
    {
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

        string randomHexColor = hexColors[Random.Range(0, hexColors.Length)];

        emissionTween = DOTween.To(() => cubeMaterial.GetColor("_EmissionColor"), color => cubeMaterial.SetColor("_EmissionColor", color), HexToColor(randomHexColor), 30f)
           .SetEase(Ease.InOutQuad)
           .SetLoops(-1, LoopType.Yoyo);

        colorTween = cubeRenderer.material.DOColor(HexToColor(randomHexColor), 30f);
        DeathWish = randomHexColor;

        shakePositionTween = transform.DOShakePosition(30f, 1f, 15);
        shakeRotationTween = transform.DOShakeRotation(30f, 15f, 15);

        float randomMoveDuration = Random.Range(40f, 60f);

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
                if(_isGrab) return;
                isCollided = true;
                float randomDelay = Random.Range(4.5f, 10f);
                Invoke("StopAnimations", randomDelay);
            }
        }
        if (other.CompareTag("Monolithe"))
        {
            DeathColor monolitheDeathColor = other.GetComponent<DeathColor>();
            if (monolitheDeathColor != null)
            {
                monolitheDeathColor.ballonScript = this;

                SetDeathColorInstance(monolitheDeathColor);
            }

            Invoke("Dela1", 0.5f);
        }

    }

    public void Release() {
        _isGrab = false;
    }

    public void Grab()
    {
        _isGrab = true;
    }
    
    public void StopAnimations() {
        emissionTween.Kill();
        colorTween.Kill();
        shakePositionTween.Kill();
        shakeRotationTween.Kill();
        moveTween?.Kill();

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            if(_isGrab) return;
            Invoke("Dela2", 3.49f);
            Destroy(gameObject, 3.5f);
        }
    }

    void Dela1()
    {
         if (deathColorInstance != null)
        {
            deathColorInstance.AutomnColor();
        }

        Destroy(gameObject);
    }

    private void Dela2()
    {
        if (particleEffectPrefab != null)
        {
            // Instancier l'effet de particule
            GameObject particleEffectInstance = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);

            if (particleEffectInstance != null)
            {
                Renderer particleRenderer = particleEffectInstance.GetComponent<Renderer>();

                if (particleRenderer != null)
                {
                    Color hexColor = HexToColor(DeathWish);

                    particleRenderer.material.SetColor("_BaseColor", hexColor);
                }
            }
        }
    }
}
