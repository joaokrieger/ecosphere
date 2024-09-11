using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class AnimalData
{
    public string especie;
    public int vida;
    public float velocidade;
    public float tempoFome;
    public float tempoApetite;
    public float intervaloReproducao;
    public float tempoReproducao;
    public bool morreu;
    public float saciedade;
    public Vector3 position; // Posição do animal no mundo
    public Vector3 destination; // Destino atual

    public AnimalData(Animal animal)
    {
        especie = animal.especie.ToString();
        vida = animal.vida;
        velocidade = animal.velocidade;
        tempoFome = animal.tempoFome;
        tempoApetite = animal.tempoApetite;
        intervaloReproducao = animal.intervaloReproducao;
        tempoReproducao = animal.tempoReproducao;
        morreu = !animal.EstaVivo();
        saciedade = animal.saciedade;
        position = animal.transform.position;

        var navMeshAgent = animal.GetComponent<NavMeshAgent>();
        destination = navMeshAgent != null ? navMeshAgent.destination : Vector3.zero;
    }
}
