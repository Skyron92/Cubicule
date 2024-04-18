using UnityEngine;
using DG.Tweening;

public class DeathColor : MonoBehaviour
{
    Renderer cubeRenderer;
    Material cubeMaterial;
    Tween colorTween;

    // Ajoute une r�f�rence au script oui2
    public oui2 ballonScript;
    public oui2 HexC;

    public void AutomnColor()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeMaterial = cubeRenderer.sharedMaterial;

        Debug.Log(ballonScript);
        Debug.Log(ballonScript.DeathWish);
        // Utilise la r�f�rence au script oui2 pour r�cup�rer la couleur du ballon
        colorTween = cubeRenderer.sharedMaterial.DOColor(HexToColor(ballonScript.DeathWish), 4f);
    }

    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}