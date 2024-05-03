using UnityEngine;
using System.Collections; // N'oublie pas d'importer cet espace de noms

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab; // Prefab du cube à spawn
    public Vector3 mapSize = new Vector3(120f, 10f, 120f); // Taille de la carte
    private bool BaBalloon = false;

    void Start()
    {
        Invoke("BonjourCoroutine",5);
    }

    void BonjourCoroutine()
    {
        StartCoroutine(SpawnCubesRoutine());
    }
    IEnumerator SpawnCubesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f); // Attendre 5 secondes

            SpawnCubeRandomPosition();
        }
    }

    void SpawnCubeRandomPosition()
    {
        // Générer une position aléatoire sur la carte
        Vector3 randomPosition = GenerateRandomPosition();

        // Spawn un cube à la position aléatoire
        Instantiate(cubePrefab, randomPosition, Quaternion.identity);
    }

    Vector3 GenerateRandomPosition()
    {
        // Générer des coordonnées aléatoires dans les limites de la carte
        float randomX = Random.Range(-mapSize.x / 2f, mapSize.x / 2f);
        float randomZ = Random.Range(-mapSize.z / 2f, mapSize.z / 2f);

        // La hauteur Y peut être ajustée selon tes besoins
        float randomY = Random.Range(5f, 10);

        // Retourner les coordonnées aléatoires dans un Vector3
        return new Vector3(randomX, randomY, randomZ);
    }
}