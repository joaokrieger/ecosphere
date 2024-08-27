using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuizManager : MonoBehaviour
{

    private List<Pergunta> perguntas = new List<Pergunta>();
    public GameObject[] opcoes;
    public Image imagemQuestao;
    public int questaoAtual;

    private SceneHandler sceneHandler;

    public Text textoQuestao;

    private void Start() {
        CarregarPerguntas();
        GerarQuestao();

        GameObject sceneHandlerObject = GameObject.FindGameObjectWithTag("SceneHandler");
        if (sceneHandlerObject != null)
        {
            sceneHandler = sceneHandlerObject.GetComponent<SceneHandler>();
        }
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
        GerarImagem();
        GerarRespostas();
    }

    public void GerarImagem()
    {
        string imagem = perguntas[questaoAtual].caminhoImagem;
        Sprite sprite = Resources.Load<Sprite>(imagem);
        imagemQuestao.sprite = sprite;
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
            "Observe a imagem acima que mostra diferentes organismos do ecossistema. Qual dos seguintes organismos pode ser considerado um consumidor secundario?",
            new string[] { "Urso", "Cervo", "Javali", "Coelho" },
            1,
            "Images/Fase 01/ImagemQuestao (1)"
        ));        

        perguntas.Add(new Pergunta(
            "No seu ecossistema, os cogumelos atuam como decompositores. Qual o papel essencial desses organismos numa cadeia alimentar?",
            new string[] { "Produzir oxigenio para os animais respirarem", "Reciclar nutrientes ao decompor materia organica morta", "Cacar pequenos herbivoros para controlar a populacao", "Polinizar plantas e flores para garantir o crescimento vegetal" },
            2,
            "Images/Fase 01/ImagemQuestao (2)"
        ));

        perguntas.Add(new Pergunta(
            "Qual desses animais é um produtor em uma cadeia alimentar?",
            new string[] { "Grama", "Coelho", "Raposa", "Urso" },
            1,
            "Images/Fase 01/ImagemQuestao (3)"
        ));

        perguntas.Add(new Pergunta(
            "Em um ecossistema, o que aconteceria se a populacao de lobos aumentasse significativamente?",
            new string[] { "A populacao de coelhos aumentaria", "A populacao de cervos diminuiria", "A populacao de grama aumentaria", "A populacao de cogumelos aumentaria" },
            2,
            "Images/Fase 01/ImagemQuestao (4)"
        ));

        perguntas.Add(new Pergunta(
            "Em um ecossistema, se a populacao de raposas diminuisse, o que poderia acontecer?",
            new string[] { "A populacao de coelhos aumentaria", "A populacao de lobos diminuiria", "A populacao de cogumelos aumentaria", "A populacao de cervos aumentaria" },
            1,
            "Images/Fase 01/ImagemQuestao (5)"
        ));

        perguntas.Add(new Pergunta(
            "Se a populacao de coelhos em um ecossistema aumenta, qual dessas populacoes provavelmente aumentara como consequencia?",
            new string[] { "Grama", "Cervos", "Arvores", "Raposa" },
            4,
            "Images/Fase 01/ImagemQuestao (6)"
        ));

        perguntas.Add(new Pergunta(
            "Qual desses animais geralmente pode ser encontrado no mesmo nivel trofico que o coelho?",
            new string[] { "Lobo", "Raposa", "Javali", "Grama" },
            3,
            "Images/Fase 01/ImagemQuestao (7)"
        ));
    }

    private void FinalizaAvalicaoEcologica()
    {
        sceneHandler.NavegarParaEcossistema();
    }
}
