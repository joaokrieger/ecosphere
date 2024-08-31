using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MonitorRecursos : MonoBehaviour
{
    public GameObject indicadorCarnivoros;
    public GameObject indicadorHerbivoros;
    public GameObject indicadorProdutores;
    public GameObject indicadorPontoVida;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        indicadorCarnivoros.GetComponent<Text>().text = GameController.GetInstance().GetQuantidade(GameController.Entidade.Carnivoro).ToString();
        indicadorHerbivoros.GetComponent<Text>().text = GameController.GetInstance().GetQuantidade(GameController.Entidade.Herbivoro).ToString();
        indicadorProdutores.GetComponent<Text>().text = GameController.GetInstance().GetQuantidade(GameController.Entidade.Produtor).ToString();
        indicadorPontoVida.GetComponent<Text>().text = GameController.GetInstance().GetCarteiraPontoVida().GetSaldo().ToString();
    }
}
