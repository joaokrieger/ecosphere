using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] RectTransform fader;
    private void Start()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha (fader, 1, 0);
        LeanTween.alpha (fader, 0, 1f).setOnComplete (() => {
             fader.gameObject.SetActive (false);
        });
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
            GameManager.Instance.SalvarJogo();
            SceneManager.LoadScene("AvaliacaoEcologica");
        });
    }
}
