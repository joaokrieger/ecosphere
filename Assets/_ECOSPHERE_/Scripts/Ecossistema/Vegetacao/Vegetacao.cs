using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vegetacao : MonoBehaviour
{
    public int numeroDeGrama = 100;
    public int spawnInicial = 50;
    public GameObject gramaPrefab;
    public Vector2 areaSpawnMin;
    public Vector2 areaSpawnMax;
    public float intervaloSpawn = 1f;

    private Tilemap tilemapCampo;
    private Tilemap tilemapEstrutura;

    void Start()
    {
        GameObject campoObject = GameObject.FindWithTag("Campo");
        tilemapCampo = campoObject.GetComponent<Tilemap>();
        GameObject estruturaObject = GameObject.FindWithTag("Estruturas");
        tilemapEstrutura = estruturaObject.GetComponent<Tilemap>();
        SpawnInicialGrama();
        StartCoroutine(RotinaSpawnGrama());
    }

    public void SpawnInicialGrama()
    {
        int gramas = GameObject.FindGameObjectsWithTag("Grama").Length;
        if (gramas == 0)
        {
            for (int i = 0; i < spawnInicial; i++)
            {
                SpawnGrama();
            }
        }
    }

    IEnumerator RotinaSpawnGrama()
    {
        int gramas = GameObject.FindGameObjectsWithTag("Grama").Length;
        while (gramas < numeroDeGrama)
        {
            SpawnGrama();
            yield return new WaitForSeconds(intervaloSpawn); 
        }
    }

    void SpawnGrama()
    {
        bool conseguiuSpawnar = false;
        while (!conseguiuSpawnar)
        {
            float randomX = Random.Range(areaSpawnMin.x, areaSpawnMax.x);
            float randomY = Random.Range(areaSpawnMin.y, areaSpawnMax.y);
            Vector3 posicaoAleatoria = new Vector3(randomX, randomY, 0);
            Vector3Int posicaoCelula = tilemapCampo.WorldToCell(posicaoAleatoria);

            if (tilemapCampo.HasTile(posicaoCelula) && !tilemapEstrutura.HasTile(posicaoCelula))
            {
                Vector3 posicaoSpawn = tilemapCampo.CellToWorld(posicaoCelula) + tilemapCampo.tileAnchor;
                Instantiate(gramaPrefab, posicaoSpawn, Quaternion.identity);
                conseguiuSpawnar = true;
            }
        }
    }

}
