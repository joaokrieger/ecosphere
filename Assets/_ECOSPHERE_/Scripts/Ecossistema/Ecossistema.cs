using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ecossistema : MonoBehaviour
{
    public float raioSpawnCogumelo = 3f;
    public float tempoDesaparecimento = 5f; 
    public GameObject cogumeloPrefab;
    private Tilemap tilemapCampo;

    void Start()
    {
        GameObject campoObject = GameObject.FindWithTag("Campo");
        tilemapCampo = campoObject.GetComponent<Tilemap>();
    }

    public IEnumerator Decompor(GameObject cadaver)
    {
        yield return new WaitForSeconds(3f);

        Vector3 posicaoCadaver = cadaver.transform.position;
        int numeroCogumelos = Random.Range(1, 3);

        for (int i = 0; i < numeroCogumelos; i++)
        {
            Vector3 posicaoAleatoria = GerarPosicaoAleatoria(posicaoCadaver);

            if (tilemapCampo.HasTile(tilemapCampo.WorldToCell(posicaoAleatoria)))
            {
                GameObject cogumelo = Instantiate(cogumeloPrefab, posicaoAleatoria, Quaternion.identity);
                Destroy(cogumelo, tempoDesaparecimento);
            }
        }
        Destroy(cadaver, tempoDesaparecimento);
    }

    private Vector3 GerarPosicaoAleatoria(Vector3 posicaoCadaver)
    {
        float anguloAleatorio = Random.Range(0f, 360f);
        float raio = Random.Range(0f, raioSpawnCogumelo);

        float xOffset = Mathf.Cos(anguloAleatorio * Mathf.Deg2Rad) * raio;
        float yOffset = Mathf.Sin(anguloAleatorio * Mathf.Deg2Rad) * raio;

        return new Vector3(posicaoCadaver.x + xOffset, posicaoCadaver.y + yOffset, posicaoCadaver.z);
    }
}
