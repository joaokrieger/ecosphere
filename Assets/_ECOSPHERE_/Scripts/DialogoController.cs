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
    public float tempoExibicao = 5f;
    public float intervaloAvaliacao = 20f; // 2 minutos
    private bool permitePular;

    void Start()
    {
        GameObject sceneHandlerObject = GameObject.FindGameObjectWithTag("SceneHandler");
        sceneHandler = sceneHandlerObject.GetComponent<SceneHandler>();
        StartCoroutine(RequisitarAvaliacoesPeriodicamente());
        if (GameManager.Instance.faseAtual == Fase.Introducao)
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
        }
    }

    private IEnumerator MensagemInicial()
    {
        string mensagem = "Bem-vindo ao Ecosphere, um mundo onde a natureza precisa da sua ajuda.\n\n Boa sorte nesta nova jornada meu jovem!";
        ExibirMensagem(mensagem);

        yield return new WaitForSeconds(tempoExibicao);

        mensagem = "Restaure o equilibrio natural deste ecossistema. \n\nAdicione novas especies e descubra como cada uma delas contribui para o funcionamento harmonioso do ambiente";
        ExibirMensagem(mensagem);

        yield return new WaitForSeconds(tempoExibicao);

        GameManager.Instance.AtualizarFase(Fase.Fase1);
        StartCoroutine(HabilitaPular());
    }

    private IEnumerator RequisitarAvalicao(string mensagem)
    {
        ExibirMensagem(mensagem);
        yield return new WaitForSeconds(tempoExibicao);
        sceneHandler.NavegarParaAvaliacaoEcologica();
    }

    private void ExibirMensagem(string mensagem)
    {
        AudioManager.instance.PlayEfeito("Dialogo");
        GameObject[] telas = GameObject.FindGameObjectsWithTag("TelaExibicao");
        foreach (GameObject tela in telas)
        {
            tela.SetActive(false);
        }

        permitePular = false;
        textoDialogo.text = mensagem;
        painelMensagemDialogo.SetActive(true);
    }

    private IEnumerator HabilitaPular()
    {
        yield return new WaitForSeconds(tempoExibicao);
        permitePular = true;
        painelPermitePular.SetActive(true);
    }

    private IEnumerator RequisitarAvaliacoesPeriodicamente()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloAvaliacao);
            string mensagem = "Tempo para avaliação ecológica! Vamos ver como está o seu progresso.";
            StartCoroutine(RequisitarAvalicao(mensagem));
        }
    }
}
