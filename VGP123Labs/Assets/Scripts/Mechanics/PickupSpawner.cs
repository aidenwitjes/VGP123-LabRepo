using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCollectibleSpawner : MonoBehaviour
{
    public GameObject[] collectiblePrefabs; // Array to hold the collectible prefabs
    public Transform[] spawnLocations; // Array to hold the spawn locations

    void Start()
    {
        if (collectiblePrefabs.Length == 0 || spawnLocations.Length == 0)
        {
            Debug.LogError("Collectible prefabs or spawn locations are not assigned!");
            return;
        }

        // Ensure we have at least 5 spawn locations
        if (spawnLocations.Length < 5)
        {
            Debug.LogError("There must be at least 5 spawn locations!");
            return;
        }

        SpawnCollectibles();
    }

    void SpawnCollectibles()
    {
        // Shuffle the spawn locations array to ensure randomness
        ShuffleArray(spawnLocations);

        // Spawn a random collectible at each of the first 5 spawn locations
        for (int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(0, collectiblePrefabs.Length);
            Instantiate(collectiblePrefabs[randomIndex], spawnLocations[i].position, Quaternion.identity);
        }
    }

    void ShuffleArray(Transform[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}