using System.Collections;
using UnityEngine;

public class GradientSkyBox : MonoBehaviour
{
    public Color couleurDebut = Color.grey; // Couleur de d�part du d�grad�
    public Color couleurFinale = Color.blue; // Couleur finale du d�grad�
    public float dureeChangement = 217f; // Dur�e du changement de couleur en secondes

    private float tempsEcoule = 0f;
    public bool canChangeColor = false;

    private void Start()
    {
        RenderSettings.skybox.SetColor("_Tint", couleurDebut);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (canChangeColor)
        {
            // Calculer le ratio en utilisant la fonction f(x) = x^2 * 0.5, born�e entre 0 et 3.40
            float ratio = Mathf.Clamp01(Mathf.Pow(tempsEcoule / dureeChangement, 2) * 0.5f * 3.40f);

            // Appliquer la couleur en utilisant le ratio calcul�
            RenderSettings.skybox.SetColor("_Tint", Color.Lerp(couleurDebut, couleurFinale, ratio));

            tempsEcoule += Time.deltaTime;
        }
    }

    public void ActivateSkyChange()
    {
        StartCoroutine(WaitForColorChange());//M�thode � appeler quand on veut lancer le changement de couyleur du ciel
    }

    IEnumerator WaitForColorChange()
    {
        yield return new WaitForSeconds(60);
        canChangeColor = true;
    }
}
