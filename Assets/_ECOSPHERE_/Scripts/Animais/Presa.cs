using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presa : MonoBehaviour
{
    [Header("Configurações de Consumo")]
    public float distanciaConsumo = 1f;
    public float tempoConsumo = 2f;

    private Transform gramaAlvo;
    private MovimentacaoAnimal movimentacaoAnimal;
    private Animator animator;
    private bool consumindoGrama;

    // Start is called before the first frame update
    void Start()
    {
        movimentacaoAnimal = GetComponent<MovimentacaoAnimal>();
        animator = GetComponent<Animator>();
        consumindoGrama = false;
    }

    // Update is called once per frame
    void Update()
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
}
