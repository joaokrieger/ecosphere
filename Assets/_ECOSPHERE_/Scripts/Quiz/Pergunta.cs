[System.Serializable]
public class Pergunta
{
    public string questao;
    public string[] respostas;
    public int repostaCorreta;
    public string caminhoImagem;

    public Pergunta(string questao, string[] respostas, int repostaCorreta, string caminhoImagem)
    {
        this.questao = questao;
        this.respostas = respostas;
        this.repostaCorreta = repostaCorreta;
        this.caminhoImagem = caminhoImagem;
    }
}
