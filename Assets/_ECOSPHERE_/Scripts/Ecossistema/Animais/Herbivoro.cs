using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herbivoro : Animal
{
    [Header("Configurações da Presa")]
    public float distanciaConsumoGrama;

    public GameObject gramaAlvo;
    private bool consumindoGrama = false;
    private Animator animator;

    protected virtual void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        gramaAlvo = null;
    }

    protected virtual void Update()
    {
        base.Update();
        VerificarMovimento();

        if (parceiroAcasalamento != null)
        {
            LocomoverPara(parceiroAcasalamento.transform.position);
            if (Vector2.Distance(transform.position, parceiroAcasalamento.transform.position) < 2f)
            {
                Reproduzir();
            }
        }
        else
        {
            if (tempoFome <= tempoApetite) 
            {
                if (gramaAlvo == null)
                {
                    gramaAlvo = BuscarGramaMaisProxima();
                }

                if (gramaAlvo != null)
                {
                    Vector3 posicaoGrama = gramaAlvo.transform.position;
                    float distanciaParaGrama = Vector2.Distance(transform.position, posicaoGrama);
                    LocomoverPara(posicaoGrama);

                    if (distanciaParaGrama <= distanciaConsumoGrama)
                    {
                        Comer();
                    }
                }
            }
            else if (!esperandoRonda)
            {
                RealizarRonda();
            }
        }
    }


    private void VerificarMovimento()
    {
        if (EstaVivo())
        {
            if (navMeshAgent.velocity.magnitude > 0.5f)
            {
                if (!animator.GetBool("estaAndando"))
                {
                    animator.SetBool("estaAndando", true);
                }
            }
            else
            {
                if (animator.GetBool("estaAndando"))
                {
                    animator.SetBool("estaAndando", false);
                }
            }
        }
        else
        {
            if (animator.GetBool("estaAndando"))
            {
                animator.SetBool("estaAndando", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        Animal animal = colider.GetComponent<Animal>();
        if (animal != null)
        {
            if (animal.especie == this.especie && parceiroAcasalamento == null && animal.EstaVivo() && (tempoReproducao <= intervaloReproducao && animal.tempoReproducao <= animal.intervaloReproducao))
            {
                parceiroAcasalamento = colider.gameObject;
                animal.parceiroAcasalamento = gameObject;
                LocomoverPara(colider.transform.position);
            }
        }
    }

    private GameObject BuscarGramaMaisProxima()
    {
        float raioBusca = 10f; 
        Collider2D[] colisoes = Physics2D.OverlapCircleAll(transform.position, raioBusca);

        GameObject gramaMaisProxima = null;
        float menorDistancia = Mathf.Infinity;

        foreach (Collider2D colisao in colisoes)
        {
            if (colisao.CompareTag("Grama"))
            {
                float distancia = Vector2.Distance(transform.position, colisao.transform.position);
                if (distancia < menorDistancia)
                {
                    menorDistancia = distancia;
                    gramaMaisProxima = colisao.gameObject;
                }
            }
        }

        return gramaMaisProxima;
    }


    protected override void Comer()
    {
        if (gramaAlvo != null)
        {
            Destroy(gramaAlvo);
            gramaAlvo = null;
            navMeshAgent.ResetPath();
            this.tempoFome = this.saciedade;
            base.Comer();
        }
    }

    protected override void Morrer()
    {
        base.Morrer();
        animator.SetTrigger("morreu");
    }
}
