using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPrincipal : MonoBehaviour
{
    private SceneHandler sceneHandler;

    private void Start()
    {
        GameObject sceneHandlerObject = GameObject.FindGameObjectWithTag("SceneHandler");
        if (sceneHandlerObject != null)
        {
            sceneHandler = sceneHandlerObject.GetComponent<SceneHandler>();
        }
    }

    public void NovoJogo()
    {
        GameManager.Instance.ExcluirArquivoJson();
        sceneHandler.NavegarParaEcossistema();
    }

    public void CarregarJogo()
    {
        sceneHandler.NavegarParaEcossistema();
    }

    public void SobreJogo()
    {

    }

    public void SairDoJogo()
    {
        Application.Quit();
    }
}
