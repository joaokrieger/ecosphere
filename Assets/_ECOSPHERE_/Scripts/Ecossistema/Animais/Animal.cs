using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour, IDano
{
    [Header("Configurações de Animal")]
    public int vida;
    public float tempoFome;
    public float tempoInanicao;
    public float distanciaConsumo;
    public float tempoConsumo;
    protected float tempoDesdeUltimoConsumo;
    protected MovimentacaoAnimal movimentacaoAnimal;
    protected Animator animator;
    protected bool morreu;
    protected bool estaComFome;

    [Header("Configurações de Economia")]
    public int precoSpawn;
    public int valorGanhoAlimentacao;
    public int valorGanhoReproducao;

    protected virtual void Start()
    {
        movimentacaoAnimal = GetComponent<MovimentacaoAnimal>();
        animator = GetComponent<Animator>();
        morreu = false;
        estaComFome = false;
        tempoDesdeUltimoConsumo = 0f;
    }

    protected virtual void Update()
    {
        if (!morreu)
        {
            tempoDesdeUltimoConsumo += Time.deltaTime;  // Incrementa o tempo desde a última refeição
            estaComFome = tempoDesdeUltimoConsumo >= tempoFome;

            if (estaComFome && tempoDesdeUltimoConsumo >= tempoInanicao)
            {
                this.Morrer();
            }
        }
        else
        {
            return;
        }
    }

    // Método para receber dano, comum a todos os animais
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

    public virtual void Morrer()
    {
        morreu = true;
        animator.SetTrigger("morreu");
        movimentacaoAnimal.RemoverDestino();
        movimentacaoAnimal.PararMovimento();
    }

    public bool EstaVivo()
    {
        return !morreu;
    }

    public int GetPrecoSpawn()
    {
        return precoSpawn; 
    }

    public void RenderPontoVida(int pontoVida)
    {
        GameController.GetInstance().GetCarteiraPontoVida().AdicionaSaldo(pontoVida);
    }
}
