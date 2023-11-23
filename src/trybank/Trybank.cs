namespace Trybank.Lib;

public class TrybankLib
{
    public bool Logged;
    public int loggedUser;

    //0 -> Número da conta
    //1 -> Agência
    //2 -> Senha
    //3 -> Saldo
    public int[,] Bank;
    public int registeredAccounts;
    private int maxAccounts = 50;

    public TrybankLib()
    {
        loggedUser = -99;
        registeredAccounts = 0;
        Logged = false;
        Bank = new int[maxAccounts, 4];
    }

    // 1. Construa a funcionalidade de cadastrar novas contas
    public void RegisterAccount(int number, int agency, int pass)
    {
        for (int i = 0; i < Bank.GetLength(0); i++)
        {
            if (number == Bank[i, 0] && agency == Bank[i, 1])
            {
                throw new ArgumentException("A conta já está sendo usada!");
            }
        }
        Bank[registeredAccounts, 0] = number;
        Bank[registeredAccounts, 1] = agency;
        Bank[registeredAccounts, 2] = pass;
        Bank[registeredAccounts, 3] = 0;
        registeredAccounts++;
    }

    // 2. Construa a funcionalidade de fazer Logins
    public void Login(int number, int agency, int pass)
    {
        if (Logged == true)
        {
            throw new AccessViolationException("Usuário já está logado");
        }
        else
        {
            for (int i = 0; i < registeredAccounts; i++)
            {
                if (number == Bank[i, 0] && agency == Bank[i, 1])
                {
                    if (pass == Bank[i, 2])
                    {
                        Logged = true;
                        loggedUser = i;
                    }
                    else
                    {
                        throw new ArgumentException("Senha incorreta");
                    }
                }
                else
                {
                    throw new ArgumentException("Agência + Conta não encontrada");
                }
            }
        }
    }

    // 3. Construa a funcionalidade de fazer Logout
    public void Logout()
    {
        if (Logged == false)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        else
        {
            Logged = false;
            loggedUser = -99;
        }
    }

    // 4. Construa a funcionalidade de checar o saldo
    public int CheckBalance()
    {
        if (Logged == false)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        return Bank[loggedUser, 3];
    }

    // 5. Construa a funcionalidade de depositar dinheiro
    public void Deposit(int value)
    {
        if (Logged == false)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        Bank[loggedUser, 3] = value;
    }

    // 6. Construa a funcionalidade de sacar dinheiro
    public void Withdraw(int value)
    {
        if (Logged == false)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        if (Bank[loggedUser, 3] < value)
        {
            throw new InvalidOperationException("Saldo insuficiente");
        }
        int saque = Bank[loggedUser, 3] - value;
        Bank[loggedUser, 3] = saque;
    }

    // 7. Construa a funcionalidade de transferir dinheiro entre contas
    public void Transfer(int destinationNumber, int destinationAgency, int value)
    {
        if (Logged == false)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        if (Bank[loggedUser, 3] < value)
        {
            throw new InvalidOperationException("Saldo insuficiente");
        }
        for (int i = 0; i < registeredAccounts; i++)
        {
            if (destinationNumber == Bank[i, 0] && destinationAgency == Bank[i, 1])
            {
                Bank[i, 3] = Bank[i, 3] + value;
                Bank[loggedUser, 3] = Bank[loggedUser, 3] - value;
            }
        }
    }


}
