using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadGame : MonoBehaviour
{
    public GameObject[] prefabEspecies;
    private string enderecoArquivoJson; 

    [System.Serializable]
    public class GameData
    {
        public CarnivoroData[] carnivorosData;
        public HerbivoroData[] herbivorosData;
    }

    // Start is called before the first frame update
    void Start()
    {
        enderecoArquivoJson = Path.Combine(Application.persistentDataPath, "gameData.json");
    }

    public void SalvarJogo()
    {

        CarnivoroData[] carnivorosData = GetCarnivorosData();
        HerbivoroData[] herbivorosData = GetHerbivorosData();
        GameData gameData = new GameData
        {
            carnivorosData = carnivorosData,
            herbivorosData = herbivorosData
        };

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(enderecoArquivoJson, json);
        Debug.Log($"Arquivo JSON salvo em: {enderecoArquivoJson}");
    }

    public void CarregarJogo()
    {
        if (File.Exists(enderecoArquivoJson))
        {
            string json = File.ReadAllText(enderecoArquivoJson);
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            foreach (CarnivoroData carnivoroData in gameData.carnivorosData)
            {
                InstanciarCarnivoro(carnivoroData);
            }

            foreach (HerbivoroData herbivoroData in gameData.herbivorosData)
            {
                InstanciarHebivoro(herbivoroData);
            }
        }
    }

    private CarnivoroData[] GetCarnivorosData()
    {
        GameObject[] carnivoros = GameObject.FindGameObjectsWithTag("Predador");
        CarnivoroData[] carnivorosData = new CarnivoroData[carnivoros.Length];

        for (int i = 0; i < carnivoros.Length; i++)
        {
            GameObject carnivoroGO = carnivoros[i];
            Carnivoro carnivoro = carnivoroGO.GetComponent<Carnivoro>();

            if (carnivoro != null)
            {
                CarnivoroData carnivoroData = new CarnivoroData(carnivoro);
                carnivorosData[i] = carnivoroData;
            }
        }

        return carnivorosData;
    }

    private HerbivoroData[] GetHerbivorosData()
    {
        GameObject[] herbivoros = GameObject.FindGameObjectsWithTag("Presa");
        HerbivoroData[] herbivorosData = new HerbivoroData[herbivoros.Length];

        for (int i = 0; i < herbivoros.Length; i++)
        {
            GameObject herbivoroGO = herbivoros[i];
            Herbivoro herbivoro = herbivoroGO.GetComponent<Herbivoro>();

            if (herbivoro != null)
            {
                HerbivoroData herbivoroData = new HerbivoroData(herbivoro);
                herbivorosData[i] = herbivoroData;
            }
        }

        return herbivorosData;
    }

    private void InstanciarCarnivoro(CarnivoroData carnivoroData)
    {
        if (Enum.TryParse(carnivoroData.especie, out Especie especie))
        {
            GameObject carnivoroGO = Instantiate(GetPrefabPorEspecie(especie), carnivoroData.position, Quaternion.identity);
            Carnivoro carnivoro = carnivoroGO.GetComponent<Carnivoro>();

            if (carnivoro != null)
            {
                carnivoro.especie = especie;
                carnivoro.vida = carnivoroData.vida;
                carnivoro.velocidade = carnivoroData.velocidade;
                carnivoro.tempoFome = carnivoroData.tempoFome;
                carnivoro.tempoApetite = carnivoroData.tempoApetite;
                carnivoro.intervaloReproducao = carnivoroData.intervaloReproducao;
                carnivoro.tempoReproducao = carnivoroData.tempoReproducao;
                carnivoro.saciedade = carnivoroData.saciedade;

                Especie[] presasEnums = new Especie[carnivoroData.presas.Length];
                for (int i = 0; i < carnivoroData.presas.Length; i++)
                {
                    string especieString = carnivoroData.presas[i];
                    if (Enum.TryParse(especieString, out Especie especiePresa))
                    {
                        presasEnums[i] = especiePresa;
                    }
                }

                carnivoro.presas = presasEnums;
                carnivoro.danoAtaque = carnivoroData.danoAtaque;
                carnivoro.distanciaAtaque = carnivoroData.distanciaAtaque;
                carnivoro.velocidadeAtaque = carnivoroData.velocidadeAtaque;
            }
        }
    }

    private void InstanciarHebivoro(HerbivoroData herbivoroData)
    {

        if (Enum.TryParse(herbivoroData.especie, out Especie especie))
        {
            GameObject herbivoroGO = Instantiate(GetPrefabPorEspecie(especie), herbivoroData.position, Quaternion.identity);
            Herbivoro herbivoro = herbivoroGO.GetComponent<Herbivoro>();

            if (herbivoro != null)
            {
                herbivoro.especie = especie;
                herbivoro.vida = herbivoroData.vida;
                herbivoro.velocidade = herbivoroData.velocidade;
                herbivoro.tempoFome = herbivoroData.tempoFome;
                herbivoro.tempoApetite = herbivoroData.tempoApetite;
                herbivoro.intervaloReproducao = herbivoroData.intervaloReproducao;
                herbivoro.tempoReproducao = herbivoroData.tempoReproducao;
                herbivoro.saciedade = herbivoroData.saciedade;
                herbivoro.distanciaConsumoGrama = herbivoroData.distanciaConsumoGrama;
            }
        }
    }

    private GameObject GetPrefabPorEspecie(Especie especie)
    {
        return prefabEspecies[(int)especie];
    }
}
