using UnityEngine;

[System.Serializable]
public class EcossistemaData
{
    public string fase;
    public int passoAtual;
    public int pontuacaoJogador;

    public EcossistemaData()
    {
        pontuacaoJogador = GameManager.Instance.pontuacaoJogador;
        fase = GameManager.Instance.faseAtual.ToString();
        passoAtual = GameManager.Instance.passoAtual;
    }
}
