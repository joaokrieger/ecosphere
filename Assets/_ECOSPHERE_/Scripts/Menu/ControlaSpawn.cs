using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControlaSpawn : MonoBehaviour
{

    public Button spawnCoelho;
    public Button spawnRaposa;
    public Button spawnJavali;
    public Button spawnLobo;
    public Button spawnCervo;
    public Button spawnUrso;

    public int limiteCoelho = 10;
    public int limiteRaposa = 5;
    public int limiteJavali = 6;
    public int limiteLobo = 4;
    public int limiteCervo = 6;
    public int limiteUrso = 2;

    private int countCoelho;
    private int countRaposa;
    private int countJavali;
    private int countLobo;
    private int countCervo;
    private int countUrso;

    void Update()
    {
        Supervisionar();

    }

    private void Supervisionar()
    {
        countCoelho = ContarEspecie<Herbivoro>(Especie.Coelho);
        countJavali = ContarEspecie<Herbivoro>(Especie.Javali);
        countCervo = ContarEspecie<Herbivoro>(Especie.Cervo);
        countRaposa = ContarEspecie<Carnivoro>(Especie.Raposa);
        countLobo = ContarEspecie<Carnivoro>(Especie.Lobo);
        countUrso = ContarEspecie<Carnivoro>(Especie.Urso);

        spawnCoelho.interactable = countCoelho < limiteCoelho;
        spawnRaposa.interactable = countRaposa < limiteRaposa;
        spawnJavali.interactable = countJavali < limiteJavali;
        spawnLobo.interactable = countLobo < limiteLobo;
        spawnCervo.interactable = countCervo < limiteCervo;
        spawnUrso.interactable = countUrso < limiteUrso;
    }

    private int ContarEspecie<T>(Especie especie) where T : Animal
    {
        T[] todosAnimais = FindObjectsOfType<T>();
        int count = 0;

        foreach (T animal in todosAnimais)
        {
            if (animal.especie == especie)
            {
                count++;
            }
        }

        return count;
    }
}
