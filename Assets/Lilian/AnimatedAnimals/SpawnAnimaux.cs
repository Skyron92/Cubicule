using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnAnimaux : MonoBehaviour
{
    public GameObject[] animalPrefabs; // Tableau des prefabs d'animaux disponibles
    public Vector3 mapSize = new Vector3(120f, 0f, 120f); // Taille de la carte
    public int numberOfCubes = 10; // Nombre de cubes � instancier

    // Changez la visibilit� de la m�thode Start() � public
    public void Start()
    {
        // G�n�rer une position al�atoire sur la carte
        Vector3 randomPosition = GenerateRandomPosition();

        // Choisir al�atoirement un prefab d'animal
        GameObject randomAnimalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];

        // Spawn l'animal � la position al�atoire
        Instantiate(randomAnimalPrefab, randomPosition, Quaternion.identity);

        Invoke("SayHello", 1f);
    }

    Vector3 GenerateRandomPosition()
    {
        // G�n�rer des coordonn�es al�atoires dans les limites de la carte
        float randomX = Random.Range(-mapSize.x / 2f, mapSize.x / 2f);
        float randomZ = Random.Range(-mapSize.z / 2f, mapSize.z / 2f);

        // La hauteur Y peut �tre ajust�e selon tes besoins
        float randomY = 0;

        // Retourner les coordonn�es al�atoires dans un Vector3
        return new Vector3(randomX, randomY, randomZ);
    }
}