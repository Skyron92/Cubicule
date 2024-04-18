using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientSkyBox : MonoBehaviour
{
    public Color couleurDebut = Color.grey; // Couleur de départ du dégradé
    public Color couleurFinale = Color.blue; // Couleur finale du dégradé
    public float dureeChangement = 217f; // Durée du changement de couleur en secondes

    private float tempsEcoule = 0f;
    public bool canChangeColor = false;

    private void Start()
    {
        RenderSettings.skybox.SetColor("_Tint", couleurDebut);
        StartCoroutine(WaitForColorChange());
    }

    void Update()
    {
        if (canChangeColor == true)
        {
            // Calculer le ratio en utilisant la fonction f(x) = x^2 * 0.5, bornée entre 0 et 3.40
            float ratio = Mathf.Clamp01(Mathf.Pow(tempsEcoule / dureeChangement, 2) * 0.5f * 3.40f);

            // Appliquer la couleur en utilisant le ratio calculé
            RenderSettings.skybox.SetColor("_Tint", Color.Lerp(couleurDebut, couleurFinale, ratio));

            tempsEcoule += Time.deltaTime;

            if (tempsEcoule >= dureeChangement)
            {
                tempsEcoule = 0f; // Réinitialiser le temps écoulé pour permettre une boucle continue du dégradé
            }
        }
    }

    public void ActivateSkyChange()
    {
        StartCoroutine(WaitForColorChange());//Méthode à appeler quand on veut lancer le changement de couyleur du ciel
    }

    IEnumerator WaitForColorChange()
    {
        yield return new WaitForSeconds(60);
        canChangeColor = true;
    }
}
