using UnityEngine;
using DG.Tweening;

public class DeathColor : MonoBehaviour
{
    Renderer cubeRenderer;
    Material cubeMaterial;
    Tween colorTween;
    Tween alphaTween; // Ajout d'une variable pour stocker le tween de transparence
    Collider objectCollider; // Ajout d'une variable pour le collider


    // Ajoute une référence au script oui2
    public oui2 ballonScript;
    public oui2 HexC;

    void Start()
    {
        // Récupère le composant Renderer attaché à cet objet
        cubeRenderer = GetComponent<Renderer>();
        // Récupère le composant Material du Renderer
        cubeMaterial = cubeRenderer.sharedMaterial;

        // Définit la couleur du matériau en gris
        cubeMaterial.color = Color.grey;
        cubeMaterial.color = HexToColor("#2B2B2C");
    }
    public void AutomnColor()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeMaterial = cubeRenderer.sharedMaterial;

        Debug.Log(ballonScript);
        Debug.Log(ballonScript.DeathWish);
        // Utilise la référence au script oui2 pour récupérer la couleur du ballon
        colorTween = cubeRenderer.sharedMaterial.DOColor(HexToColor(ballonScript.DeathWish), 4f);

        // Lancer le script SpawnAnimaux après un délai de 4 secondes
        Invoke("SpawnAnimalsAfterDelay", 0.5f);
    }

    void SpawnAnimalsAfterDelay()
    {
        // Trouve le composant SpawnAnimaux et appelle sa méthode Start pour lancer le spawn des animaux
        FindObjectOfType<SpawnAnimaux>().Spawn();
    }
    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}