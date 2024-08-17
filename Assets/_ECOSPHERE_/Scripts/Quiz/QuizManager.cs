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
            "O que é uma cadeia alimentar?",
            new string[] { "Uma sequência de animais que vivem no mesmo habitat", "A ordem em que os animais se tornam amigos", "A transferência de energia dos produtores para os consumidores", "A quantidade de água disponível em um ecossistema" },
            3
        ));

        perguntas.Add(new Pergunta(
            "Qual dos seguintes organismos é um produtor?",
            new string[] { "Cervo", "Urso", "Cogumelo", "Grama" },
            4
        ));

        perguntas.Add(new Pergunta(
            "Conjunto de seres da mesma ____________, que habita determinada região geográfica, forma uma ____________",
            new string[] { "espécie e bioma", "biosfera e espécie", "população e biosfera", "espécie e população" },
            4
        ));

        perguntas.Add(new Pergunta(
            "Qual é o papel de um herbívoro em uma cadeia alimentar?",
            new string[] { "Comer outros herbívoros", "Produzir sua própria comida", "Comer plantas e fornecer comida para carnívoros", "Decompor plantas e animais mortos" },
           3
        ));

        perguntas.Add(new Pergunta(
            "O que pode acontecer se um predador desaparecer de um ecossistema?",
            new string[] { "As plantas vão morrer", "Os herbívoros podem se multiplicar demais e afetar o equilíbrio do ecossistema", "Todos os outros animais vão se tornar produtores", "A água vai desaparecer do ambiente" },
           2
        ));

        perguntas.Add(new Pergunta(
            "A relação entre os seres de uma espécie que captura e destrói fisicamente seres de outra espécie, utilizando-os como alimento, é chamada de",
            new string[] { "Parasitismo", "Competição", "Predatismo", "Mutualismo" },
            3
        ));

        perguntas.Add(new Pergunta(
            "O conjunto formado por fatores bióticos e abióticos que atuam simultaneamente sobre determinada região é denominado:",
            new string[] { "Ecossistema", "Habitat", "Bioma", "População" },
            1
        ));
    }

    private void FinalizaAvalicaoEcologica()
    {
        SceneManager.LoadScene("SimuladorEcossistema");
    }
}
