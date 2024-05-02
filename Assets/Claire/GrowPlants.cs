using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrowPlants : MonoBehaviour
{
    public float facteurCroissance = 10f; // Facteur d'�chelle pour d�terminer la taille finale
    public float facteurCroissanceDeux = 10f;
    public List<AudioClip> audioClips;
    public AudioSource audioSource;

    public float tempsCroissanceMin = 6f;
    public float tempsCroissanceMax = 6f;
    public float tempsCroissanceDeuxMin = 10f;
    public float tempsCroissanceDeuxMax = 10f;
    public float croissanceAmount;
    public float croissanceAmountDeux;

    public GameObject premierObjet;
    public GameObject deuxiemeObjet;

    public bool canGrowNext = false; //permet de savoir si j'ai un deuxi�me truc � faire grandir 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (canGrowNext != true)
        {
            GrowUp();
            Debug.Log("J'ai lanc� la fonction !");
        }
        else GrowUp2();

        
    }

    private void GrowUp()
    {

        premierObjet.transform.DOScale(croissanceAmount, Random.Range(tempsCroissanceMin, tempsCroissanceMax));
        Debug.Log(premierObjet.transform.lossyScale);
    }

    private void GrowUp2()
    {
        Debug.Log("je suis"+ audioSource.gameObject);
        Debug.Log("Je suis la deuxi�me animation");
        premierObjet.transform.DOScale(croissanceAmount, Random.Range(tempsCroissanceMin, tempsCroissanceMax))
            .onComplete += () => {
            deuxiemeObjet.transform
                .DOScaleY(croissanceAmountDeux, Random.Range(tempsCroissanceDeuxMin, tempsCroissanceDeuxMax))
                .onComplete += () => {
                deuxiemeObjet.transform.DOScale(1, 5);
                audioSource.clip = audioClips[Random.Range(0, 1)];
                audioSource.volume = 0.1f;
                audioSource.Play();
            };
        };
    }
}