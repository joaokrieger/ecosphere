using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Predador : Animal
{
    [Header("Configurações do Predador")]
    public int danoAtaque;
    public float tempoAtaque = 2f;

    private Transform presaAlvo;
    private bool consumindoPresa;
    private bool atacandoPresa;

    protected override void Start()
    {
        base.Start();
        consumindoPresa = false;
        atacandoPresa = false;
    }

    protected override void Update()
    {
        base.Update();
        if (presaAlvo != null && !consumindoPresa && !atacandoPresa && estaComFome)
        {
            float distanciaParaPresa = Vector2.Distance(transform.position, presaAlvo.position);
            if (distanciaParaPresa <= distanciaConsumo && !atacandoPresa)
            {
                StartCoroutine(AtacarPresa());
            }
            else
            {
                movimentacaoAnimal.SetDestination(presaAlvo.position);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.CompareTag("Presa") && presaAlvo == null)
        {
            Presa presa = colider.transform.GetComponent<Presa>();
            if (presa.EstaVivo())
            {
                presaAlvo = colider.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D colider)
    {
        if (colider.CompareTag("Presa") && presaAlvo == colider.transform)
        {
            presaAlvo = null;
            movimentacaoAnimal.RemoverDestino();
        }
    }

    private IEnumerator AtacarPresa()
    {
        atacandoPresa = true;
        Presa presa = presaAlvo.GetComponent<Presa>();
        if (presa != null)
        {
            animator.SetBool("estaAtacando", true);
            presa.ReceberDano(danoAtaque);

            if (!presa.EstaVivo())
            {
                StartCoroutine(ConsumirPresa());
            }
        }

        yield return new WaitForSeconds(tempoAtaque);
        atacandoPresa = false;
        animator.SetBool("estaAtacando", false);
    }

    private IEnumerator ConsumirPresa()
    {
        if (presaAlvo != null)
        {
            consumindoPresa = true;
            animator.SetBool("estaAndando", false);
            animator.SetBool("estaComendo", true);
            yield return new WaitForSeconds(tempoConsumo);

            presaAlvo = null;
            animator.SetBool("estaComendo", false);
            consumindoPresa = false;
            tempoDesdeUltimoConsumo = 0f;  // Reseta o tempo de fome
        }
    }

    public override void Morrer()
    {
        base.Morrer();
    }
}
