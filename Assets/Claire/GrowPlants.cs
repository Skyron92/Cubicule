using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrowPlants : MonoBehaviour
{
    public float facteurCroissance = 10f; // Facteur d'échelle pour déterminer la taille finale
    public float facteurCroissanceDeux = 10f;

    public float tempsCroissanceMin = 6f;
    public float tempsCroissanceMax = 6f;
    public float tempsCroissanceDeuxMin = 10f;
    public float tempsCroissanceDeuxMax = 10f;
    public float croissanceAmount;
    public float croissanceAmountDeux;

    public GameObject premierObjet;
    public GameObject deuxiemeObjet;

    public bool canGrowNext = false; //permet de savoir si j'ai un deuxième truc à faire grandir 

    private void Start()
    {
        if (canGrowNext != true)
        {
            GrowUp();
        }
        else GrowUp2();
    }

    private void GrowUp()
    {
        premierObjet.transform.DOScale(croissanceAmount, Random.Range(tempsCroissanceMin, tempsCroissanceMax));
    }

    private void GrowUp2() 
    {
        premierObjet.transform.DOScale(croissanceAmount, Random.Range(tempsCroissanceMin, tempsCroissanceMax)).onComplete += () => deuxiemeObjet.transform.DOScaleY(croissanceAmountDeux, Random.Range(tempsCroissanceDeuxMin, tempsCroissanceDeuxMax)).onComplete += () => deuxiemeObjet.transform.DOScale(1, 5);
    }
}