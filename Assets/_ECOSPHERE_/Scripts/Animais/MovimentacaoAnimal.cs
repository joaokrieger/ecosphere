using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class MovimentacaoAnimal : MonoBehaviour
{
    [Header("Configura��es de Movimento")]
    public Vector2 areaMin;
    public Vector2 areaMax;
    public float tempoRonda = 5f;
    public float tempoParado = 2f;

    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Tilemap tilemapCampo;
    private Tilemap tilemapEstrutura;
    private bool estaRondando = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        GameObject campoObject = GameObject.FindWithTag("Campo");
        tilemapCampo = campoObject.GetComponent<Tilemap>();

        GameObject estruturaObject = GameObject.FindWithTag("Estruturas");
        tilemapEstrutura = estruturaObject.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!agent.isStopped) {

            if (!estaRondando)
            {
                estaRondando = true;
                StartCoroutine(RealizaRonda());
            }

            CorrigeLocomocao();
        }
    }

    private void CorrigeLocomocao()
    {
        Vector3 velocidade = agent.velocity;

        if (velocidade.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (velocidade.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (velocidade.magnitude > 0.1f)
        {
            animator.SetBool("estaAndando", true);
        }
        else
        {
            animator.SetBool("estaAndando", false);
        }
    }

    private IEnumerator RealizaRonda()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoRonda);

            float randomX = Random.Range(areaMin.x, areaMax.x);
            float randomY = Random.Range(areaMin.y, areaMax.y);
            Vector3 posicaoAleatoria = new Vector3(randomX, randomY, 0);

            Vector3Int posicaoCelula = tilemapCampo.WorldToCell(posicaoAleatoria);

            while (!tilemapCampo.HasTile(posicaoCelula) || tilemapEstrutura.HasTile(posicaoCelula))
            {
                randomX = Random.Range(areaMin.x, areaMax.x);
                randomY = Random.Range(areaMin.y, areaMax.y);
                posicaoAleatoria = new Vector3(randomX, randomY, 0);
                posicaoCelula = tilemapCampo.WorldToCell(posicaoAleatoria);
            }

            agent.SetDestination(posicaoAleatoria);

            yield return new WaitForSeconds(tempoParado);
        }
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public Vector3 GetVelocity()
    {
        return agent.velocity;
    }

    public void PararMovimento()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public void RemoverDestino()
    {
        agent.ResetPath();
    }
}