using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoVida : MonoBehaviour
{
    public Vector3 alvo;
    public float velocidade = 30f;

    void Start()
    {
        float valorX = transform.position.x;
        float valorY = transform.position.y;
        alvo = new Vector3(valorX, valorY + 50f, transform.position.z);

        GameObject somManager = GameObject.FindWithTag("SomManager");
        somManager.GetComponent<SomManager>().TocarSom(SomManager.TipoSom.PontoDeVida);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, alvo) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, alvo, velocidade * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
