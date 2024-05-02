using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnAnimaux : MonoBehaviour
{
    public GameObject[] animalPrefabs; // Tableau des prefabs d'animaux disponibles
    public Vector3 mapSize = new Vector3(120f, 0f, 120f); // Taille de la carte
    public int numberOfCubes = 10; // Nombre de cubes à instancier

    // Changez la visibilité de la méthode Start() à public
    public void Start()
    {
        // Générer une position aléatoire sur la carte
        Vector3 randomPosition = GenerateRandomPosition();

        // Choisir aléatoirement un prefab d'animal
        GameObject randomAnimalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];

        // Spawn l'animal à la position aléatoire
        Instantiate(randomAnimalPrefab, randomPosition, Quaternion.identity);

        Invoke("SayHello", 1f);
    }

    Vector3 GenerateRandomPosition()
    {
        // Générer des coordonnées aléatoires dans les limites de la carte
        float randomX = Random.Range(-mapSize.x / 2f, mapSize.x / 2f);
        float randomZ = Random.Range(-mapSize.z / 2f, mapSize.z / 2f);

        // La hauteur Y peut être ajustée selon tes besoins
        float randomY = 0;

        // Retourner les coordonnées aléatoires dans un Vector3
        return new Vector3(randomX, randomY, randomZ);
    }
}