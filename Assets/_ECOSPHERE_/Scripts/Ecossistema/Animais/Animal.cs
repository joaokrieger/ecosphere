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
    private float tempoVida;

    [Header("Configurações de Animal")]
    private GameObject emoteMorte;
    private GameObject emoteReproducao;
    private GameObject emoteFome;

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
        tempoReproducao = intervaloReproducao * 2;

        emoteMorte = transform.Find("EmoteMorte").gameObject;
        emoteReproducao = transform.Find("EmoteReproducao").gameObject;
        emoteFome = transform.Find("EmoteFome").gameObject;

        tempoVida = Random.Range(120f, 240f);
    }

    protected void Update()
    {
        VerificaFome();
        CorrigeLocomocao();
        tempoReproducao -= Time.deltaTime;
        tempoVida -= Time.deltaTime;

        if (tempoVida <= 0 && !morreu)
        {
            Morrer();
        }

        if (morreu)
        {
            ExibirEmote(EmoteAnimal.Morte);
        }
        else if(tempoFome <= tempoApetite)
        {
            ExibirEmote(EmoteAnimal.Fome);
        }
        else if (parceiroAcasalamento != null)
        {

            if (!parceiroAcasalamento.GetComponent<Animal>().EstaVivo())
            {
                parceiroAcasalamento = null;
            }

            ExibirEmote(EmoteAnimal.Reproducao);
        }
        else
        {
            ExibirEmote(EmoteAnimal.Padrao);
        }
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
        if (!navMeshAgent.pathPending && !navMeshAgent.hasPath || navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance || navMeshAgent.velocity.sqrMagnitude < 0.1f && navMeshAgent.hasPath)
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
        if (EstaVivo() && (parceiroAcasalamento != null && parceiroAcasalamento.GetComponent<Animal>().EstaVivo()))
        {

            tempoReproducao = intervaloReproducao * 2;

            // Garantir que apenas um dos dois crie o filhote
            if (this.GetInstanceID() < parceiroAcasalamento.GetInstanceID())
            {
                parceiroAcasalamento = null;
                GameObject filhote = Instantiate(this.gameObject, transform.position, Quaternion.identity);
                Animal filhoteAnimal = filhote.GetComponent<Animal>();
                filhoteAnimal.vida = this.vida;
                filhoteAnimal.tempoVida = Random.Range(120f, 240f);
                filhoteAnimal.tempoFome = this.saciedade;
                filhoteAnimal.tempoReproducao = this.tempoReproducao;
                RenderPontoVida(rendaReproducao);
            }
            else
            {
                parceiroAcasalamento = null;
            }
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

        if (tempoFome <= tempoApetite)
        {
            yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }

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
        GameManager.Instance.AdicionaSaldo(pontos);
        Instantiate(pontoVidaPrefab, gameObject.transform.position, Quaternion.identity);
    }

    public void ExibirEmote(EmoteAnimal tipoEmote)
    {
        emoteFome.SetActive(false);
        emoteMorte.SetActive(false);
        emoteReproducao.SetActive(false);

        switch (tipoEmote)
        {
            case EmoteAnimal.Fome:
                emoteFome.SetActive(true);
                break;
            case EmoteAnimal.Morte:
                emoteMorte.SetActive(true);
                break;
            case EmoteAnimal.Reproducao:
                emoteReproducao.SetActive(true);
                break;
        }
    }
}