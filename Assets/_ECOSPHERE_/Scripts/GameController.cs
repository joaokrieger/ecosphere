using UnityEngine;

public class GameController
{
    private static GameController instancia;
    private int predador = 0;
    private int presa = 0;
    private int produtor = 0;
    private CarteiraPontoVida carteira = new CarteiraPontoVida();

    private GameController(){}

    public static GameController GetInstance()
    {
        if (instancia == null)
        {
            instancia = new GameController();
        }
        return instancia;
    }

    public enum Entidade
    {
        Predador,
        Presa,
        Produtor
    }

    private void AlterarQuantidade(Entidade entidade, int quantidade)
    {
        switch (entidade)
        {
            case Entidade.Predador:
                predador += quantidade;
                break;
            case Entidade.Presa:
                presa += quantidade;
                break;
            case Entidade.Produtor:
                produtor += quantidade;
                break;
        }
    }

    public void Add(Entidade entidade)
    {
        AlterarQuantidade(entidade, 1);
    }

    public void Remove(Entidade entidade)
    {
        AlterarQuantidade(entidade, -1);
    }

    public int GetQuantidade(Entidade entidade) {
        switch (entidade)
        {
            case Entidade.Predador:
                return predador;
            case Entidade.Presa:
                return presa;
            case Entidade.Produtor:
                return produtor;
            default:
                return 0;
        }
    }

    public CarteiraPontoVida GetCarteiraPontoVida() {
        return carteira;
    }
}
