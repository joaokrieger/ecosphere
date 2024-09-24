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
    private int pontosGanhos;
    private int questoesErradas;
    private int quantidadeQuestoes;
    private SceneHandler sceneHandler;

    public Text textoQuestao;
    public Text textoFase;
    public Image imagemFase;

    public GameObject telaDeFinalizacao;
    public GameObject telaQuestionario;
    public Text textoQuantidadeAcerto;
    public Text textoPontosRecebidos;

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
            textoFase.text = "Fase 02 - Ecossistemas";
            Sprite sprite = Resources.Load<Sprite>("Images/MapaFases/Fase02");
            imagemFase.sprite = sprite;
        }
        else if (GameManager.Instance.faseAtual == Fase.Fase03)
        {
            textoFase.text = "Fase 03 - Populacoes e Comunidades";
            Sprite sprite = Resources.Load<Sprite>("Images/MapaFases/Fase03");
            imagemFase.sprite = sprite;
        }
        else if (GameManager.Instance.faseAtual == Fase.Fase04)
        {
            textoFase.text = "Fase 04 - Cadeias e Teias Alimentares";
            Sprite sprite = Resources.Load<Sprite>("Images/MapaFases/Fase04");
            imagemFase.sprite = sprite;
        }
        else if (GameManager.Instance.faseAtual == Fase.Fase05)
        {
            textoFase.text = "Parabens! Todas as fases foram concluidas com sucesso!";
            Sprite sprite = Resources.Load<Sprite>("Images/MapaFases/Fase05");
            imagemFase.sprite = sprite;
        }

        pontosGanhos = 0;
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
            perguntas.RemoveAt(questaoAtual);
            ResetarRespostas();
            GerarQuestao();
        }
        else
        {
            ExibirTelaDesempenhoAvalicao();
        }

        pontosGanhos += 9;
    }

    public void RespostaErrada()
    {
        AudioManager.instance.PlayEfeito("RespostaErrada");
        pontosGanhos -= 3;
        questoesErradas++;
    }

    public void GerarRespostas()
    {
        for (int i = 0; i < opcoes.Length; i++) {
            opcoes[i].GetComponent<Resposta>().estaCorreta = false;
            opcoes[i].GetComponent<Button>().interactable = true;
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
        // Perguntas Fase 01 - Fundamentos Ecologicos
        if (GameManager.Instance.faseAtual == Fase.Fase01)
        {
            perguntas.Add(new Pergunta(
                "Na imagem, todos os lobos estao vivendo na mesma area. O que isso representa no ecossistema?",
                new string[] {
                    "Uma comunidade",
                    "Uma populacao",
                    "Um ecossistema",
                    "Um habitat"
                },
                2,
                "Images/Fase 01/Questao01"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, o lobo, o coelho e o cervo estao vivendo na mesma area e interagindo entre si. O que isso representa?",
                new string[] {
                    "Um ecossistema",
                    "Uma populacao",
                    "Uma comunidade",
                    "Um habitat"
                },
                3,
                "Images/Fase 01/Questao02"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem acima, o que representa o ecossistema?",
                new string[] {
                    "Apenas os animais que vivem na area",
                    "As interacoes entre os seres vivos e os fatores nao vivos, como o rio e o solo",
                    "O grupo de animais que compete por recursos",
                    "O conjunto de fatores nao vivos, como o solo e a agua"
                },
                2,
                "Images/Fase 01/Questao03"
            ));

            perguntas.Add(new Pergunta(
                "Observe a imagem. Qual a diferenca entre a populacao e a comunidade representadas na cena?",
                new string[] {
                    "A populacao inclui apenas os lobos, enquanto a comunidade inclui os lobos, coelhos e cervos",
                    "A populacao inclui todas as especies, e a comunidade inclui apenas os coelhos",
                    "A populacao inclui todos os seres vivos, enquanto a comunidade sao os fatores nao vivos",
                    "Populacao e comunidade sao sinonimos"
                },
                1,
                "Images/Fase 01/Questao04"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, como os fatores abioticos (nao vivos), como o sol e a agua, afetam os seres vivos?",
                new string[] {
                    "Eles sao consumidos diretamente pelos herbivoros",
                    "Eles fornecem as condicoes necessarias para o crescimento das plantas, que sao a base da cadeia alimentar",
                    "Eles sao importantes apenas para os carnivoros",
                    "Eles protegem os animais contra predadores"
                },
                2,
                "Images/Fase 01/Questao05"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, o coelho esta pastando e a raposa esta a espreita. Que tipo de interacao esta ocorrendo entre eles?",
                new string[] {
                    "Comensalismo",
                    "Mutualismo",
                    "Predatismo",
                    "Parasitismo"
                },
                3,
                "Images/Fase 01/Questao06"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, o cervo esta pastando em uma area de campo aberto. O que seria o habitat desse cervo?",
                new string[] {
                    "O lugar onde o cervo encontra comida e se abriga",
                    "O conjunto de todas as especies que vivem com ele",
                    "O ecossistema como um todo",
                    "O conjunto de animais que interagem com o cervo"
                },
                1,
                "Images/Fase 01/Questao07"
            ));
        }


        // Perguntas Fase 02 - Niveis Troficos
        if (GameManager.Instance.faseAtual == Fase.Fase02)
        {
            perguntas.Add(new Pergunta(
                "Na imagem, o coelho esta se alimentando de uma planta. Qual o nivel trofico ocupado pelo coelho?",
                new string[] {
                    "Produtor",
                    "Consumidor primario",
                    "Consumidor secundario",
                    "Consumidor terciario"
                },
                2,
                "Images/Fase 02/Questao01"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, as plantas utilizam a luz do sol para realizar fotossintese. Qual o nivel trofico delas?",
                new string[] {
                    "Consumidor primario",
                    "Consumidor secundario",
                    "Produtor",
                    "Decompositor"
                },
                3,
                "Images/Fase 02/Questao02"
            ));

            perguntas.Add(new Pergunta(
                "O urso na imagem predou um cervo. Qual nivel trofico o urso ocupa?",
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
                "A sequencia de imagens representa uma cadeia alimentar. Qual a ordem correta dos niveis troficos?",
                new string[] {
                    "Produtor -> Consumidor primario -> Consumidor secundario",
                    "Consumidor primario -> Produtor -> Consumidor secundario",
                    "Produtor -> Decompositor -> Consumidor primario",
                    "Consumidor secundario -> Produtor -> Consumidor primario"
                },
                1,
                "Images/Fase 02/Questao04"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, a energia flui dos produtores para os herbivoros e depois para os carnivoros. Como a energia se move ao longo da cadeia alimentar?",
                new string[] {
                    "A energia aumenta conforme sobe nos niveis troficos",
                    "A energia diminui conforme sobe nos niveis troficos",
                    "A energia torna-se constante em todos os niveis troficos",
                    "A energia apenas existe nos consumidores secundarios"
                },
                2,
                "Images/Fase 02/Questao05"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, os fungos que crescem ao lado de restos de animais e plantas. Em qual nivel trofico eles ocupam?",
                new string[] {
                    "Consumidor primario",
                    "Consumidor secundario",
                    "Produtor",
                    "Decompositor"
                },
                4,
                "Images/Fase 02/Questao06"
            ));

            perguntas.Add(new Pergunta(
                "Na cadeia alimentar apresentada, todos os organismos dependem de quem para obter energia inicialmente?",
                new string[] {
                    "Consumidores primarios",
                    "Consumidores secundarios",
                    "Produtores",
                    "Decompositores"
                },
                3,
                "Images/Fase 02/Questao07"
            ));
        }

        // Perguntas Fase 03 - Cadeias Alimentares
        if (GameManager.Instance.faseAtual == Fase.Fase03)
        {
            perguntas.Add(new Pergunta(
                "Qual a ordem correta dos organismos na cadeia alimentar da imagem?",
                new string[] {
                    "Produtor -> Consumidor secundario -> Consumidor primario",
                    "Produtor -> Consumidor primario -> Consumidor secundario",
                    "Consumidor primario -> Produtor -> Consumidor secundario",
                    "Produtor -> Decompositor -> Consumidor primario"
                },
                2,
                "Images/Fase 03/Questao01"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, as plantas estao na base da cadeia alimentar. Qual o papel delas no ecossistema?",
                new string[] {
                    "Elas fornecem energia diretamente aos consumidores secundarios",
                    "Elas produzem energia por fotossintese, que sera utilizada por consumidores primarios",
                    "Elas decompoem materia organica para liberar nutrientes",
                    "Elas sao predadores que predam consumidores primarios"
                },
                2,
                "Images/Fase 03/Questao02"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, o urso esta no topo da cadeia alimentar. O que isso significa?",
                new string[] {
                    "O urso se alimenta de todos os organismos da cadeia",
                    "O urso depende apenas de decompositores para se alimentar",
                    "O urso nao tem predadores naturais",
                    "O urso nao esta relacionado a outras cadeias alimentares do ecossistema"
                },
                3,
                "Images/Fase 03/Questao03"
            ));

            perguntas.Add(new Pergunta(
                "O que pode acontecer se o herbivoro da imagem for removido da cadeia alimentar?",
                new string[] {
                    "A populacao de plantas pode aumentar sem controle",
                    "Os predadores terao mais presas disponiveis",
                    "O ecossistema ficara equilibrado automaticamente",
                    "Os consumidores secundarios se adaptarao sem problemas"
                },
                1,
                "Images/Fase 03/Questao04"
            ));

            perguntas.Add(new Pergunta(
                "Se os produtores da imagem forem drasticamente reduzidos, como isso pode afetar a cadeia alimentar?",
                new string[] {
                    "Aumenta a populacao de consumidores primarios",
                    "Os consumidores secundarios serão diretamente afetados pela falta de presas",
                    "A cadeia alimentar se adapta imediatamente",
                    "Os decompositores tomam o lugar dos produtores"
                },
                2,
                "Images/Fase 03/Questao05"
            ));

            perguntas.Add(new Pergunta(
                "Na imagem, o lobo esta como um consumidor secundario. O que isso significa em termos de cadeia alimentar?",
                new string[] {
                    "Ele se alimenta diretamente de plantas",
                    "Ele se alimenta de consumidores primarios, como herbivoros",
                    "Ele se alimenta de decompositores no ecossistema",
                    "Ele se alimenta de outros consumidores secundarios"
                },
                2,
                "Images/Fase 03/Questao06"
            ));

            perguntas.Add(new Pergunta(
                "O que pode acontecer com a cadeia alimentar da imagem se os predadores desaparecerem?",
                new string[] {
                    "A populacao de produtores aumenta drasticamente",
                    "A populacao de consumidores primarios aumenta descontroladamente",
                    "A populacao de consumidores secundarios aumenta",
                    "A cadeia alimentar se reorganiza para o equilibrio imediato"
                },
                2,
                "Images/Fase 03/Questao07"
            ));
        }

        // Perguntas Fase 04 - Teias Alimentares
        if (GameManager.Instance.faseAtual == Fase.Fase04)
        {

        }
    }

    private void ExibirTelaDesempenhoAvalicao()
    {
        telaQuestionario.SetActive(false);
        telaDeFinalizacao.SetActive(true);
        textoQuantidadeAcerto.text = ((quantidadeQuestoes * 4) - questoesErradas)+"/"+(quantidadeQuestoes * 4);
        textoPontosRecebidos.text = "+"+pontosGanhos;
    }

    public void FinalizaAvalicaoEcologica()
    {
        sceneHandler.NavegarParaEcossistema();
        GameManager.Instance.AtualizarFase(GameManager.Instance.GetNextFase());
        GameManager.Instance.AdicionaSaldo(this.pontosGanhos);
    }
}
