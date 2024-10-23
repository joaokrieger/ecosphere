using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuizManager : MonoBehaviour
{

    private List<Pergunta> perguntas = new List<Pergunta>();
    public GameObject[] opcoes;
    public Image imagemQuestao;
    private int questoesErradas;
    private int quantidadeQuestoes;
    private SceneHandler sceneHandler;

    public Text textoQuestao;
    public Text textoFase;
    public Image imagemFase;

    public GameObject telaDeFinalizacao;
    public GameObject telaQuestionario;
    public Text textoQuantidadeAcerto;

    private void Start() {
        CarregarPerguntas();
        GerarQuestao();

        quantidadeQuestoes = perguntas.Count;

        GameObject sceneHandlerObject = GameObject.FindGameObjectWithTag("SceneHandler");
        if (sceneHandlerObject != null)
        {
            sceneHandler = sceneHandlerObject.GetComponent<SceneHandler>();
        }

        if (GameManager.Instance.faseAtual == Fase.Fase01)
        {
            textoFase.text = "Fase 01 - Fundamentos Ecologicos";
            Sprite sprite = Resources.Load<Sprite>("Images/MapaFases/Fase01");
            imagemFase.sprite = sprite;
        }
        else if (GameManager.Instance.faseAtual == Fase.Fase02)
        {
            textoFase.text = "Fase 02 - Niveis Troficos";
            Sprite sprite = Resources.Load<Sprite>("Images/MapaFases/Fase02");
            imagemFase.sprite = sprite;
        }
        else if (GameManager.Instance.faseAtual == Fase.Fase03)
        {
            textoFase.text = "Fase 03 - Cadeias Alimentares";
            Sprite sprite = Resources.Load<Sprite>("Images/MapaFases/Fase03");
            imagemFase.sprite = sprite;
        }

        questoesErradas = 0;
    }

    public IEnumerator RespostaCorreta()
    {
        AudioManager.instance.PlayEfeito("RespostaCorreta");
        for (int i = 0; i < opcoes.Length; i++)
        {
            opcoes[i].GetComponent<Button>().interactable = false;
        }

        yield return new WaitForSeconds(2f);
        if (perguntas.Count > 1)
        {
            perguntas.RemoveAt(0);
            ResetarRespostas();
            GerarQuestao();
        }
        else
        {
            ExibirTelaDesempenhoAvalicao();
        }
    }

    public void RespostaErrada()
    {
        AudioManager.instance.PlayEfeito("RespostaErrada");
        questoesErradas++;
    }

    public void GerarRespostas()
    {
        for (int i = 0; i < opcoes.Length; i++) {
            opcoes[i].GetComponent<Resposta>().estaCorreta = false;
            opcoes[i].GetComponent<Button>().interactable = true;
            opcoes[i].transform.GetChild(0).GetComponent<Text>().text = perguntas[0].respostas[i];

            if (perguntas[0].repostaCorreta == i+1)
            {
                opcoes[i].GetComponent<Resposta>().estaCorreta = true;
            }
        }
    }

    public void GerarQuestao()
    {
        textoQuestao.text = perguntas[0].questao;
        GerarImagem();
        GerarRespostas();
    }

    public void GerarImagem()
    {
        string imagem = perguntas[0].caminhoImagem;
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
        // Perguntas Fase 01 - Fundamentos Ecologicos
        if (GameManager.Instance.faseAtual == Fase.Fase01)
        {
            perguntas.Add(new Pergunta(
                "O que sao os produtores em um ecossistema?",
                new string[] {
                    "Organismos que se alimentam de plantas",
                    "Organismos que produzem seu proprio alimento usando a luz solar",
                    "Organismos que decompoem materia organica",
                    "Animais que cacam outros animais"
                },
                2,
                "Images/Fase 01/Questao01"
            ));

            perguntas.Add(new Pergunta(
                "O que sao os consumidores primarios?",
                new string[] {
                    "Organismos que produzem energia para consumidores secundarios",
                    "Organismos que se alimentam de consumidores terciarios",
                    "Organismos que se alimentam de produtores, como plantas",
                    "Organismos que decompoem materia organica"
                },
                3,
                "Images/Fase 01/Questao02"
            ));

            perguntas.Add(new Pergunta(
                "O que sao os consumidores secundarios?",
                new string[] {
                    "Organismos que se alimentam de produtores",
                    "Organismos que se alimentam de consumidores primarios",
                    "Organismos que decompoem materia organica",
                    "Organismos que produzem seu proprio alimento"
                },
                2,
                "Images/Fase 01/Questao03"
            ));

            perguntas.Add(new Pergunta(
                "O que sao os consumidores terciarios?",
                new string[] {
                    "Organismos que produzem energia para a cadeia alimentar",
                    "Organismos que se alimentam de consumidores secundarios e outros carnivoros",
                    "Organismos que se alimentam diretamente de produtores",
                    "Organismos que decompoem materia organica"
                },
                2,
                "Images/Fase 01/Questao04"
            ));

            perguntas.Add(new Pergunta(
                "O que sao os decompositores?",
                new string[] {
                    "Organismos que produzem energia para a cadeia alimentar",
                    "Organismos que se alimentam de consumidores terciarios",
                    "Organismos que decompoem materia organica e reciclam nutrientes no solo",
                    "Organismos que se alimentam de consumidores primarios"
                },
                3,
                "Images/Fase 01/Questao05"
            ));

            perguntas.Add(new Pergunta(
                "Qual o papel dos produtores em uma cadeia alimentar?",
                new string[] {
                    "Controlar a populacao de consumidores primarios",
                    "Fornecer energia para todos os outros niveis troficos",
                    "Reciclar nutrientes no solo",
                    "Se alimentar de consumidores secundarios"
                },
                2,
                "Images/Fase 01/Questao06"
            ));

            perguntas.Add(new Pergunta(
                "Qual o papel dos consumidores secundarios em um ecossistema?",
                new string[] {
                    "Se alimentam de produtores para obter energia",
                    "Mantem o equilibrio populacional ao se alimentarem de consumidores primarios",
                    "Produzem alimento para consumidores terciarios",
                    "Decompoem organismos mortos para liberar nutrientes"
                },
                2,
                "Images/Fase 01/Questao07"
            ));
        }

        // Perguntas Fase 02 - Niveis Troficos
        if (GameManager.Instance.faseAtual == Fase.Fase02)
        {
            perguntas.Add(new Pergunta(
                "Na imagem, a grama cresce no solo. Qual o nivel trofico ocupado pela grama?",
                new string[] {
                    "Produtor",
                    "Consumidor primario",
                    "Consumidor secundario",
                    "Decompositor"
                },
                1,
                "Images/Fase 02/Questao01"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, o coelho se alimenta de uma grama. Qual o nivel trofico ocupado pelo coelho?",
                new string[] {
                    "Consumidor primario",
                    "Consumidor secundario",
                    "Produtor",
                    "Decompositor"
                },
                1,
                "Images/Fase 02/Questao02"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, a raposa observa os coelhos. Qual o nivel trofico ocupado pela raposa?",
                new string[] {
                    "Consumidor primario",
                    "Consumidor secundario",
                    "Produtor",
                    "Decompositor"
                },
                2,
                "Images/Fase 02/Questao03"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, o cervo se alimenta de vegetacao. Qual o nivel trofico ocupado pelo cervo?",
                new string[] {
                    "Consumidor primario",
                    "Consumidor secundario",
                    "Produtor",
                    "Decompositor"
                },
                1,
                "Images/Fase 02/Questao04"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, uma alcateia de lobos perseguem um cervo. Qual o nivel trofico ocupado pelos lobos?",
                new string[] {
                    "Consumidor primario",
                    "Consumidor secundario",
                    "Produtor",
                    "Decompositor"
                },
                2,
                "Images/Fase 02/Questao05"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, cogumelos crescem ao lado de um animal morto. Qual o nivel trofico ocupado pelos cogumelos?",
                new string[] {
                    "Produtor",
                    "Decompositor",
                    "Consumidor Secundario",
                    "Consumidor Primario"
                },
                2,
                "Images/Fase 02/Questao06"
            ));

            perguntas.Add(new Pergunta(
                "Qual a ordem correta dos organismos na cadeia alimentar da imagem?",
                new string[] {
                    "Produtor -> Consumidor secundario -> Consumidor primario",
                    "Produtor -> Consumidor primario -> Consumidor secundario",
                    "Consumidor primario -> Produtor -> Consumidor secundario",
                    "Produtor -> Decompositor -> Consumidor primario"
                },
                2,
                "Images/Fase 02/Questao07"
            ));
        }

        // Perguntas Fase 03 - Cadeias Alimentares
        if (GameManager.Instance.faseAtual == Fase.Fase03)
        {
            perguntas.Add(new Pergunta(
                "No ecossistema, a energia flui da grama para o coelho e, em seguida, para a raposa. Qual a funcao principal da grama nessa cadeia alimentar?",
                new string[] {
                    "Decompositor",
                    "Consumidor primario",
                    "Produtor",
                    "Consumidor secundario"
                },
                3,
                "Images/Fase 03/Questao01"
            ));

            perguntas.Add(new Pergunta(
                "Qual seria o impacto na cadeia alimentar se todos os produtores, como a grama, fossem removidos do ecossistema?",
                new string[] {
                    "Os consumidores primarios iriam aumentar",
                    "Os decompositores iriam se tornar produtores",
                    "Os consumidores primarios iriam morrer por falta de alimento",
                    "Os consumidores secundarios passariam a se alimentar de decompositores"
                },
                3,
                "Images/Fase 03/Questao02"
            ));

            perguntas.Add(new Pergunta(
                "Se a populacao de coelhos aumentasse significativamente, como isso afetaria as raposas?",
                new string[] {
                    "A populacao de raposas aumentaria devido ao maior acesso a alimento",
                    "A populacao de raposas diminuiria devido a competicao com os coelhos",
                    "As raposas se tornariam consumidores primarios",
                    "A populacao de raposas permaneceria a mesma"
                },
                1,
                "Images/Fase 03/Questao03"
            ));

            perguntas.Add(new Pergunta(
                "Na cadeia alimentar, o coelho se alimenta de gramas, e o lobo se alimenta de coelhos. O que o lobo representa na cadeia alimentar?",
                new string[] {
                    "Produtor",
                    "Consumidor primario",
                    "Consumidor secundario",
                    "Decompositor"
                },
                3,
                "Images/Fase 03/Questao04"
            ));

            perguntas.Add(new Pergunta(
                "Se a populacao de decompositores, como cogumelos, diminuisse no ecossistema, qual seria a consequencia mais provavel?",
                new string[] {
                    "O acumulo de materia organica morta aumentaria",
                    "A populacao de consumidores primarios aumentaria",
                    "Os produtores iriam se tornar decompositores",
                    "Os consumidores secundarios iriam desaparecer"
                },
                1,
                "Images/Fase 03/Questao05"
            ));

            perguntas.Add(new Pergunta(
                "Indique a ordem correta do fluxo de energia em uma cadeia alimentar simples que inclui grama, javali, e urso.",
                new string[] {
                    "Produtor -> Consumidor secundario -> Consumidor primario",
                    "Consumidor secundario -> Consumidor primario -> Produtor",
                    "Produtor -> Consumidor primario -> Consumidor secundario",
                    "Consumidor primario -> Produtor -> Consumidor secundario"
                },
                3,
                "Images/Fase 03/Questao06"
            ));

            perguntas.Add(new Pergunta(
                "Em um ecossistema equilibrado, o que aconteceria se o numero de predadores, como o lobo, aumentasse significativamente?",
                new string[] {
                    "A populacao de consumidores primarios aumentaria",
                    "A populacao de produtores aumentaria",
                    "A populacao de consumidores secundarios diminuiria",
                    "A populacao de consumidores primarios diminuiria"
                },
                4,
                "Images/Fase 03/Questao07"
            ));
        }
    }

    private void ExibirTelaDesempenhoAvalicao()
    {
        telaQuestionario.SetActive(false);
        telaDeFinalizacao.SetActive(true);

        int pontuacaoAvaliacao = ((quantidadeQuestoes * 4) - questoesErradas);
        textoQuantidadeAcerto.text = pontuacaoAvaliacao + "/"+(quantidadeQuestoes * 4);
        GameManager.Instance.pontuacaoJogador += pontuacaoAvaliacao;
    }

    public void FinalizaAvalicaoEcologica()
    {
        sceneHandler.NavegarParaEcossistema();
        GameManager.Instance.AtualizarFase(GameManager.Instance.GetNextFase());
    }
}
