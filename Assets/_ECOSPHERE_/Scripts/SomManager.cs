using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomManager : MonoBehaviour
{
    public enum TipoSom
    {
        PontoDeVida
    }

    public List<AudioClip> listaDeSons;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TocarSom(TipoSom tipoSom)
    {
        int indice = (int) tipoSom;
        if (indice >= 0 && indice < listaDeSons.Count)
        {
            audioSource.clip = listaDeSons[indice];
            audioSource.Play();
        }
    }
}
