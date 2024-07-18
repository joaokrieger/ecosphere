using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vegetacao : MonoBehaviour
{
    public GameObject prefabGrama; // O prefab de grama que ser� spawnado
    public int numeroDeGrama; // N�mero de inst�ncias de grama que voc� deseja spawnar
    public Tilemap tilemapCampo; // O Tilemap da camada de campo
    public Vector2 areaSpawnMin; // Coordenadas m�nimas da �rea de spawn
    public Vector2 areaSpawnMax; // Coordenadas m�ximas da �rea de spawn
    public float intervaloSpawn; // Intervalo de tempo entre cada spawn

    // Start � chamado antes da primeira atualiza��o do quadro
    void Start()
    {
        StartCoroutine(RotinaSpawnGrama());
    }

    // Update � chamado uma vez por quadro
    void Update()
    {

    }

    IEnumerator RotinaSpawnGrama()
    {
        int gramaSpawnada = 0;

        while (gramaSpawnada < numeroDeGrama)
        {
            // Gera uma posi��o aleat�ria dentro da �rea de spawn
            float randomX = Random.Range(areaSpawnMin.x, areaSpawnMax.x);
            float randomY = Random.Range(areaSpawnMin.y, areaSpawnMax.y);
            Vector3 posicaoAleatoria = new Vector3(randomX, randomY, 0);

            // Converte a posi��o do mundo para a posi��o da c�lula do tilemap
            Vector3Int posicaoCelula = tilemapCampo.WorldToCell(posicaoAleatoria);

            // Verifica se h� um tile na posi��o da c�lula no tilemap de campo
            if (tilemapCampo.HasTile(posicaoCelula))
            {
                // Converte a posi��o da c�lula de volta para a posi��o do mundo
                Vector3 posicaoSpawn = tilemapCampo.CellToWorld(posicaoCelula) + tilemapCampo.tileAnchor;

                // Instancia o prefab de grama na posi��o aleat�ria
                Instantiate(prefabGrama, posicaoSpawn, Quaternion.identity);
                gramaSpawnada++;
            }

            // Espera pelo intervalo de tempo antes de spawnar novamente
            yield return new WaitForSeconds(intervaloSpawn);
        }
    }
}
