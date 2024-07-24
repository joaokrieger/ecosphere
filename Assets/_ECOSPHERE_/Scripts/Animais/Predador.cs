using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Predador : MonoBehaviour, IDano
{
    public float distanciaConsumo = 1f;
    public float tempoConsumo = 2f;
    public int danoAtaque;
    public int vida;

    private Transform presaAlvo;
    private MovimentacaoAnimal movimentacaoAnimal;
    private Animator animator;
    private bool consumindoPresa;
    private bool morreu;

    // Start is called before the first frame update
    void Start()
    {
        movimentacaoAnimal = GetComponent<MovimentacaoAnimal>();
        animator = GetComponent<Animator>();
        consumindoPresa = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (presaAlvo != null && !consumindoPresa)
        {
            movimentacaoAnimal.SetDestination(presaAlvo.position);

            float distanciaParaPresa = Vector2.Distance(transform.position, presaAlvo.position);
            if (distanciaParaPresa <= distanciaConsumo)
            {
                AtacarPresa();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.CompareTag("Presa") && presaAlvo == null)
        {
            Presa presa = colider.transform.GetComponent<Presa>();
            if (presa.estaViva())
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

    private void AtacarPresa()
    {
        Presa presa = presaAlvo.GetComponent<Presa>();
        if (presa != null)
        {
            animator.SetBool("estaAtacando", true);
            presa.ReceberDano(danoAtaque);
            if (!presa.estaViva()) {
                animator.SetBool("estaAtacando", false);
                StartCoroutine(ConsumirPresa());
            }
        }
    }

    private IEnumerator ConsumirPresa()
    {
        if (presaAlvo != null)
        {
            consumindoPresa = true;
            animator.SetBool("estaAndando", false);
            animator.SetBool("estaComendo", true);
            yield return new WaitForSeconds(tempoConsumo);

            //Matando a presa
            Presa presa = presaAlvo.GetComponent<Presa>();
            presa.Morrer();

            presaAlvo = null;
            animator.SetBool("estaComendo", false);
            consumindoPresa = false;
        }
    }

    public void ReceberDano(int quantidade)
    {
        if (!morreu)
        {
            vida -= quantidade;
            if (vida <= 0)
            {
                Morrer();
            }
        }
    }

    public void Morrer()
    {
        morreu = true;
        animator.SetTrigger("morreu");
        movimentacaoAnimal.PararMovimento();
    }
}
