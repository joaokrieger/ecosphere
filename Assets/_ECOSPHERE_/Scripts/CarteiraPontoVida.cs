public class CarteiraPontoVida
{
    private int saldo = 6;

    public void AdicionaSaldo(int valor)
    {
        saldo += valor;
    }

    public bool RemoveSaldo(int valor)
    {
        if ((saldo - valor) >= 0)
        {
            saldo -= valor;
            return true;
        }

        return false;
    }

    public int GetSaldo()
    {
        return this.saldo;
    }
}
