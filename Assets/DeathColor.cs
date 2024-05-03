using UnityEngine;
using DG.Tweening;

public class DeathColor : MonoBehaviour
{
    Renderer cubeRenderer;
    Material cubeMaterial;
    Tween colorTween;
    Tween alphaTween; // Ajout d'une variable pour stocker le tween de transparence
    Collider objectCollider; // Ajout d'une variable pour le collider


    // Ajoute une r�f�rence au script oui2
    public oui2 ballonScript;
    public oui2 HexC;

    void Start()
    {
        // R�cup�re le composant Renderer attach� � cet objet
        cubeRenderer = GetComponent<Renderer>();
        // R�cup�re le composant Material du Renderer
        cubeMaterial = cubeRenderer.sharedMaterial;

        // D�finit la couleur du mat�riau en gris
        cubeMaterial.color = Color.grey;
        cubeMaterial.color = HexToColor("#2B2B2C");
    }
    public void AutomnColor()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeMaterial = cubeRenderer.sharedMaterial;

        Debug.Log(ballonScript);
        Debug.Log(ballonScript.DeathWish);
        // Utilise la r�f�rence au script oui2 pour r�cup�rer la couleur du ballon
        colorTween = cubeRenderer.sharedMaterial.DOColor(HexToColor(ballonScript.DeathWish), 4f);

        // Lancer le script SpawnAnimaux apr�s un d�lai de 4 secondes
        Invoke("SpawnAnimalsAfterDelay", 0.5f);
    }

    void SpawnAnimalsAfterDelay()
    {
        // Trouve le composant SpawnAnimaux et appelle sa m�thode Start pour lancer le spawn des animaux
        FindObjectOfType<SpawnAnimaux>().Start();
    }
    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}