using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public abstract class Animal : MonoBehaviour
{
    [Header("Configurações do Ecossistema")]
    private Ecossistema ecossistema;

    [Header("Configurações da Economia")]
    public int precoSpawn;

    [Header("Configurações de Animal")]
    public Especie especie;
    public int vida;
    public float velocidade;
    public float tempoFome;
    public float tempoApetite;
    public float intervaloReproducao;
    public float tempoReproducao;
    public GameObject parceiroAcasalamento;

    protected float saciedade;
    protected bool morreu = false;
   
    protected NavMeshAgent navMeshAgent;

    protected void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = velocidade;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        ecossistema = FindObjectOfType<Ecossistema>();
        saciedade = tempoFome;
        tempoReproducao = intervaloReproducao * 2;
    }

    protected void Update()
    {
        VerificaFome();
        CorrigeLocomocao();
        tempoReproducao -= Time.deltaTime;
    }

    protected virtual void Morrer()
    {
        StartCoroutine(ecossistema.Decompor(this.gameObject));
        morreu = true;
        navMeshAgent.ResetPath();
        navMeshAgent.isStopped = true;
    }

    public virtual void RealizarRonda()
    {
        if (!navMeshAgent.pathPending && !navMeshAgent.hasPath || navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            this.LocomoverDestinoAleatorio();
        }
    }

    public virtual void LocomoverDestinoAleatorio()
    {
        Tilemap tilemapCampo = GameObject.FindWithTag("Campo").GetComponent<Tilemap>();
        BoundsInt bounds = tilemapCampo.cellBounds;

        Vector3 posicaoAleatoria;
        Vector3Int posicaoCelula;

        do
        {
            float randomX = Random.Range(bounds.xMin, bounds.xMax);
            float randomY = Random.Range(bounds.yMin, bounds.yMax);
            posicaoAleatoria = new Vector3(randomX, randomY, 0);
            posicaoCelula = tilemapCampo.WorldToCell(posicaoAleatoria);
        } while (!tilemapCampo.HasTile(posicaoCelula));

        this.LocomoverPara(posicaoAleatoria);
    }

    protected virtual void LocomoverPara(Vector3 destination)
    {
        if (!morreu)
        {
            navMeshAgent.ResetPath();
            navMeshAgent.SetDestination(destination);
        }
    }

    protected void CorrigeLocomocao()
    {
        Vector3 velocidade = navMeshAgent.velocity;

        if (velocidade.x < 0.5f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (velocidade.x > 0.5f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void Reproduzir()
    {
        if (EstaVivo() && parceiroAcasalamento != null)
        {
            parceiroAcasalamento = null;
            GameObject filhote = Instantiate(this.gameObject, transform.position, Quaternion.identity);
            filhote.transform.localScale = transform.localScale * 0.75f;

            Animal filhoteAnimal = filhote.GetComponent<Animal>();
            filhoteAnimal.vida = this.vida / 2;
            filhoteAnimal.tempoFome = this.saciedade;
            tempoReproducao = intervaloReproducao * 2;
            filhoteAnimal.tempoReproducao = this.tempoReproducao;
        }
    }

    protected abstract void Comer();

    protected void VerificaFome()
    {
        if (!morreu)
        {
            tempoFome -= Time.deltaTime;
            if (tempoFome <= 0)
            {
                this.Morrer();
            }
        }
    }

    public void ReceberDano(int quantidade)
    {
        if (!morreu)
        {
            vida -= quantidade;
            if (vida <= 0)
            {
                this.Morrer();
            }
        }
    }

    public bool EstaVivo()
    {
        return !morreu;
    }

    public int GetPrecoSpawn()
    {
        return precoSpawn;
    }
}