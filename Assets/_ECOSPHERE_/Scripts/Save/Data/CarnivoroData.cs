[System.Serializable]
public class CarnivoroData : AnimalData
{
    public string[] presas;
    public int danoAtaque;
    public float distanciaAtaque;
    public float velocidadeAtaque;
    public string presaAlvo; // Nome ou ID do alvo
    public string prefabName; // Nome do prefab

    public CarnivoroData(Carnivoro carnivoro) : base(carnivoro)
    {
        presas = new string[carnivoro.presas.Length];
        for (int i = 0; i < carnivoro.presas.Length; i++)
        {
            presas[i] = carnivoro.presas[i].ToString();
        }
        danoAtaque = carnivoro.danoAtaque;
        distanciaAtaque = carnivoro.distanciaAtaque;
        velocidadeAtaque = carnivoro.velocidadeAtaque;
        presaAlvo = null;
    }
}
