using UnityEngine;
using System.Collections; // N'oublie pas d'importer cet espace de noms

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab; // Prefab du cube � spawn
    public Vector3 mapSize = new Vector3(120f, 10f, 120f); // Taille de la carte

    void Start()
    {
        // Lancer la coroutine pour faire spawn les cubes
        StartCoroutine(SpawnCubesRoutine());
    }

    IEnumerator SpawnCubesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Attendre 5 secondes

            SpawnCubeRandomPosition();
        }
    }

    void SpawnCubeRandomPosition()
    {
        Debug.Log("oui");
        // G�n�rer une position al�atoire sur la carte
        Vector3 randomPosition = GenerateRandomPosition();

        // Spawn un cube � la position al�atoire
        Instantiate(cubePrefab, randomPosition, Quaternion.identity);
    }

    Vector3 GenerateRandomPosition()
    {
        // G�n�rer des coordonn�es al�atoires dans les limites de la carte
        float randomX = Random.Range(-mapSize.x / 2f, mapSize.x / 2f);
        float randomZ = Random.Range(-mapSize.z / 2f, mapSize.z / 2f);

        // La hauteur Y peut �tre ajust�e selon tes besoins
        float randomY = Random.Range(4f, 15);

        // Retourner les coordonn�es al�atoires dans un Vector3
        return new Vector3(randomX, randomY, randomZ);
    }
}