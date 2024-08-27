using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class DialogoController : MonoBehaviour
{
    private SceneHandler sceneHandler;
    public GameObject painelMensagemDialogo;
    public Text textoDialogo;
    public GameObject painelPermitePular;
    public float tempoExibicao = 5f;

    private bool permitePular = false;

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
        ExibirMensagem(mensagem);
    }

    private void ExibirMensagem(string mensagem)
    {
        textoDialogo.text = mensagem;
        painelMensagemDialogo.SetActive(true);
        StartCoroutine(HabilitaPular(tempoExibicao));
    }

    private IEnumerator HabilitaPular(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        permitePular = true;
        painelPermitePular.SetActive(true);
    }
}
