using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class Presa : MonoBehaviour
{
    public float distanciaConsumo = 1f;
    public float tempoConsumo = 2f;

    private Transform gramaAlvo;
    private NavMeshAgent agent;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator _animator;
    private bool consumindoGrama;

    public Tilemap tilemapCampo;
    public Vector2 areaMin;
    public Vector2 areaMax;

    private bool estaRondando = false;
    public float tempoRonda;
    public float tempoParado;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        consumindoGrama = false;

        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gramaAlvo != null && !consumindoGrama)
        {
            agent.SetDestination(gramaAlvo.position);

            // Verifica a distância entre o agente e o alvo
            float distanciaParaGrama = Vector2.Distance(transform.position, gramaAlvo.position);
            if (distanciaParaGrama <= distanciaConsumo)
            {
                StartCoroutine(ConsumirGrama());
            }
        }
        else
        {
            if (!estaRondando)
            {
                estaRondando = true;
                StartCoroutine(RealizaRonda());
            }
        }

        CorrigeLocomocao();
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
            _animator.SetBool("estaAndando", true);
        }
        else
        {
            _animator.SetBool("estaAndando", false);
        }
    }

    IEnumerator RealizaRonda()
    {
        while (true)
        {
            // Espera pelo tempo definido em tempoRonda
            yield return new WaitForSeconds(tempoRonda);

            float randomX = Random.Range(areaMin.x, areaMax.x);
            float randomY = Random.Range(areaMin.y, areaMax.y);
            Vector3 posicaoAleatoria = new Vector3(randomX, randomY, 0);

            Vector3Int posicaoCelula = tilemapCampo.WorldToCell(posicaoAleatoria);

            // Garante que a posição aleatória seja válida
            while (!tilemapCampo.HasTile(posicaoCelula))
            {
                randomX = Random.Range(areaMin.x, areaMax.x);
                randomY = Random.Range(areaMin.y, areaMax.y);
                posicaoAleatoria = new Vector3(randomX, randomY, 0);
                posicaoCelula = tilemapCampo.WorldToCell(posicaoAleatoria);
            }
            agent.SetDestination(posicaoAleatoria);

            // Espera pelo tempo definido em tempoParado
            yield return new WaitForSeconds(tempoParado);
        }
    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.CompareTag("Grama") && gramaAlvo == null)
        {
            gramaAlvo = colider.transform;
        }
    }

    private IEnumerator ConsumirGrama()
    {
        if (gramaAlvo != null)
        {
            consumindoGrama = true;
            _animator.SetBool("estaAndando", false);
            _animator.SetBool("estaPastando", true);
            yield return new WaitForSeconds(tempoConsumo);
            Destroy(gramaAlvo.gameObject);
            gramaAlvo = null;
            _animator.SetBool("estaPastando", false);
            consumindoGrama = false;
        }
    }
}

