using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [Header("Painel do Dialogo")]
    public DialogoController dialogoController;

    [Header("Painel do Tutorial")]
    public GameObject painelTutorial;
    public Text textoTutorial;
    public Image imagemTutorial;

    [Header("Painel Miss�o")]
    public GameObject painelMissao;
    public Text tituloMissao;
    public Text descricaoMissao;
    public Image imagemMissao;

    [Header("Painel Spawn Animais")]
    public GameObject painelSpawnCoelho;
    public GameObject painelSpawnRaposa;
    public GameObject painelSpawnJavali;
    public GameObject painelSpawnLobo;
    public GameObject painelSpawnCervo;
    public GameObject painelSpawnUrso;

    private bool tutorialSpawnCoelho = false;
    private bool tutorialSpawnRaposa = false;
    private bool tutorialSpawnJavali = false;
    private bool tutorialSpawnLobo = false;

    public bool coelhoMissaoConcluida = false;
    public bool raposaMissaoConcluida = false;
    public bool javaliMissaoConcluida = false;
    public bool loboMissaoConcluida = false;

    private const int requisitoCoelho = 10;
    private const int requisitoRaposa = 2;
    private const int requisitoJavali = 4;
    private const int requisitoLobo = 2;

    void Update()
    {
        if (tutorialSpawnCoelho && !coelhoMissaoConcluida)
        {
            AtualizarMissao(Especie.Coelho, requisitoCoelho, () =>
            {
                coelhoMissaoConcluida = true;
                tutorialSpawnCoelho = false;
                OcultarPainelMissao();
                StartCoroutine(AguardarETrocarMissao(30f, ShowTutorialSpawnRaposa));
            });
        }

        if (tutorialSpawnRaposa && !raposaMissaoConcluida)
        {
            AtualizarMissao(Especie.Raposa, requisitoRaposa, () =>
            {
                raposaMissaoConcluida = true;
                tutorialSpawnRaposa = false;
                OcultarPainelMissao();
                StartCoroutine(AguardarETrocarMissao(20f, ShowTutorialSpawnJavali));
            });
        }

        if (tutorialSpawnJavali && !javaliMissaoConcluida)
        {
            AtualizarMissao(Especie.Javali, requisitoRaposa, () =>
            {
                javaliMissaoConcluida = true;
                tutorialSpawnJavali = false;
                OcultarPainelMissao();
                StartCoroutine(AguardarETrocarMissao(20f, ShowTutorialSpawnLobo));
            });
        }

        if (tutorialSpawnLobo && !loboMissaoConcluida)
        {
            AtualizarMissao(Especie.Lobo, requisitoLobo, () =>
            {
                loboMissaoConcluida = true;
                tutorialSpawnLobo = false;
                OcultarPainelMissao();
                dialogoController.RequisitarAvaliacoesPeriodicamente();
            });
        }

        if (painelTutorial.activeSelf && Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
        }
        else if (!painelTutorial.activeSelf && Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    private void AtualizarMissao(Especie especieRequerida, int quantidadeRequerida, System.Action onSuccess)
    {
        Animal[] animais = FindObjectsOfType<Animal>();
        int total = 0;
        foreach (Animal animal in animais)
        {
            if (animal.especie == especieRequerida)
            {
                total++;
            }
        }

        descricaoMissao.text = $"{total}/{quantidadeRequerida}";

        if (total >= quantidadeRequerida)
        {
            AudioManager.instance.PlayEfeito("SucessoMissao");
            onSuccess?.Invoke();
        }
    }


    public IEnumerator ShowTutorialSpawnCoelho()
    {
        if (!tutorialSpawnCoelho && !coelhoMissaoConcluida)
        {
            tutorialSpawnCoelho = true;
            yield return new WaitForSeconds(5f);

            ExibirPainelTutorial(
                "Para iniciarmos a popular o nosso ecossistema, selecione uma especie disponivel no painel. \r\n\r\n" +
                "Observe que a grama esta crescendo sem parar, introduza uma populacao de coelhos para controla-la!",
                "Images/Tutorial/SpawnAnimalCoelho"
            );

            ExibirPainelMissao(
                "Adicione Coelhos",
                "0/10",
                "Images/Animais/Coelho"
            );

            AudioManager.instance.PlayEfeito("Tutorial");
            painelSpawnCoelho.SetActive(true);
        }
    }

    public void ShowTutorialPontoVida()
    {
        ExibirPainelTutorial(
            "Voce esta sem pontos de vida suficientes para adicionar essa especie. \r\n\r\n" +
            "A alimentacao e a reproducao dos animais geram pontos de vida, entao fique atento aos comportamentos naturais e acumule saldo para novas adicoes.",
            "Images/Tutorial/PontoVida"
        );

        AudioManager.instance.PlayEfeito("Tutorial");
    }

    public void ShowTutorialSpawnRaposa()
    {
        if (!tutorialSpawnRaposa && !raposaMissaoConcluida)
        {
            ExibirPainelTutorial(
                "Os coelhos estao comecando a se reproduzir rapidamente, e em breve poderao superlotar o ecossistema! \r\n\r\n" +
                "Observe na imagem! A raposa pode ser o predador ideal para controlar a populacao de coelhos.",
                "Images/Tutorial/SpawnAnimalRaposa"
            );

            ExibirPainelMissao(
                "Adicione Raposas",
                "0/2",
                "Images/Animais/Raposa"
            );

            AudioManager.instance.PlayEfeito("Tutorial");
            painelSpawnRaposa.SetActive(true);
            tutorialSpawnRaposa = true;
        }
    }

    public void ShowTutorialSpawnJavali()
    {
        if (!tutorialSpawnJavali && !javaliMissaoConcluida)
        {
            ExibirPainelTutorial(
                "Lembre-se: os coelhos se alimentam da grama, e as raposas se alimentam dos coelhos. \r\n\r\n" +
                "Que tal adicionarmos uma nova especie? Estou com alguns javalis prontos para serem liberados!",
                "Images/Tutorial/SpawnAnimalJavali"
            );

            ExibirPainelMissao(
                "Adicione Javalis",
                "0/2",
                "Images/Animais/Javali"
            );

            AudioManager.instance.PlayEfeito("Tutorial");
            painelSpawnJavali.SetActive(true);
            tutorialSpawnJavali = true;
        }
    }

    public void ShowTutorialSpawnLobo()
    {
        if (!tutorialSpawnLobo && !loboMissaoConcluida)
        {
            ExibirPainelTutorial(
                "Algo esta errado... Sem predadores, os javalis podem acabar consumindo todos os recursos disponiveis! \r\n\r\n" +
                "Esta na hora de introduzir um predador de topo na cadeia alimentar!",
                "Images/Tutorial/SpawnAnimalLobo"
            );

            ExibirPainelMissao(
                "Adicione Lobos",
                "0/2",
                "Images/Animais/Lobo"
            );

            AudioManager.instance.PlayEfeito("Tutorial");
            painelSpawnLobo.SetActive(true);
            tutorialSpawnLobo = true;
        }
    }

    private void ExibirPainelTutorial(string texto, string tutorialImagePath)
    {
        textoTutorial.text = texto;
        imagemTutorial.sprite = Resources.Load<Sprite>(tutorialImagePath);
        painelTutorial.SetActive(true);
    }

    private void ExibirPainelMissao(string tituloMissaoText, string descricaoMissaoText, string missaoImagePath)
    {
        tituloMissao.text = tituloMissaoText;
        descricaoMissao.text = descricaoMissaoText;
        imagemMissao.sprite = Resources.Load<Sprite>(missaoImagePath);
        painelMissao.SetActive(true);
    }

    private void OcultarPainelMissao()
    {
        painelMissao.SetActive(false);
        painelTutorial.SetActive(false);;
    }

    private IEnumerator AguardarETrocarMissao(float delay, System.Action onDelayComplete)
    {
        yield return new WaitForSeconds(delay);
        onDelayComplete?.Invoke();
    }
}