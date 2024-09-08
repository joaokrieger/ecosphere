using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    public GameObject biodex;

    private SceneHandler sceneHandler;

    private void Start()
    {
        GameObject sceneHandlerObject = GameObject.FindGameObjectWithTag("SceneHandler");
        sceneHandler = sceneHandlerObject.GetComponent<SceneHandler>();
    }

    public void RenderizaBiodex()
    {
        biodex.SetActive(!biodex.activeSelf);
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
