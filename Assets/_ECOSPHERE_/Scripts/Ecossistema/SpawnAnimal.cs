using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimal : MonoBehaviour
{
    [Header("Tutorial")]
    public TutorialController tutorialController;

    [Header("Painel de Prefabs de Animais")]
    public GameObject coelhoPrefab;
    public GameObject raposaPrefab;
    public GameObject javaliPrefab;
    public GameObject loboPrefab;
    public GameObject ursoPrefab;
    public GameObject cervoPrefab;

    [Header("Posição do Spawn")]
    public Vector3 spawnPosition;
    public float spawnRadius = 5f;

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;

        Vector3 randomPosition = spawnPosition + new Vector3(randomPoint.x, randomPoint.y, 0);
        return randomPosition;
    }

    public void SpawnUrso()
    {
        Instantiate(ursoPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        AudioManager.instance.PlayAnimal("Urso");
    }

    public void SpawnLobo()
    {
        Instantiate(loboPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        AudioManager.instance.PlayAnimal("Lobo");
    }

    public void SpawnCoelho()
    {
        Instantiate(coelhoPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        AudioManager.instance.PlayAnimal("Coelho");
    }

    public void SpawnRaposa()
    {
        Instantiate(raposaPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        AudioManager.instance.PlayAnimal("Raposa");
    }

    public void SpawnJavali()
    {
        Instantiate(javaliPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        AudioManager.instance.PlayAnimal("Javali");
    }

    public void SpawnCervo()
    {
        Instantiate(cervoPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        AudioManager.instance.PlayAnimal("Cervo");
    }

    public static void SpawnGrama(GameObject gramaPrefab, Vector3 posicaoSpawn)
    {
        Instantiate(gramaPrefab, posicaoSpawn, Quaternion.identity);
    }
}
