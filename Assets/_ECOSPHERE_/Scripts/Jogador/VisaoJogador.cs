using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisaoJogador : MonoBehaviour
{

    public float velocidadeMovimento;
    public BoxCollider2D limiteMapa;

    private float velocidadeX, velocidadeY;
    private Rigidbody2D rb;
    private float minX, maxX, minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Calcular os limites do mapa a partir do BoxCollider2D
        minX = limiteMapa.bounds.min.x;
        maxX = limiteMapa.bounds.max.x;
        minY = limiteMapa.bounds.min.y;
        maxY = limiteMapa.bounds.max.y;
    }

    // Update is called once per frame
    void Update()
    {
        velocidadeX = Input.GetAxisRaw("Horizontal") * velocidadeMovimento;
        velocidadeY = Input.GetAxisRaw("Vertical") * velocidadeMovimento;
        rb.velocity = new Vector2(velocidadeX, velocidadeY);

        // Verificar e corrigir a posição do jogador para que ele não ultrapasse os limites
        Vector2 novaPosicao = rb.position;
        novaPosicao.x = Mathf.Clamp(novaPosicao.x, minX, maxX);
        novaPosicao.y = Mathf.Clamp(novaPosicao.y, minY, maxY);
        rb.position = novaPosicao;
    }
}
