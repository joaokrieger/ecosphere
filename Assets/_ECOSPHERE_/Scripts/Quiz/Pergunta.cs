[System.Serializable]
public class Pergunta
{
    public string questao;
    public string[] respostas;
    public int repostaCorreta;

    public Pergunta(string questao, string[] respostas, int repostaCorreta)
    {
        this.questao = questao;
        this.respostas = respostas;
        this.repostaCorreta = repostaCorreta;
    }
}
