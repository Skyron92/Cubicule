using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientSkyBox : MonoBehaviour
{
    public Color couleurDebut = Color.grey; // Couleur de d�part du d�grad�
    public Color couleurFinale = Color.blue; // Couleur finale du d�grad�
    public float dureeChangement = 30f; // Dur�e du changement de couleur en secondes

    private float tempsEcoule = 0f;
    private bool stop;

    private void Start()
    {
        RenderSettings.skybox.SetColor("_Tint", couleurDebut);
    }

    void Update()
    {
        if (stop != true)
        {
            tempsEcoule += Time.deltaTime;
            RenderSettings.skybox.SetColor("_Tint", Color.Lerp(couleurDebut, couleurFinale, tempsEcoule / dureeChangement));
        }

        if (tempsEcoule >= dureeChangement)
        {
            stop = true;
        }
    }
}
