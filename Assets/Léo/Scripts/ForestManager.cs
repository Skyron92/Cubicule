using System.Collections.Generic;
using UnityEngine;

public class ForestManager : MonoBehaviour
{
    private float _time;
    [SerializeField] private List<GameObject> blocs = new List<GameObject>();
    [SerializeField, Range(0, 60)] private float step;
    private int currentIndex;
    
    
    private void Update() {
        _time += Time.deltaTime;
        if (_time >= step) {
            _time = 0;
            blocs[currentIndex].SetActive(true);
            currentIndex++;
        }
    }
}