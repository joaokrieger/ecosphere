public class GameController
{
    private static GameController instancia;
    private int quantidadeCarnivoro = 0;
    private int quantidadeHerbivoro = 0;
    private int quantidadeProdutor = 0;
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
        Animais,
        Carnivoro,
        Herbivoro,
        Produtor
    }

    private void AlterarQuantidade(Entidade entidade, int quantidade)
    {
        switch (entidade)
        {
            case Entidade.Carnivoro:
                quantidadeCarnivoro += quantidade;
                break;
            case Entidade.Herbivoro:
                quantidadeHerbivoro += quantidade;
                break;
            case Entidade.Produtor:
                quantidadeProdutor += quantidade;
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
            case Entidade.Carnivoro:
                return quantidadeCarnivoro;
            case Entidade.Herbivoro:
                return quantidadeHerbivoro;
            case Entidade.Produtor:
                return quantidadeProdutor;
            case Entidade.Animais:
                return quantidadeHerbivoro + quantidadeCarnivoro;
            default:
                return 0;
        }
    }

    public CarteiraPontoVida GetCarteiraPontoVida() {
        return carteira;
    }
}
