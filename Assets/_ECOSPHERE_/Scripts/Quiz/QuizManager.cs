using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class QuizManager : MonoBehaviour
{

    private List<Pergunta> perguntas = new List<Pergunta>();
    public GameObject[] opcoes;
    public int questaoAtual;

    public Text textoQuestao;

    private void Start() {
        CarregarPerguntas();
        GerarQuestao();
    }

    public IEnumerator RespostaCorreta()
    {
        yield return new WaitForSeconds(1f);
        if (perguntas.Count > 1)
        {
            perguntas.RemoveAt(questaoAtual);
            ResetarRespostas();
            GerarQuestao();
        }
        else
        {
            FinalizaAvalicaoEcologica();
        }
    }

    public void RespostaErrada()
    {

    }

    public void GerarRespostas()
    {
        for (int i = 0; i < opcoes.Length; i++) {
            opcoes[i].GetComponent<Resposta>().estaCorreta = false;
            opcoes[i].transform.GetChild(0).GetComponent<Text>().text = perguntas[questaoAtual].respostas[i];

            if (perguntas[questaoAtual].repostaCorreta == i+1)
            {
                opcoes[i].GetComponent<Resposta>().estaCorreta = true;
            }
        }
    }

    public void GerarQuestao()
    {
        questaoAtual = Random.Range(0, perguntas.Count);
        textoQuestao.text = perguntas[questaoAtual].questao;
        GerarRespostas();
    }

    private void ResetarRespostas()
    {
        foreach (GameObject opcao in opcoes)
        {
            opcao.GetComponent<Resposta>().Resetar();
        }
    }


    private void CarregarPerguntas()
    {
        perguntas.Add(new Pergunta(
            "A rela��o entre os seres de uma esp�cie que captura e destr�i fisicamente seres de outra esp�cie, utilizando-os como alimento, � chamada de",
            new string[] { "Parasitismo", "Competi��o", "Predatismo", "Mutualismo" },
            3
        ));

        perguntas.Add(new Pergunta(
            "Conjunto de seres da mesma ____________, que habita determinada regi�o geogr�fica, forma uma ____________",
            new string[] { "esp�cie e bioma", "biosfera e esp�cie", "popula��o e biosfera", "esp�cie e popula��o" },
            4
        ));

        perguntas.Add(new Pergunta(
            "O conjunto formado por fatores bi�ticos e abi�ticos que atuam simultaneamente sobre determinada regi�o � denominado:",
            new string[] { "Ecossistema", "Habitat", "Bioma", "Popula��o" },
            1
        ));
    }

    private void FinalizaAvalicaoEcologica()
    {
        SceneManager.LoadScene("SimuladorEcossistema");
    }
}
