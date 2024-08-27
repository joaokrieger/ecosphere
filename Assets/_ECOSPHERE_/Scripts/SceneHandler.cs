using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] RectTransform fader;

    public enum EstadoJogo
    {
        Gameplay,
        Pausado
    }

    private EstadoJogo estadoAtual;

    private void Start()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha (fader, 1, 0);
        LeanTween.alpha (fader, 0, 1f).setOnComplete (() => {
             fader.gameObject.SetActive (false);
        });

        estadoAtual = EstadoJogo.Gameplay;
    }

    public void NavegarParaEcossistema()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha (fader, 0, 0);
        LeanTween.alpha (fader, 1, 1f).setOnComplete (() => {
            SceneManager.LoadScene("SimuladorEcossistema");
        });
    }

    public void NavegarParaAvaliacaoEcologica()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 1f).setOnComplete(() => {
            SceneManager.LoadScene("AvaliacaoEcologica");
        });
    }

    public void AlterarEstadoJogo(EstadoJogo estadoJogo)
    {
        estadoAtual = estadoJogo;

        if (estadoJogo == EstadoJogo.Pausado)
        {
            Time.timeScale = 0f;
        }
        else if (estadoJogo == EstadoJogo.Gameplay)
        {
            Time.timeScale = 1f;
        }
    }

    public EstadoJogo GetEstadoJogo()
    {
        return estadoAtual;
    }
}
