using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class SpawnAnimaux : MonoBehaviour
{
    public GameObject[] animalPrefabs; // Tableau des prefabs d'animaux disponibles
    public Vector3 mapSize = new Vector3(120f, 0f, 120f); // Taille de la carte
    public int numberOfCubes = 10; // Nombre de cubes � instancier

    private void Update()
    {
        if(Input.GetButtonDown("Jump")) Spawn();
    }

    // Changez la visibilite de la methode Start() a public
    public void Spawn()
    {
        // Generer une position aleatoire sur la carte
        Vector3 randomPosition = GenerateRandomPosition();

        // Choisir aleatoirement un prefab d'animal
        GameObject randomAnimalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];

        // Spawn l'animal � la position al�atoire
        Instantiate(randomAnimalPrefab, randomPosition, Quaternion.identity);

        Invoke("SayHello", 1f);
    }

    Vector3 GenerateRandomPosition()
    {
        // Generer des coordonnees aleatoires dans les limites de la carte
        float randomX = Random.Range(-mapSize.x / 2f, mapSize.x / 2f);
        float randomZ = Random.Range(-mapSize.z / 2f, mapSize.z / 2f);

        // La hauteur Y peut �tre ajust�e selon tes besoins
        float randomY = 0.03f;

        // Retourner les coordonnees aleatoires dans un Vector3
        return new Vector3(randomX, randomY, randomZ);
    }
}