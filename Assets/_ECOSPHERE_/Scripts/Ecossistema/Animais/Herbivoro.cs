using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herbivoro : Animal
{
    [Header("Configurações da Presa")]
    public float distanciaConsumoGrama;

    private GameObject gramaAlvo;
    private bool consumindoGrama = false;
    private Animator animator;

    protected virtual void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        GameController.GetInstance().Add(GameController.Entidade.Herbivoro);
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

            if (gramaAlvo != null && tempoFome <= tempoApetite)
            {
                Vector3 posicaoGrama = gramaAlvo.transform.position;
                float distanciaParaGrama = Vector2.Distance(transform.position, posicaoGrama);
                LocomoverPara(posicaoGrama);

                if (distanciaParaGrama <= distanciaConsumoGrama)
                {
                    Comer();
                }
            }
            else
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
            if (animal.especie == this.especie && parceiroAcasalamento == null && (tempoReproducao <= intervaloReproducao && animal.tempoReproducao <= animal.intervaloReproducao))
            {
                parceiroAcasalamento = colider.gameObject;
                animal.parceiroAcasalamento = gameObject;
                LocomoverPara(colider.transform.position);
            }
        }
        else if (colider.CompareTag("Grama") && gramaAlvo == null)
        {
            gramaAlvo = colider.gameObject;
        }
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
        GameController.GetInstance().Remove(GameController.Entidade.Herbivoro);
    }
}
