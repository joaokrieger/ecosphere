using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimal : MonoBehaviour
{

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
        
    }

    public void SpawnCervo()
    {
        if (VerificaSaldoSpawn(cervoPrefab))
        { 
            Instantiate(cervoPrefab, spawnPosition, Quaternion.identity);
            GameController.GetInstance().Add(GameController.Entidade.Presa);
        }
    }

    public void SpawnGrama(GameObject gramaPrefab, Vector3 posicaoSpawn)
    {
        Instantiate(gramaPrefab, posicaoSpawn, Quaternion.identity);
        GameController.GetInstance().Add(GameController.Entidade.Produtor);
    }
}
