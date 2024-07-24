using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presa : MonoBehaviour, IDano
{
    [Header("Configurações de Consumo")]
    public float distanciaConsumo = 1f;
    public float tempoConsumo = 2f;
    public int vida;

    private Transform gramaAlvo;
    private MovimentacaoAnimal movimentacaoAnimal;
    private Animator animator;
    private bool consumindoGrama;
    private bool morreu;

    // Start is called before the first frame update
    void Start()
    {
        movimentacaoAnimal = GetComponent<MovimentacaoAnimal>();
        animator = GetComponent<Animator>();
        consumindoGrama = false;
        morreu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!morreu)
        {

            if (gramaAlvo != null && !consumindoGrama)
            {
                movimentacaoAnimal.SetDestination(gramaAlvo.position);

                float distanciaParaGrama = Vector2.Distance(transform.position, gramaAlvo.position);
                if (distanciaParaGrama <= distanciaConsumo)
                {
                    StartCoroutine(ConsumirGrama());
                }
            }
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
            animator.SetBool("estaAndando", false);
            animator.SetBool("estaPastando", true);

            yield return new WaitForSeconds(tempoConsumo);

            // Verifica se o alvo ainda existe antes de destruir
            if (gramaAlvo != null)
            {
                Destroy(gramaAlvo.gameObject);
            }

            gramaAlvo = null;
            animator.SetBool("estaPastando", false);
            consumindoGrama = false;
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

    public bool estaViva()
    {
        return !morreu;
    }
}
