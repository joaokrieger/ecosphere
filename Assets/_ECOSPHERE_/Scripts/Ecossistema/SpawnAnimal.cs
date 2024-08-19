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

    public bool VerificaSaldoSpawn(GameObject prefab)
    {
        Animal animal = prefab.GetComponent<Animal>();
        if (GameController.GetInstance().GetCarteiraPontoVida().RemoveSaldo(animal.GetPrecoSpawn()))
        {
            return true;
        }
        return false;
    }

    public void SpawnUrso()
    {
        if (VerificaSaldoSpawn(ursoPrefab))
        {
            Instantiate(ursoPrefab, spawnPosition, Quaternion.identity);
            GameController.GetInstance().Add(GameController.Entidade.Predador);
        }
    }

    public void SpawnLobo()
    {
        if (VerificaSaldoSpawn(loboPrefab))
        {
            Instantiate(loboPrefab, spawnPosition, Quaternion.identity);
            GameController.GetInstance().Add(GameController.Entidade.Predador);
        }
    }

    public void SpawnCoelho()
    {
        if (VerificaSaldoSpawn(coelhoPrefab))
        {
            Instantiate(coelhoPrefab, spawnPosition, Quaternion.identity);
            GameController.GetInstance().Add(GameController.Entidade.Presa);
        }
    }

    public void SpawnRaposa()
    {
        if (VerificaSaldoSpawn(raposaPrefab))
        {
            Instantiate(raposaPrefab, spawnPosition, Quaternion.identity);
            GameController.GetInstance().Add(GameController.Entidade.Predador);
        }
    }

    public void SpawnJavali()
    {
        if (VerificaSaldoSpawn(javaliPrefab))
        {
            Instantiate(javaliPrefab, spawnPosition, Quaternion.identity);
            GameController.GetInstance().Add(GameController.Entidade.Presa);
        }
    }

    public void SpawnCervo()
    {
        if (VerificaSaldoSpawn(cervoPrefab))
        { 
            Instantiate(cervoPrefab, spawnPosition, Quaternion.identity);
            GameController.GetInstance().Add(GameController.Entidade.Presa);
        }
    }

    public static void SpawnGrama(GameObject gramaPrefab, Vector3 posicaoSpawn)
    {
        Instantiate(gramaPrefab, posicaoSpawn, Quaternion.identity);
        GameController.GetInstance().Add(GameController.Entidade.Produtor);
    }
}
