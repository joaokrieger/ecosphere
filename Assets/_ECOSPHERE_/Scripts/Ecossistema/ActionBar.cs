using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    public GameObject subActionBar;
    private bool estaExibindoSubActionBar = false;

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
        SceneManager.LoadScene("AvaliacaoEcologica");
    }

    public void SalvarJogo()
    {
        
    }
}
