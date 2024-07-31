using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vegetacao : MonoBehaviour
{
    public GameObject prefabGrama; // O prefab de grama que ser� spawnado
    public int numeroDeGrama; // N�mero de inst�ncias de grama que voc� deseja spawnar
    public Vector2 areaSpawnMin; // Coordenadas m�nimas da �rea de spawn
    public Vector2 areaSpawnMax; // Coordenadas m�ximas da �rea de spawn
    public float intervaloSpawn; // Intervalo de tempo entre cada spawn

    private Tilemap tilemapCampo;
    private Tilemap tilemapEstrutura;

    void Start()
    {

        GameObject campoObject = GameObject.FindWithTag("Campo");
        tilemapCampo = campoObject.GetComponent<Tilemap>();

        GameObject estruturaObject = GameObject.FindWithTag("Estruturas");
        tilemapEstrutura = estruturaObject.GetComponent<Tilemap>();

        StartCoroutine(RotinaSpawnGrama());
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
            Vector3Int posicaoCelula = tilemapCampo.WorldToCell(posicaoAleatoria);

            if (tilemapCampo.HasTile(posicaoCelula) && !tilemapEstrutura.HasTile(posicaoCelula))
            {
                Vector3 posicaoSpawn = tilemapCampo.CellToWorld(posicaoCelula) + tilemapCampo.tileAnchor;

                Instantiate(prefabGrama, posicaoSpawn, Quaternion.identity);
                gramaSpawnada++;
            }

            yield return new WaitForSeconds(intervaloSpawn);
        }
    }
}
