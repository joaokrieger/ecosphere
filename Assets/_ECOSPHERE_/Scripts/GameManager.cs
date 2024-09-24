using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject[] prefabEspecies;
    public GameObject gramaPrefab;
    public Fase faseAtual;

    private string enderecoArquivoJson;
    private int saldo = 100;

    [System.Serializable]
    public class GameData
    {
        public CarnivoroData[] carnivorosData;
        public HerbivoroData[] herbivorosData;
        public GramaData[] gramasData;
        public EcossistemaData ecossistemaData;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SimuladorEcossistema")
        {
            CarregarJogo();
        }
    }

    private void Start()
    {
        enderecoArquivoJson = Path.Combine(Application.persistentDataPath, "gameData.json");
    }

    public void SalvarJogo()
    {
        CarnivoroData[] carnivorosData = GetCarnivorosData();
        HerbivoroData[] herbivorosData = GetHerbivorosData();
        GramaData[] gramasData = GetGramasData();
        EcossistemaData ecossistemaData = new EcossistemaData();
        GameData gameData = new GameData
        {
            carnivorosData = carnivorosData,
            herbivorosData = herbivorosData,
            gramasData = gramasData,
            ecossistemaData = ecossistemaData
        };

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(enderecoArquivoJson, json);
        Debug.Log($"Jogo salvo em: {enderecoArquivoJson}");
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
                InstanciarHerbivoro(herbivoroData);
            }

            foreach (GramaData gramaData in gameData.gramasData)
            {
                InstanciarGrama(gramaData);
            }

            SetSaldo(gameData.ecossistemaData.pontosVida);
            if (this.faseAtual == null && Enum.TryParse(gameData.ecossistemaData.fase, out Fase fase))
            {
                AtualizarFase(fase);
            }

            Debug.Log("Jogo carregado com sucesso.");
        }
        else
        {
            Debug.LogWarning("Nenhum arquivo de jogo salvo encontrado.");
        }
    }

    public void ExcluirArquivoJson()
    {
        if (File.Exists(enderecoArquivoJson))
        {
            File.Delete(enderecoArquivoJson);
            Debug.Log("Arquivo JSON excluído.");
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

    private GramaData[] GetGramasData()
    {
        GameObject[] gramas = GameObject.FindGameObjectsWithTag("Grama");
        GramaData[] gramasData = new GramaData[gramas.Length];

        for (int i = 0; i < gramas.Length; i++)
        {
            GameObject gramaGO = gramas[i];
  
            if (gramaGO != null)
            {
                GramaData gramaData = new GramaData(gramaGO);
                gramasData[i] = gramaData;
            }
        }

        return gramasData;
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

    private void InstanciarHerbivoro(HerbivoroData herbivoroData)
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

    private void InstanciarGrama(GramaData gramaData)
    {
        GameObject gramaGO = Instantiate(gramaPrefab, gramaData.position, Quaternion.identity);
    }

    private GameObject GetPrefabPorEspecie(Especie especie)
    {
        return prefabEspecies[(int)especie];
    }

    public void AdicionaSaldo(int valor)
    {
        saldo += valor;
    }

    public bool RemoveSaldo(int valor)
    {
        if ((saldo - valor) >= 0)
        {
            saldo -= valor;
            return true;
        }

        return false;
    }

    public int GetSaldo()
    {
        return this.saldo;
    }

    private void SetSaldo(int saldo) {
        this.saldo = saldo;
    }

    public void AtualizarFase(Fase novaFase)
    {
        if (novaFase != null)
        {
            faseAtual = novaFase;
        }
    }

    public Fase GetNextFase()
    {
        if (faseAtual == Fase.Fase01)
        {
            return Fase.Fase02;
        }

        if (faseAtual == Fase.Fase02)
        {
            return Fase.Fase03;
        }

        if (faseAtual == Fase.Fase03)
        {
            return Fase.Fase04;
        }

        if (faseAtual == Fase.Fase04)
        {
            return Fase.Fase05;
        }

        return faseAtual;
    }
}
