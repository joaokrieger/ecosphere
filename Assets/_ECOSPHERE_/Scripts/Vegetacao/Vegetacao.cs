using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vegetacao : MonoBehaviour
{
    public GameObject prefabGrama; // O prefab de grama que será spawnado
    public int numeroDeGrama; // Número de instâncias de grama que você deseja spawnar
    public Tilemap tilemapCampo; // O Tilemap da camada de campo
    public Vector2 areaSpawnMin; // Coordenadas mínimas da área de spawn
    public Vector2 areaSpawnMax; // Coordenadas máximas da área de spawn
    public float intervaloSpawn; // Intervalo de tempo entre cada spawn

    // Start é chamado antes da primeira atualização do quadro
    void Start()
    {
        StartCoroutine(RotinaSpawnGrama());
    }

    // Update é chamado uma vez por quadro
    void Update()
    {

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

            // Converte a posição do mundo para a posição da célula do tilemap
            Vector3Int posicaoCelula = tilemapCampo.WorldToCell(posicaoAleatoria);

            // Verifica se há um tile na posição da célula no tilemap de campo
            if (tilemapCampo.HasTile(posicaoCelula))
            {
                // Converte a posição da célula de volta para a posição do mundo
                Vector3 posicaoSpawn = tilemapCampo.CellToWorld(posicaoCelula) + tilemapCampo.tileAnchor;

                // Instancia o prefab de grama na posição aleatória
                Instantiate(prefabGrama, posicaoSpawn, Quaternion.identity);
                gramaSpawnada++;
            }

            // Espera pelo intervalo de tempo antes de spawnar novamente
            yield return new WaitForSeconds(intervaloSpawn);
        }
    }
}
