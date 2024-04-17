using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlants : MonoBehaviour
{
    public float facteurCroissance = 10f; // Facteur d'échelle pour déterminer la taille finale
    public float facteurCroissanceDeux = 10f;

    private Vector3 tailleInitiale;
    private Vector3 tailleFinale;
    public float tempsCroissance = 6f;
    public float tempsCroissanceDeux = 10f;
    private Vector3 tailleInitialeDeux;
    private Vector3 tailleFinaleDeux;

    public GameObject premierObjet;
    public GameObject deuxiemeObjet;

    public bool canGrowNext = false; //permet de savoir si j'ai un deuxième truc à faire grandir 

    private void Start()
    {
        if (premierObjet != null)
        {
            tailleInitiale = premierObjet.transform.localScale;
            tailleFinale = tailleInitiale * facteurCroissance;
            StartCoroutine(CroissanceCoroutine());
        }

    }

    private IEnumerator CroissanceCoroutine()
    {
        float tempsEcoule = 0f;

        while (tempsEcoule < tempsCroissance)
        {
            tempsEcoule += Time.deltaTime;
            float ratioCroissance = Mathf.Clamp01(tempsEcoule / tempsCroissance);
            premierObjet.transform.localScale = Vector3.Lerp(tailleInitiale, tailleFinale, ratioCroissance);

            yield return null; // Attendre la prochaine frame
        }
        if (canGrowNext == true)
        {
            tailleInitialeDeux = deuxiemeObjet.transform.localScale;
            tailleFinaleDeux = tailleInitialeDeux * facteurCroissanceDeux;
            StartCoroutine(CroissanceDeuxieme());
        }
 
    }

    private IEnumerator CroissanceDeuxieme()
    {
        float tempsEcouleY = 0f;
        float tempsEcouleXZ = 0f;

        // Calcul de la taille initiale de l'objet par rapport à sa taille locale
        Vector3 tailleInitialeLocale = Vector3.Scale(tailleInitialeDeux, deuxiemeObjet.transform.localScale);

        // Phase 1 : Croissance sur l'axe Y
        while (tempsEcouleY < tempsCroissance)
        {
            tempsEcouleY += Time.deltaTime;
            float ratioCroissanceY = Mathf.Clamp01(tempsEcouleY / tempsCroissanceDeux);
            Vector3 nouvelleTailleY = Vector3.Lerp(tailleInitialeLocale, new Vector3(0f, tailleInitialeLocale.y, 0.1f), ratioCroissanceY);
            deuxiemeObjet.transform.localScale = nouvelleTailleY;

            yield return null; // Attendre la prochaine frame
        }

        // Phase 2 : Croissance sur les axes X et Z
        while (tempsEcouleXZ < tempsCroissance)
        {
            tempsEcouleXZ += Time.deltaTime;
            float ratioCroissanceXZ = Mathf.Clamp01(tempsEcouleXZ / tempsCroissance);
            Vector3 nouvelleTailleXZ = Vector3.Lerp(new Vector3(tailleInitialeLocale.x, deuxiemeObjet.transform.localScale.y, tailleInitialeLocale.z), tailleFinaleDeux, ratioCroissanceXZ);
            deuxiemeObjet.transform.localScale = nouvelleTailleXZ;

            yield return null; // Attendre la prochaine frame
        }
    }
}