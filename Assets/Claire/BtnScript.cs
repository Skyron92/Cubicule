using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnScript : MonoBehaviour
{

    public GameObject objet1;
    public GameObject objet2;
    public GameObject objet3;
    public void StartSpreading()
    {
        Debug.Log("Que je spread");
        StartCoroutine(Benjamin());
    }

    IEnumerator Benjamin()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Je suis le benji");
        objet1.SetActive(true);
        yield return new WaitForSeconds(3);
        objet2.SetActive(true);
        yield return new WaitForSeconds(3);
        objet3.SetActive(true);
    }
}
