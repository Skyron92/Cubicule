using UnityEngine;
using DG.Tweening;

public class oui2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Secouer le cube avec une intensité de 1, une durée de 1 seconde et 10 vibratos par seconde
        transform.DOShakePosition(1f, 5f, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
