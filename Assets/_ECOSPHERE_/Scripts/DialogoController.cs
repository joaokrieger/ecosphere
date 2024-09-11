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
    private bool permitePular;

    void Start()
    {
        GameObject sceneHandlerObject = GameObject.FindGameObjectWithTag("SceneHandler");
        sceneHandler = sceneHandlerObject.GetComponent<SceneHandler>();
        MensagemInicial();
    }

    void Update()
    {
        if (Input.anyKeyDown && permitePular)
        {
            painelMensagemDialogo.SetActive(false);
            painelPermitePular.SetActive(false);
        }
    }

    private void MensagemInicial()
    {
        string mensagem = "Bem-vindo ao Ecosphere, um mundo onde a natureza precisa da sua ajuda.\n\n Boa sorte nesta nova jornada meu jovem!";
        ExibirDialogo(mensagem);
    }

    private void ExibirDialogo(string mensagem)
    {
        ExibirMensagem(mensagem);
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
}
