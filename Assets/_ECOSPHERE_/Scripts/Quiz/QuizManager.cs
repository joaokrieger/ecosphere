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
            "O que � uma cadeia alimentar?",
            new string[] { "Uma sequ�ncia de animais que vivem no mesmo habitat", "A ordem em que os animais se tornam amigos", "A transfer�ncia de energia dos produtores para os consumidores", "A quantidade de �gua dispon�vel em um ecossistema" },
            3
        ));

        perguntas.Add(new Pergunta(
            "Qual dos seguintes organismos � um produtor?",
            new string[] { "Cervo", "Urso", "Cogumelo", "Grama" },
            4
        ));

        perguntas.Add(new Pergunta(
            "Conjunto de seres da mesma ____________, que habita determinada regi�o geogr�fica, forma uma ____________",
            new string[] { "esp�cie e bioma", "biosfera e esp�cie", "popula��o e biosfera", "esp�cie e popula��o" },
            4
        ));

        perguntas.Add(new Pergunta(
            "Qual � o papel de um herb�voro em uma cadeia alimentar?",
            new string[] { "Comer outros herb�voros", "Produzir sua pr�pria comida", "Comer plantas e fornecer comida para carn�voros", "Decompor plantas e animais mortos" },
           3
        ));

        perguntas.Add(new Pergunta(
            "O que pode acontecer se um predador desaparecer de um ecossistema?",
            new string[] { "As plantas v�o morrer", "Os herb�voros podem se multiplicar demais e afetar o equil�brio do ecossistema", "Todos os outros animais v�o se tornar produtores", "A �gua vai desaparecer do ambiente" },
           2
        ));

        perguntas.Add(new Pergunta(
            "A rela��o entre os seres de uma esp�cie que captura e destr�i fisicamente seres de outra esp�cie, utilizando-os como alimento, � chamada de",
            new string[] { "Parasitismo", "Competi��o", "Predatismo", "Mutualismo" },
            3
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
