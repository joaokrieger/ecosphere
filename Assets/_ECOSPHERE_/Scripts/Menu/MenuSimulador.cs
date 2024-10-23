using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSimulador : MonoBehaviour
{
    public GameObject painelMenuPausa;

    void Update()
    {
        if (GameObject.FindWithTag("TelaExibicao") == null && GameObject.FindWithTag("TelaDialogo") == null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                painelMenuPausa.SetActive(!painelMenuPausa.activeSelf);
            }
        }
        else
        {
            painelMenuPausa.SetActive(false);
        }
    }
}
