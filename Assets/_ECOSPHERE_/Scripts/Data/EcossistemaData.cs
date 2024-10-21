using UnityEngine;

[System.Serializable]
public class EcossistemaData
{
    public string fase;
    public string tutorial;
    public int pontuacaoJogador;

    public EcossistemaData()
    {
        pontuacaoJogador = GameManager.Instance.pontuacaoJogador;
        fase = GameManager.Instance.faseAtual.ToString();
        tutorial = GameManager.Instance.tutorial.ToString();
    }
}
