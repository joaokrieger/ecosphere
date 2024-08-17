using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MonitorRecursos : MonoBehaviour
{
    public GameObject indicadorPredadores;
    public GameObject indicadorPresas;
    public GameObject indicadorProdutores;
    public GameObject indicadorPontoVida;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        indicadorPredadores.GetComponent<Text>().text = GameController.GetInstance().GetQuantidade(GameController.Entidade.Predador).ToString();
        indicadorPresas.GetComponent<Text>().text = GameController.GetInstance().GetQuantidade(GameController.Entidade.Presa).ToString();
        indicadorProdutores.GetComponent<Text>().text = GameController.GetInstance().GetQuantidade(GameController.Entidade.Produtor).ToString();
        indicadorPontoVida.GetComponent<Text>().text = GameController.GetInstance().GetCarteiraPontoVida().GetSaldo().ToString();
    }
}
