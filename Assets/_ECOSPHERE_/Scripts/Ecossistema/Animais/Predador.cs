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
    private bool atacandoPresa;

    protected override void Start()
    {
        base.Start();
        atacandoPresa = false;
    }

    protected override void Update()
    {
        if (!morreu)
        {
            base.Update();
            if (presaAlvo != null && !atacandoPresa && estaComFome)
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
        else
        {
            return;
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
                presaAlvo = null;
                tempoDesdeUltimoConsumo = 0f;  // Reseta o tempo de fome
                RenderPontoVida(valorGanhoAlimentacao);
            }
        }

        yield return new WaitForSeconds(tempoAtaque);
        atacandoPresa = false;
        animator.SetBool("estaAtacando", false);
    }

    public override void Morrer()
    {
        base.Morrer();
        GameController.GetInstance().Remove(GameController.Entidade.Predador);
    }
}
