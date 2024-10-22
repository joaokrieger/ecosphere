using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("Painel do Dialogo")]
    public GameObject painelMensagemDialogo;
    public Text textoDialogo;
    public GameObject painelPermitePular;

    [Header("Painel do Tutorial")]
    public GameObject painelTutorial;
    public Text textoTutorial;
    public Image imagemTutorial;

    [Header("Painel Missão")]
    public GameObject painelMissao;
    public Text tituloMissao;
    public Text descricaoMissao;
    public Image imagemMissao;

    [Header("Painel do Resetar Jogo")]
    public GameObject painelResetar;
    public GameObject painelBloqueioResetarJogo;

    [Header("Painel Spawn")]
    public PainelSpawn[] paineisSpawn;

    [Header("Variáveis de Controle")]
    private SceneHandler sceneHandler;
    private bool tutorialReproducao = false;
    private bool permitePular = false;
    private float intervaloAvaliacao = 240f;
    private float ultimoTempoAvaliacao;

    [System.Serializable]
    public class PainelSpawn
    {
        public Especie especie;
        public GameObject painelSpawn;
    }

    void Start()
    {
        sceneHandler = FindObjectOfType<SceneHandler>();
        MostrarTutorial();

        if (GameManager.Instance.faseAtual == Fase.Fase02 || (GameManager.Instance.faseAtual == Fase.Fase01 && GameManager.Instance.passoAtual > 3))
        {
            PainelSpawn[] paineisSpawnLiberados = Array.FindAll(paineisSpawn, x => (
                x.especie == Especie.Coelho || 
                x.especie == Especie.Raposa ||
                x.especie == Especie.Javali ||
                x.especie == Especie.Lobo
             ));
            foreach (PainelSpawn painelSpawnLiberado in paineisSpawnLiberados)
            {
                painelSpawnLiberado.painelSpawn.SetActive(true);
            }

            painelBloqueioResetarJogo.SetActive(false);
            IniciarMissaoAvaliacaoEcologica();
        }
        else if (GameManager.Instance.faseAtual == Fase.Fase03)
        {
            PainelSpawn[] paineisSpawnLiberados = Array.FindAll(paineisSpawn, x => (
                x.especie == Especie.Coelho || 
                x.especie == Especie.Raposa ||
                x.especie == Especie.Javali ||
                x.especie == Especie.Lobo ||
                x.especie == Especie.Cervo ||
                x.especie == Especie.Urso 
            ));
            foreach (PainelSpawn painelSpawnLiberado in paineisSpawnLiberados)
            {
                painelSpawnLiberado.painelSpawn.SetActive(true);
            }

            painelBloqueioResetarJogo.SetActive(false);
            IniciarMissaoAvaliacaoEcologica();
        }
        else if (GameManager.Instance.faseAtual == Fase.Fase04)
        {
            PainelSpawn[] paineisSpawnLiberados = Array.FindAll(paineisSpawn, x => (
                x.especie == Especie.Coelho ||
                x.especie == Especie.Raposa ||
                x.especie == Especie.Javali ||
                x.especie == Especie.Lobo ||
                x.especie == Especie.Cervo ||
                x.especie == Especie.Urso
            ));
            foreach (PainelSpawn painelSpawnLiberado in paineisSpawnLiberados)
            {
                painelSpawnLiberado.painelSpawn.SetActive(true);
            }

            painelBloqueioResetarJogo.SetActive(false);
        }
    }

    void Update()
    {
        if ((painelTutorial.activeSelf || painelResetar.activeSelf) && Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
        }
        else if ((!painelTutorial.activeSelf && !painelResetar.activeSelf) && Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        if (Input.anyKeyDown && permitePular)
        {
            painelMensagemDialogo.SetActive(false);
            painelPermitePular.SetActive(false);
            permitePular = false;
            StartCoroutine(AguardarExecutar(5f, () =>
            {
                ConcluirEtapaTutorial();
                MostrarTutorial();
            }));
        }

        if (GameManager.Instance.passoAtual == 1)
        {
            int countCoelho = ContarEspecie<Herbivoro>(Especie.Coelho);
            descricaoMissao.text = $"{countCoelho}/7";
            if (countCoelho == 7)
            {
                PainelSpawn painelSpawnCoelho = Array.Find(paineisSpawn, x => x.especie == Especie.Coelho);
                if (painelSpawnCoelho != null)
                {
                    painelSpawnCoelho.painelSpawn.SetActive(false);
                }
                ConcluirEtapaTutorial();
                StartCoroutine(AguardarExecutar(5f, () =>
                {
                    ExibirPainelTutorial(
                        "Otimo trabalho! \n\n" +
                        "Conheca os coelhos! Ele sao consumidores primarios, o que significa que se alimentam diretamente das plantas do seu ecossistema. \n\n" +
                        "Preste atencao e veja como os consumidores primarios aproveitam a grama!",
                        "Images/Tutorial/ApresentacaoCoelho"
                    );
                }));
            }
        }

        if (GameManager.Instance.passoAtual == 2)
        {
            int countRaposa = ContarEspecie<Carnivoro>(Especie.Raposa);
            descricaoMissao.text = $"{countRaposa}/3";
            if (countRaposa == 3)
            {
                PainelSpawn painelSpawnRaposa = Array.Find(paineisSpawn, x => x.especie == Especie.Raposa);
                if (painelSpawnRaposa != null)
                {
                    painelSpawnRaposa.painelSpawn.SetActive(false);
                }
                ConcluirEtapaTutorial();

                StartCoroutine(AguardarExecutar(5f, () =>
                {
                    ExibirPainelTutorial(
                        "Voce esta indo bem! \n\n" +
                        "Apresento as raposas! As raposas sao consumidoras secundarias, pois se alimentam de consumidores primarios como o coelho. \n\n" +
                        "As raposas vao predar os coelhos! Isso mostra como a cadeia alimentar funciona na natureza.",
                        "Images/Tutorial/ApresentacaoRaposa"
                    );
                }));

                StartCoroutine(AguardarExecutar(30f, () =>
                {
                    ExibirPainelTutorial(
                        "Fique atento! Os animais tem tres emotes importantes que indicam seu estado. \n\n" +
                        "Um emote de fome aparece quando estao procurando comida, o de reproducao quando estao prontos para aumentar a populacao, e o de morte quando estao em seus ultimos momentos. \n\n" +
                        "Observe-os para manter o equilibrio no ecossistema!",
                        "Images/Tutorial/ApresentacaoEmote"
                    );
                }));


                StartCoroutine(AguardarExecutar(60f, () =>
                {
                    ExibirPainelTutorial(
                        "Os cogumelos que surgem ao lado dos animais mortos sao decompositores. \n\n" +
                        "Eles desempenham um papel vital no ecossistema, quebrando a materia organica e devolvendo nutrientes ao solo, garantindo que o ciclo da vida continue!",
                        "Images/Tutorial/ApresentacaoDecompositores"
                    );
                }));

                StartCoroutine(AguardarExecutar(90f, MostrarTutorial));
            }
        }
    }

    public void MostrarTutorial()
    {
        painelTutorial.SetActive(false);
        painelMissao.SetActive(false);

        switch(GameManager.Instance.passoAtual)
        {
            case 0:
                ExibirDialogo("Bem-vindo ao Ecosphere, um mundo onde a natureza precisa da sua ajuda.\n\n" +
                    "Boa sorte nesta nova jornada meu jovem!");

                StartCoroutine(AguardarExecutar(10f, () =>
                {
                    ExibirDialogo("Restaure o equilibrio natural deste ecossistema! \n\n" +
                                  "Adicione novas especies e descubra como cada uma delas contribui para o funcionamento harmonioso do ambiente.");
                }));

                StartCoroutine(AguardarExecutar(20f, HabilitaPular));
                break;
            case 1:
                ExibirPainelTutorial(
                    "Vamos comecar a popular o nosso ecossistema! Selecione uma especie disponivel no painel. \r\n\r\n" +
                    "A grama esta crescendo rapidamente, entao introduza uma populacao de coelhos para ajuda-la a ser controlada!",
                    "Images/Tutorial/SpawnAnimalCoelho"
                );

                ExibirPainelMissao(
                    "Adicione Coelhos",
                    "0/7",
                    "Images/Animais/Coelho"
                );

                PainelSpawn painelSpawnCoelho = Array.Find(paineisSpawn, x => x.especie == Especie.Coelho);
                if (painelSpawnCoelho != null) {
                    painelSpawnCoelho.painelSpawn.SetActive(true);
                }

                break;
            case 2:
                ExibirPainelTutorial(
                    "Os coelhos se reproduzem rapidamente e podem superlotar o ecossistema em pouco tempo! \r\n\r\n" +
                    "Veja na imagem! A raposa pode ser o predador ideal para ajudar a controlar a populacao de coelhos.",
                    "Images/Tutorial/SpawnAnimalRaposa"
                );

                ExibirPainelMissao(
                    "Adicione Raposas",
                    "0/3",
                    "Images/Animais/Raposa"
                );

                PainelSpawn painelSpawnRaposa = Array.Find(paineisSpawn, x => x.especie == Especie.Raposa);
                if (painelSpawnRaposa != null) {
                    painelSpawnRaposa.painelSpawn.SetActive(true);
                }

                break;
            case 3:

                painelResetar.SetActive(true);
                painelBloqueioResetarJogo.SetActive(false);

                PainelSpawn[] paineisSpawnLiberados = Array.FindAll(paineisSpawn, x => (x.especie == Especie.Coelho || x.especie == Especie.Raposa));
                foreach (PainelSpawn painelSpawnLiberado in paineisSpawnLiberados)
                {
                    painelSpawnLiberado.painelSpawn.SetActive(true);
                }

                ConcluirEtapaTutorial();
                MostrarTutorial();
                break;
            case 4:
                ConcluirEtapaTutorial();

                StartCoroutine(AguardarExecutar(10f, () =>
                {
                        ExibirPainelTutorial(
                        "Prepare-se! \n\n" +
                        "Em breve, questionarios serao aplicados para testar seus conhecimentos sobre o ecossistema. \n\n" +
                        "Fique atento e mostre o que aprendeu!",
                        "Images/Tutorial/AvaliacaoTutorial"
                    );
                }));

                IniciarMissaoAvaliacaoEcologica();

                break;
            case 5:

                if (GameManager.Instance.faseAtual == Fase.Fase02)
                {
                    StartCoroutine(AguardarExecutar(5f, () =>
                    {
                        ExibirPainelTutorial(
                            "Fase Concluída! \n\n" +
                            "Você liberou duas novas especies! O javali, um consumidor primario que se alimenta de plantas, e o lobo, um consumidor secundario que caça outros animais. \n\n" +
                            "Veja como eles influenciam o equilibrio do ecossistema!",
                            "Images/Tutorial/LoboJavali"
                        );
                    }));

                    ConcluirEtapaTutorial();
                }

                break;
            case 6:

                if (GameManager.Instance.faseAtual == Fase.Fase03)
                {
                    StartCoroutine(AguardarExecutar(5f, () =>
                    {
                        ExibirPainelTutorial(
                            "Fase Concluída! \n\n" +
                            "Otima notícia! Voce liberou duas novas especies: o cervo, um consumidor primario que se alimenta de plantas, e o urso, um poderoso consumidor terciario que ocupa o topo da cadeia alimentar. \n\n" +
                            "Observe como essas novas especies interagem no ecossistema!",
                            "Images/Tutorial/UrsoCervo"
                        );
                    }));

                    ConcluirEtapaTutorial();
                }

                break;

            case 7:

                if (GameManager.Instance.faseAtual == Fase.Fase04)
                {
                    StartCoroutine(AguardarExecutar(5f, () =>
                    {
                        ExibirPainelTutorial(
                            "Parabens! Voce concluiu o jogo e restaurou o equilibrio do ecossistema! \n\n" +
                            "Mas a jornada nao termina aqui! Continue explorando e simulando o ecossistema para ver como suas decisoes afetam o ambiente ao longo do tempo!",
                            "Images/Tutorial/ConclusaoJogo"
                        );

                        AudioManager.instance.PlayEfeito("SucessoCampanha");
                    }));
                }

                break;
        }
    }

    public IEnumerator AguardarExecutar(float delay, System.Action onDelayComplete)
    {
        yield return new WaitForSeconds(delay);
        onDelayComplete?.Invoke();
    }

    private void ExibirDialogo(string mensagem)
    {
        GameObject[] telas = GameObject.FindGameObjectsWithTag("TelaExibicao");
        foreach (GameObject tela in telas)
        {
            tela.SetActive(false);
        }

        AudioManager.instance.PlayDialogo("Dialogo");
        textoDialogo.text = mensagem;
        painelMensagemDialogo.SetActive(true);
    }

    private void HabilitaPular()
    {
        permitePular = true;
        painelPermitePular.SetActive(true);
    }

    private void ExibirPainelTutorial(string texto, string tutorialImagePath)
    {
        textoTutorial.text = texto;
        imagemTutorial.sprite = Resources.Load<Sprite>(tutorialImagePath);
        AudioManager.instance.PlayEfeito("Tutorial");
        painelTutorial.SetActive(true);
    }

    private void ExibirPainelMissao(string tituloMissaoText, string descricaoMissaoText, string missaoImagePath)
    {
        tituloMissao.text = tituloMissaoText;
        descricaoMissao.text = descricaoMissaoText;
        imagemMissao.sprite = Resources.Load<Sprite>(missaoImagePath);
        painelMissao.SetActive(true);
    }

    private void ConcluirEtapaTutorial()
    {
        painelTutorial.SetActive(false);
        painelMissao.SetActive(false);

        GameManager.Instance.passoAtual++;
        AudioManager.instance.PlayEfeito("SucessoMissao");
    }

    private int ContarEspecie<T>(Especie especie) where T : Animal
    {
        T[] todosAnimais = FindObjectsOfType<T>();
        int count = 0;

        foreach (T animal in todosAnimais)
        {
            if (animal.especie == especie)
            {
                count++;
            }
        }

        return count;
    }

    private void IniciarMissaoAvaliacaoEcologica()
    {
        ExibirPainelMissao(
            "Avaliacao Ecologica",
            GetTempoRestanteParaAvaliacao().ToString(),
            "Images/Animais/Pesquisa"
        );

        StartCoroutine(AtualizarTempoRestanteMissao());
        StartCoroutine(RequisitarAvaliacoesPeriodicamente());
    }

    public void VerificarTutorialReproducao()
    {
        if (GameManager.Instance.passoAtual == 2 && !tutorialReproducao)
        {
            StartCoroutine(AguardarExecutar(5f, MostrarTutorial));
            tutorialReproducao = true;
        }
    }

    public IEnumerator RequisitarAvaliacoesPeriodicamente()
    {
        ultimoTempoAvaliacao = Time.time;
        while (true)
        {
            yield return new WaitForSeconds(intervaloAvaliacao);
            StartCoroutine(RequisitarAvalicao());
        }
    }

    private IEnumerator RequisitarAvalicao()
    {
        ExibirDialogo("Chegou o momento de uma avaliacao ecologica!\n\n" +
                        "Agora vamos testar o seu conhecimento e ver como esta o seu progresso revitalizando o ecossistema.");
        yield return new WaitForSeconds(10f);
        sceneHandler.NavegarParaAvaliacaoEcologica();
        ultimoTempoAvaliacao = Time.time;
    }

    private IEnumerator AtualizarTempoRestanteMissao()
    {
        while (true)
        {
            float tempoRestanteSegundos = GetTempoRestanteParaAvaliacao();
            int minutos = Mathf.FloorToInt(tempoRestanteSegundos / 60f);
            int segundos = Mathf.FloorToInt(tempoRestanteSegundos % 60);

            // Formata o tempo no formato MM:SS
            string tempoFormatado = string.Format("{0:00}:{1:00}", minutos, segundos);
            descricaoMissao.text = $"Tempo Restante: {tempoFormatado}";
            yield return new WaitForSeconds(1f);
        }
    }

    public float GetTempoRestanteParaAvaliacao()
    {
        float tempoDecorrido = Time.time - ultimoTempoAvaliacao;
        return Mathf.Max(0f, intervaloAvaliacao - tempoDecorrido);
    }
}
