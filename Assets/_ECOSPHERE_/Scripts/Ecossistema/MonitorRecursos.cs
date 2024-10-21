using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MonitorRecursos : MonoBehaviour
{
    public GameObject indicadorPopulacao;
    public GameObject indicadorCarnivoros;
    public GameObject indicadorHerbivoros;
    public GameObject indicadorProdutores;
    public GameObject pontuacaoJogador;

    // Update is called once per frame
    void Update()
    {

        int quantidadeCarnivoro = GameObject.FindGameObjectsWithTag("Predador").Length;
        int quantidadeHerbivoro = GameObject.FindGameObjectsWithTag("Presa").Length;
        int quantidadeProdutor = GameObject.FindGameObjectsWithTag("Grama").Length;

        indicadorCarnivoros.GetComponent<Text>().text = quantidadeCarnivoro.ToString();
        indicadorHerbivoros.GetComponent<Text>().text = quantidadeHerbivoro.ToString();
        indicadorProdutores.GetComponent<Text>().text = quantidadeProdutor.ToString();
        indicadorPopulacao.GetComponent<Text>().text = (quantidadeCarnivoro + quantidadeHerbivoro).ToString();
        pontuacaoJogador.GetComponent<Text>().text = "Pontos: " + GameManager.Instance.pontuacaoJogador;
    }
}
