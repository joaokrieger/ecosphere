using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Presa : MonoBehaviour
{
    public float velocidade = 5f;
    public float distanciaConsumo = 1f;
    public Transform gramaAlvo;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;      
    }

    // Update is called once per frame
    void Update()
    {
        if (gramaAlvo != null)
        {
            agent.SetDestination(gramaAlvo.position);

            // Verifica a distância entre o agente e o alvo
            float distanciaParaGrama = Vector2.Distance(transform.position, gramaAlvo.position);
            if (distanciaParaGrama <= distanciaConsumo)
            {
                ConsumirGrama();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.CompareTag("Grama"))
        {
            gramaAlvo = colider.transform;
        }
    }

    void ConsumirGrama()
    {
        if (gramaAlvo != null)
        {
            Debug.Log("Destruindo grama: " + gramaAlvo.name);
            Destroy(gramaAlvo.gameObject);
            gramaAlvo = null;
        }
    }
}

