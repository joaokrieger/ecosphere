using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predador : MonoBehaviour
{
    public float velocidade;
    public float areaVisao;
    public bool estaComFome;
    public float distanciaMinimaPerseguir = 5f;  // Distância mínima para começar a perseguir a presa
    private float distanciaPresa;

    private List<GameObject> presasProximas = new List<GameObject>();  // Lista de presas próximas

    void OnTriggerEnter2D(Collider2D entidade)
    {
        // Adicionar presas à lista quando entrarem no collider de área
        if (entidade.CompareTag("Presa"))
        {
            presasProximas.Add(entidade.gameObject);
        }

        Debug.Log("Entrou");
    }

    void OnTriggerExit2D(Collider2D entidade)
    {
        // Remover presas da lista quando saírem do collider de área
        if (entidade.CompareTag("Presa"))
        {
            presasProximas.Remove(entidade.gameObject);
        }

        Debug.Log("Saiu");
    }

    void Update()
    {
        if (estaComFome)
        {
            PerseguirPresaProxima();
        }
    }

    void PerseguirPresaProxima()
    {
        GameObject presaMaisProxima = null;
        float distanciaMinima = Mathf.Infinity;

        // Verificar presas apenas dentro da lista de presas próximas
        foreach (GameObject presa in presasProximas)
        {
            float distanciaAtePresa = Vector3.Distance(transform.position, presa.transform.position);
            if (distanciaAtePresa < distanciaMinima && distanciaAtePresa > distanciaMinimaPerseguir)
            {
                distanciaMinima = distanciaAtePresa;
                presaMaisProxima = presa;
            }
        }

        if (presaMaisProxima != null)
        {
            Debug.Log("Achou presa");

            distanciaPresa = distanciaMinima;
            Vector2 direction = presaMaisProxima.transform.position - transform.position;
            direction.Normalize();
            float visaoPredador = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (distanciaPresa < areaVisao)
            {
                transform.position = Vector2.MoveTowards(transform.position, presaMaisProxima.transform.position, velocidade * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * visaoPredador);
            }
        }
    }
}
