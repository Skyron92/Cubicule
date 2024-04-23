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
        // Récupère le composant Collider attaché à cet objet
        objectCollider = GetComponent<Collider>();
    }
    public void AutomnColor()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeMaterial = cubeRenderer.sharedMaterial;

        Debug.Log(ballonScript);
        Debug.Log(ballonScript.DeathWish);
        // Utilise la référence au script oui2 pour récupérer la couleur du ballon
        colorTween = cubeRenderer.sharedMaterial.DOColor(HexToColor(ballonScript.DeathWish), 4f);
    }

    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}