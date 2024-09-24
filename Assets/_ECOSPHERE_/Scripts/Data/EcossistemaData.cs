using UnityEngine;

[System.Serializable]
public class EcossistemaData
{
    public string fase;
    public int pontosVida;

    public EcossistemaData()
    {
        fase = GameManager.Instance.faseAtual.ToString();
        pontosVida = GameManager.Instance.GetSaldo();
    }
}
