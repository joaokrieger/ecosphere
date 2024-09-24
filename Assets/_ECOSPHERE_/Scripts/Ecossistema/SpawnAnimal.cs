using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimal : MonoBehaviour
{
    public GameObject coelhoPrefab;
    public GameObject raposaPrefab;
    public GameObject javaliPrefab;
    public GameObject loboPrefab;
    public GameObject ursoPrefab;
    public GameObject cervoPrefab;

    public Vector3 spawnPosition;
    public float spawnRadius = 5f;

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;

        Vector3 randomPosition = spawnPosition + new Vector3(randomPoint.x, randomPoint.y, 0);
        return randomPosition;
    }

    public bool VerificaSaldoSpawn(GameObject prefab)
    {
        Animal animal = prefab.GetComponent<Animal>();
        if (GameManager.Instance.RemoveSaldo(animal.GetPrecoSpawn()))
        {
            return true;
        }
        return false;
    }

    public void SpawnUrso()
    {
        if (VerificaSaldoSpawn(ursoPrefab))
        {
            Instantiate(ursoPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        }
    }

    public void SpawnLobo()
    {
        if (VerificaSaldoSpawn(loboPrefab))
        {
            Instantiate(loboPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        }
    }

    public void SpawnCoelho()
    {
        if (VerificaSaldoSpawn(coelhoPrefab))
        {
            Instantiate(coelhoPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        }
    }

    public void SpawnRaposa()
    {
        if (VerificaSaldoSpawn(raposaPrefab))
        {
            Instantiate(raposaPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        }
    }

    public void SpawnJavali()
    {
        if (VerificaSaldoSpawn(javaliPrefab))
        {
            Instantiate(javaliPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        }
    }

    public void SpawnCervo()
    {
        if (VerificaSaldoSpawn(cervoPrefab))
        {
            Instantiate(cervoPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        }
    }

    public static void SpawnGrama(GameObject gramaPrefab, Vector3 posicaoSpawn)
    {
        Instantiate(gramaPrefab, posicaoSpawn, Quaternion.identity);
    }
}
