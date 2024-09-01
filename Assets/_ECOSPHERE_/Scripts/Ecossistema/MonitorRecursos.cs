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
    public GameObject indicadorPontoVida;

    // Update is called once per frame
    void Update()
    {

        int quantidadeCarnivoro = GameController.GetInstance().GetQuantidade(GameController.Entidade.Carnivoro);
        int quantidadeHerbivoro = GameController.GetInstance().GetQuantidade(GameController.Entidade.Herbivoro);
        int quantidadeProdutor = GameController.GetInstance().GetQuantidade(GameController.Entidade.Produtor);

        indicadorCarnivoros.GetComponent<Text>().text = quantidadeCarnivoro.ToString();
        indicadorHerbivoros.GetComponent<Text>().text = quantidadeHerbivoro.ToString();
        indicadorProdutores.GetComponent<Text>().text = quantidadeProdutor.ToString();
        indicadorPopulacao.GetComponent<Text>().text = (quantidadeCarnivoro + quantidadeHerbivoro).ToString();
        indicadorPontoVida.GetComponent<Text>().text = GameController.GetInstance().GetCarteiraPontoVida().GetSaldo().ToString();
    }
}
