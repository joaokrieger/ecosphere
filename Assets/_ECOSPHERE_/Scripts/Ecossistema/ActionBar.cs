using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    public GameObject subActionBar;
    private bool estaExibindoSubActionBar = false;

    private SceneHandler sceneHandler;

    private void Start()
    {
        GameObject sceneHandlerObject = GameObject.FindGameObjectWithTag("SceneHandler");
        sceneHandler = sceneHandlerObject.GetComponent<SceneHandler>();
    }

    public void RenderizaSubActionBar()
    {
        estaExibindoSubActionBar = !estaExibindoSubActionBar;
        subActionBar.SetActive(estaExibindoSubActionBar);
    }

    public void NavegarParaMenu()
    {
        
    }

    public void NavegarParaPesquisa()
    {
        sceneHandler.NavegarParaAvaliacaoEcologica();
    }

    public void SalvarJogo()
    {
        
    }
}
