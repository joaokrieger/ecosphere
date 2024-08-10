using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presa : Animal
{
    [Header("Configurações da Presa")]
    private Transform gramaAlvo;
    private bool consumindoGrama;

    protected override void Start()
    {
        base.Start();
        consumindoGrama = false;
    }

    protected override void Update()
    {
        base.Update();
        if (!morreu && estaComFome)
        {
            if (gramaAlvo != null && !consumindoGrama)
            {
                float distanciaParaGrama = Vector2.Distance(transform.position, gramaAlvo.position);
                if (distanciaParaGrama <= distanciaConsumo)
                {
                    StartCoroutine(ConsumirGrama());
                }
                else
                {
                    movimentacaoAnimal.SetDestination(gramaAlvo.position);
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

            if (gramaAlvo != null)
            {
                Destroy(gramaAlvo.gameObject);
            }

            gramaAlvo = null;
            animator.SetBool("estaPastando", false);
            consumindoGrama = false;
            tempoDesdeUltimoConsumo = 0f;  // Reseta o tempo de fome
        }
    }

    public override void Morrer()
    {
        base.Morrer();
    }
}
