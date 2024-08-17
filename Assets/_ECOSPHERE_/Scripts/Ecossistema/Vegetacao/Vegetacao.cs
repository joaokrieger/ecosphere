using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vegetacao : MonoBehaviour
{
    public int numeroDeGrama;
    public GameObject gramaPrefab;
    public Vector2 areaSpawnMin;
    public Vector2 areaSpawnMax;
    public float intervaloSpawn;

    private Tilemap tilemapCampo;
    private Tilemap tilemapEstrutura;
    private SpawnAnimal spawnAnimal;

    void Start()
    {

        GameObject campoObject = GameObject.FindWithTag("Campo");
        tilemapCampo = campoObject.GetComponent<Tilemap>();

        GameObject estruturaObject = GameObject.FindWithTag("Estruturas");
        tilemapEstrutura = estruturaObject.GetComponent<Tilemap>();
        spawnAnimal = new SpawnAnimal();

        StartCoroutine(RotinaSpawnGrama());
    }

    IEnumerator RotinaSpawnGrama()
    {
        int gramaSpawnada = 0;

        while (gramaSpawnada < numeroDeGrama)
        {
            // Gera uma posição aleatória dentro da área de spawn
            float randomX = Random.Range(areaSpawnMin.x, areaSpawnMax.x);
            float randomY = Random.Range(areaSpawnMin.y, areaSpawnMax.y);
            Vector3 posicaoAleatoria = new Vector3(randomX, randomY, 0);
            Vector3Int posicaoCelula = tilemapCampo.WorldToCell(posicaoAleatoria);

            if (tilemapCampo.HasTile(posicaoCelula) && !tilemapEstrutura.HasTile(posicaoCelula))
            {
                Vector3 posicaoSpawn = tilemapCampo.CellToWorld(posicaoCelula) + tilemapCampo.tileAnchor;
                spawnAnimal.SpawnGrama(gramaPrefab, posicaoSpawn);
                gramaSpawnada++;
            }

            yield return new WaitForSeconds(intervaloSpawn);
        }
    }
}
