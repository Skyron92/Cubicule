using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
   
{
    public GameObject Animal;
    public GameObject[] FlyCheck;
         // Start is called before the first frame update
    void Start()
    {
        Animal = GameObject.FindGameObjectWithTag<FlyAnimal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
