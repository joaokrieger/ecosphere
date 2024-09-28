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

    public bool VerificaSaldoSpawn(GameObject prefab)
    {
        Animal animal = prefab.GetComponent<Animal>();
        if (GameManager.Instance.RemoveSaldo(animal.GetPrecoSpawn()))
        {
            return true;
        }

        tutorialController.ShowTutorialPontoVida();
        return false;
    }

    public void SpawnUrso()
    {
        if (VerificaSaldoSpawn(ursoPrefab))
        {
            Instantiate(ursoPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            AudioManager.instance.PlayAnimal("Urso");
        }
    }

    public void SpawnLobo()
    {
        if (VerificaSaldoSpawn(loboPrefab))
        {
            Instantiate(loboPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            AudioManager.instance.PlayAnimal("Lobo");
        }
    }

    public void SpawnCoelho()
    {
        if (VerificaSaldoSpawn(coelhoPrefab))
        {
            Instantiate(coelhoPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            AudioManager.instance.PlayAnimal("Coelho");
        }
    }

    public void SpawnRaposa()
    {
        if (VerificaSaldoSpawn(raposaPrefab))
        {
            Instantiate(raposaPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            AudioManager.instance.PlayAnimal("Raposa");
        }
    }

    public void SpawnJavali()
    {
        if (VerificaSaldoSpawn(javaliPrefab))
        {
            Instantiate(javaliPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            AudioManager.instance.PlayAnimal("Javali");
        }
    }

    public void SpawnCervo()
    {
        if (VerificaSaldoSpawn(cervoPrefab))
        {
            Instantiate(cervoPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            AudioManager.instance.PlayAnimal("Cervo");
        }
    }

    public static void SpawnGrama(GameObject gramaPrefab, Vector3 posicaoSpawn)
    {
        Instantiate(gramaPrefab, posicaoSpawn, Quaternion.identity);
    }
}
