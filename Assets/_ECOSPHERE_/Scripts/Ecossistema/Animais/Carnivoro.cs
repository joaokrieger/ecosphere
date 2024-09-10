using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carnivoro : Animal
{
    [Header("Configurações do Carnivoro")]
    public Especie[] presas;
    public int danoAtaque;
    public float distanciaAtaque;
    public float velocidadeAtaque;

    public GameObject presaAlvo;
    private bool atacandoPresa = false;
    private Animator animator;

    protected virtual void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        GameController.GetInstance().Add(GameController.Entidade.Carnivoro);
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
            if (presaAlvo != null && tempoFome <= tempoApetite)
            {
                Vector3 posicaoPresa = presaAlvo.transform.position;
                LocomoverPara(posicaoPresa);
                float distanciaParaPresa = Vector2.Distance(transform.position, posicaoPresa);
                if (distanciaParaPresa <= distanciaAtaque && !atacandoPresa)
                {
                    StartCoroutine(AtacarPresa());
                }
            }
            else if(!esperandoRonda)
            {
                RealizarRonda();
            }
        }
    }

    private void VerificarMovimento()
    {
        if (EstaVivo() && !animator.GetBool("estaAtacando"))
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

    private IEnumerator AtacarPresa()
    {
        atacandoPresa = true;
        if (presaAlvo != null)
        {
            Animal presa = presaAlvo.GetComponent<Animal>();
            if (presa.EstaVivo())
            {
                animator.SetBool("estaAtacando", true);
                presa.ReceberDano(danoAtaque);

                if (!presa.EstaVivo())
                {
                    Comer();
                    animator.SetBool("estaAtacando", false);
                    yield return null;
                }

                yield return new WaitForSeconds(velocidadeAtaque);
                animator.SetBool("estaAtacando", false);
            }
            else
            {
                presaAlvo = null;
                navMeshAgent.ResetPath();
            }
        }
        atacandoPresa = false;
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
            }
            else if (System.Array.Exists(presas, especie => especie == animal.especie) && presaAlvo == null)
            {
                if (animal.EstaVivo())
                {
                    presaAlvo = colider.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D colider)
    {
        if (presaAlvo == colider.gameObject)
        {
            presaAlvo = null;
        }
    }

    protected override void Comer()
    {
        base.Comer();
        presaAlvo = null;
        navMeshAgent.ResetPath();
        this.tempoFome = this.saciedade;
    }

    protected override void Morrer()
    {
        base.Morrer();
        animator.SetTrigger("morreu");
        GameController.GetInstance().Remove(GameController.Entidade.Carnivoro);
    }
}