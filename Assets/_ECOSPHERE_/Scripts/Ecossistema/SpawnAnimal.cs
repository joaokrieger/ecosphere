using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimal : MonoBehaviour
{

    public GameObject ursoPrefab;
    public GameObject cervoPrefab;

    public Vector3 spawnPosition;

    private void Spawn() {
        
    }

    public void SpawnUrso()
    {
        Instantiate(ursoPrefab, spawnPosition, Quaternion.identity);
    }

    public void SpawnLobo()
    {
        
    }

    public void SpawnCervo()
    {
        Instantiate(cervoPrefab, spawnPosition, Quaternion.identity);
    }
}
