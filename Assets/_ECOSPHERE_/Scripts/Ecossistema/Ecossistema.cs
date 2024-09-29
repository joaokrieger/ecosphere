using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ecossistema : MonoBehaviour
{
    public float raioSpawnCogumelo = 3f;
    public float tempoDesaparecimento = 5f; 
    public GameObject cogumeloPrefab;
    public Vegetacao vegetacao;
    private Tilemap tilemapCampo;

    void Start()
    {
        GameObject campoObject = GameObject.FindWithTag("Campo");
        tilemapCampo = campoObject.GetComponent<Tilemap>();
    }

    public IEnumerator Decompor(GameObject cadaver)
    {
        Destroy(cadaver.GetComponent<BoxCollider2D>());
        yield return new WaitForSeconds(4f);

        Vector3 posicaoCadaver = cadaver.transform.position;
        Vector3 posicaoAleatoria = GerarPosicaoAleatoriaAoRedorCadaver(posicaoCadaver);
        if (tilemapCampo.HasTile(tilemapCampo.WorldToCell(posicaoAleatoria)))
        {
            GameObject cogumelo = Instantiate(cogumeloPrefab, posicaoAleatoria, Quaternion.identity);
            Destroy(cogumelo, tempoDesaparecimento);
        }

        Destroy(cadaver, tempoDesaparecimento);
    }

    private Vector3 GerarPosicaoAleatoriaAoRedorCadaver(Vector3 posicaoCadaver)
    {
        float anguloAleatorio = Random.Range(0f, 360f);
        float raio = Random.Range(0f, raioSpawnCogumelo);

        float xOffset = Mathf.Cos(anguloAleatorio * Mathf.Deg2Rad) * raio;
        float yOffset = Mathf.Sin(anguloAleatorio * Mathf.Deg2Rad) * raio;

        return new Vector3(posicaoCadaver.x + xOffset, posicaoCadaver.y + yOffset, posicaoCadaver.z);
    }

    public void ResetarSimulacao()
    {
        GameObject[] gramas = GameObject.FindGameObjectsWithTag("Grama");
        foreach (GameObject grama in gramas)
        {
            Destroy(grama);
        }

        GameObject[] predadores = GameObject.FindGameObjectsWithTag("Predador");
        foreach (GameObject predador in predadores)
        {
            Destroy(predador);
        }

        GameObject[] presas = GameObject.FindGameObjectsWithTag("Presa");
        foreach (GameObject presa in presas)
        {
            Destroy(presa);
        }

        vegetacao.SpawnInicialGrama();
    }
}
