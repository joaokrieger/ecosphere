using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [Header("Painel do Tutorial")]
    public GameObject painelTutorial;
    public Text textoTutorial;
    public Image imagemTutorial;

    [Header("Painel Missão")]
    public GameObject painelMissao;
    public Text tituloMissao;
    public Text descricaoMissao;
    public Image imagemMissao;

    private bool tutorialSpawnCoelho = false;
    private bool tutorialSpawnRaposa = false;

    private const int requiredCoelhos = 10;
    private const int requiredRaposas = 2;

    void Update()
    {
        if (tutorialSpawnCoelho)
        {
            AtualizarMissao("Presa", requiredCoelhos, ShowTutorialSpawnRaposa);
        }

        if (tutorialSpawnRaposa)
        {
            AtualizarMissao("Predador", requiredRaposas, () =>
            {
                tutorialSpawnRaposa = false;
                OcultarPainelMissao();
            });
        }
    }

    // Método para atualizar as missões
    private void AtualizarMissao(string tag, int quantidadeRequerida, System.Action onSuccess)
    {
        int total = GameObject.FindGameObjectsWithTag(tag).Length;
        descricaoMissao.text = $"{total}/{quantidadeRequerida}";

        if (total >= quantidadeRequerida)
        {
            AudioManager.instance.PlayEfeito("SucessoMissao");
            onSuccess?.Invoke();
        }
    }

    public void ShowTutorialSpawnCoelho()
    {
        if (!tutorialSpawnCoelho)
        {
            ExibirTutorial(
                "Para iniciarmos a popular o nosso ecossistema, selecione uma especie disponivel no painel. \r\n\r\n" +
                "Cada especie tem um custo em pontos de vida, use seus recursos com sabedoria.\r\n\r\n" +
                "Observe que a grama esta crescendo sem parar, introduza uma populacao de coelhos para controla-la!",
                "Images/Tutorial/SpawnAnimalCoelho",
                "Adicione Coelhos",
                "0/10",
                "Images/Animais/Coelho");

            tutorialSpawnCoelho = true;
        }
    }

    public void ShowTutorialSpawnRaposa()
    {
        if (!tutorialSpawnRaposa)
        {
            ExibirTutorial(
                "Os coelhos estao comecando a se reproduzir rapidamente, e em breve poderao superlotar o ecossistema! \r\n\r\n" +
                "Para manter o equilibrio, introduza um predador que ajude a controlar essa populacao.\r\n\r\n" +
                " Observe na imagem! A raposa pode ser o predador ideal para controlar a populacao de coelhos.",
                "Images/Tutorial/SpawnAnimalRaposa",
                "Adicione Raposas",
                "0/2",
                "Images/Animais/Raposa");

            tutorialSpawnRaposa = true;
        }
    }

    // Método genérico para exibir o tutorial
    private void ExibirTutorial(string texto, string tutorialImagePath, string tituloMissaoText, string descricaoMissaoText, string missaoImagePath)
    {
        textoTutorial.text = texto;
        imagemTutorial.sprite = Resources.Load<Sprite>(tutorialImagePath);
        painelTutorial.SetActive(true);

        tituloMissao.text = tituloMissaoText;
        descricaoMissao.text = descricaoMissaoText;
        imagemMissao.sprite = Resources.Load<Sprite>(missaoImagePath);
        painelMissao.SetActive(true);
    }

    // Método para ocultar o painel de missão
    private void OcultarPainelMissao()
    {
        painelMissao.SetActive(false);
    }
}
