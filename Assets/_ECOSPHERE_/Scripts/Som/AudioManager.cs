using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public Som[] musicas, efeitos;
    public AudioSource playerMusica, playerEfeito;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusica("Tema");
    }

    public void PlayMusica(string nome)
    {

        Som som = Array.Find(musicas, x => x.nome == nome);
        if (som != null)
        {
            playerMusica.clip = som.clip;
            playerMusica.Play();
        }
    }

    public void PlayEfeito(string nome)
    {

        Som som = Array.Find(efeitos, x => x.nome == nome);
        if (som != null)
        {
            playerEfeito.clip = som.clip;
            playerEfeito.Play();
        }
    }
}
