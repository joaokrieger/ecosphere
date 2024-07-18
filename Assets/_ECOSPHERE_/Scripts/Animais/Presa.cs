using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presa : MonoBehaviour
{

    public float velocidade = 5f;
    public float distanciaConsumo = 1f;
    private Transform gramaAlvo;

    // Start is called before the first frame update
    void Start()
    {
        BuscaGramaProxima();        
    }

    // Update is called once per frame
    void Update()
    {
        if (gramaAlvo == null)
        {
            BuscaGramaProxima();
        }
        else
        {
            LocomoveAteGramaProxima();
        }
    }

    void BuscaGramaProxima()
    {
        GameObject[] grassObjects = GameObject.FindGameObjectsWithTag("Grama");
        if (grassObjects.Length > 0)
        {
            int randomIndex = Random.Range(0, grassObjects.Length);
            gramaAlvo = grassObjects[randomIndex].transform;
        }
        else
        {
            gramaAlvo = null;  // Se não houver mais grama, não há alvo
        }
    }

    void LocomoveAteGramaProxima()
    {
        Vector3 direction = (gramaAlvo.position - transform.position).normalized;
        transform.position += direction * velocidade * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Vector3.Distance(transform.position, gramaAlvo.position) <= distanciaConsumo)
        {
            ConsumirGrama();
        }
    }

    void ConsumirGrama()
    {
        Destroy(gramaAlvo.gameObject);  // Destrói o objeto da grama
        BuscaGramaProxima();  // Procura um novo alvo
    }
}
