using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Resposta : MonoBehaviour
{
    public bool estaCorreta = false;
    public QuizManager quizManager;
    private Image imagem;

    private void Start()
    {
        imagem = GetComponent<Image>();
    }

    public void RespostaQuestao()
    {
        if (estaCorreta)
        {
            imagem.color = Color.green;
            StartCoroutine(quizManager.RespostaCorreta());
        }
        else
        {
            imagem.color = Color.red;
            quizManager.RespostaErrada();
        }
    }

    public void Resetar()
    {
        imagem.color = new Color(196f, 194f, 194f);
    }
}
