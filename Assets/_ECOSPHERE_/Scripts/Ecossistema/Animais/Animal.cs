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
    public int rendaAlimentacao;
    public int rendaReproducao;
    public GameObject pontoVidaPrefab;

    [Header("Configurações de Animal")]
    public Especie especie;
    public int vida;
    public float velocidade;
    public float tempoFome;
    public float tempoApetite;
    public float intervaloReproducao;
    public float tempoReproducao;
    public GameObject parceiroAcasalamento;

    public float saciedade;
    protected bool morreu = false;
    protected bool esperandoRonda = false;
   
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
            StartCoroutine(PausarAntesDaProximaRonda());
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
            //filhote.transform.localScale = transform.localScale * 0.75f;

            Animal filhoteAnimal = filhote.GetComponent<Animal>();
            filhoteAnimal.vida = this.vida / 2;
            filhoteAnimal.tempoFome = this.saciedade;
            tempoReproducao = intervaloReproducao * 2;
            filhoteAnimal.tempoReproducao = this.tempoReproducao;

            RenderPontoVida(rendaReproducao);
        }
    }

    protected virtual void Comer() {
        RenderPontoVida(rendaAlimentacao);
    }

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

    private IEnumerator PausarAntesDaProximaRonda()
    {
        esperandoRonda = true;
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        this.LocomoverDestinoAleatorio();
        esperandoRonda = false;
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

    public void RenderPontoVida(int pontos)
    {
        GameController.GetInstance().GetCarteiraPontoVida().AdicionaSaldo(pontos);
        Instantiate(pontoVidaPrefab, gameObject.transform.position, Quaternion.identity);
    }

}