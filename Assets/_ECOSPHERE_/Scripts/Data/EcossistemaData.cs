using UnityEngine;

[System.Serializable]
public class EcossistemaData
{
    public string fase;
    public string tutorial;
    public int pontosVida;

    public EcossistemaData()
    {
        fase = GameManager.Instance.faseAtual.ToString();
        tutorial = GameManager.Instance.tutorial.ToString();
    }
}
