[System.Serializable]
public class HerbivoroData : AnimalData
{
    public float distanciaConsumoGrama;
    public string gramaAlvo; // Nome ou ID do alvo de grama

    public HerbivoroData(Herbivoro herbivoro) : base(herbivoro)
    {
        distanciaConsumoGrama = herbivoro.distanciaConsumoGrama;
        gramaAlvo = null;
    }
}