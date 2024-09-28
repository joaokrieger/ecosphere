using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogoController : MonoBehaviour
{
    private SceneHandler sceneHandler;
    public GameObject painelMensagemDialogo;
    public Text textoDialogo;
    public GameObject painelPermitePular;
    public TutorialController tutorialController;
    private float intervaloAvaliacao = 220f; // 2 minutos 
    private bool permitePular;

    void Start()
    {
        GameObject sceneHandlerObject = GameObject.FindGameObjectWithTag("SceneHandler");
        sceneHandler = sceneHandlerObject.GetComponent<SceneHandler>();
        if (GameManager.Instance.faseAtual == Fase.Fase01)
        {
            StartCoroutine(MensagemInicial());
        }
    }

    void Update()
    {
        if (Input.anyKeyDown && permitePular)
        {
            painelMensagemDialogo.SetActive(false);
            painelPermitePular.SetActive(false);
            StartCoroutine(tutorialController.ShowTutorialSpawnCoelho());
        }
    }

    private IEnumerator MensagemInicial()
    {
        string mensagem = "Bem-vindo ao Ecosphere, um mundo onde a natureza precisa da sua ajuda.\n\n" +
            "Boa sorte nesta nova jornada meu jovem!";
        ExibirMensagem(mensagem);

        yield return new WaitForSeconds(10f);

        mensagem = "Restaure o equilibrio natural deste ecossistema! \n\n" +
            "Adicione novas especies e descubra como cada uma delas contribui para o funcionamento harmonioso do ambiente.";
        ExibirMensagem(mensagem);

        yield return new WaitForSeconds(10f);
        HabilitaPular();
    }

    private IEnumerator RequisitarAvalicao(string mensagem)
    {
        ExibirMensagem(mensagem);
        yield return new WaitForSeconds(10f);
        sceneHandler.NavegarParaAvaliacaoEcologica();
    }

    private void ExibirMensagem(string mensagem)
    {
        AudioManager.instance.PlayDialogo("Dialogo");
        GameObject[] telas = GameObject.FindGameObjectsWithTag("TelaExibicao");
        foreach (GameObject tela in telas)
        {
            tela.SetActive(false);
        }

        permitePular = false;
        textoDialogo.text = mensagem;
        painelMensagemDialogo.SetActive(true);
    }

    private void HabilitaPular()
    {
        permitePular = true;
        painelPermitePular.SetActive(true);
    }

    public IEnumerator RequisitarAvaliacoesPeriodicamente()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloAvaliacao);
            string mensagem = "Tempo para avaliação ecológica! Vamos ver como está o seu progresso.";
            StartCoroutine(RequisitarAvalicao(mensagem));
        }
    }
}
