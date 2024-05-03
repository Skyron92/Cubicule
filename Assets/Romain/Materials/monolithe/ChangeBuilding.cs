using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeBuilding : MonoBehaviour
{
    Renderer cubeRenderer;
    Material cubeMaterial;
    Material particleMaterial;
    Tween emissionTween;
    Tween colorTween;
    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeMaterial = cubeRenderer.material;
        EventManager.current.AnimationStarted += (sender, args) => StartAnimations2();
    }

    public void StartAnimations2()
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

        colorTween = cubeRenderer.material.DOColor(HexToColor(randomHexColor), 300f);
    }

    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}
