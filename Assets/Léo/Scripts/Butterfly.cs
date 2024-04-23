using UnityEngine;

public class Butterfly : MonoBehaviour
{
    [SerializeField] private GameObject butterfly;
    [SerializeField] private Transform anchor;

    private void Update() {
        if (!(Vector3.Distance(transform.position, anchor.position) < 0.1f)) return;
        butterfly.SetActive(true);
        Destroy(gameObject);
    }
}
