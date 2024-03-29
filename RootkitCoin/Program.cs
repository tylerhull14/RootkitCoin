using System.Security.Cryptography;
using System.Text;
using EllipticCurve;
using Newtonsoft.Json;

namespace RootkitCoin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();

            PrivateKey key3 = new PrivateKey();
            PublicKey wallet3 = key3.publicKey();


            Blockchain rootkitCoin = new Blockchain(3, 100);

            rootkitCoin.MinePendingTransactions(wallet1);
            rootkitCoin.MinePendingTransactions(wallet2);
            rootkitCoin.MinePendingTransactions(wallet3);

            Console.Write("\nBalance of Wallet1: $" + rootkitCoin.GetBalanceOfWallet(wallet1).ToString());
            Console.Write("\nBalance of Wallet2: $" + rootkitCoin.GetBalanceOfWallet(wallet2).ToString());
            Console.Write("\nBalance of Wallet3: $" + rootkitCoin.GetBalanceOfWallet(wallet3).ToString());

            Transaction tx1 = new Transaction(wallet1, wallet2, 55.00m);
            tx1.SignTransaction(key1);
            rootkitCoin.addPendingTransaction(tx1);

            Transaction tx2 = new Transaction(wallet3, wallet2, 20.00m);
            tx2.SignTransaction(key3);
            rootkitCoin.addPendingTransaction(tx2);

            rootkitCoin.MinePendingTransactions(wallet3);

            Console.Write("\nBalance of Wallet1: $" + rootkitCoin.GetBalanceOfWallet(wallet1).ToString());
            Console.Write("\nBalance of Wallet2: $" + rootkitCoin.GetBalanceOfWallet(wallet2).ToString());
            Console.Write("\nBalance of Wallet3: $" + rootkitCoin.GetBalanceOfWallet(wallet3).ToString());

            string blockJSON = JsonConvert.SerializeObject(rootkitCoin, Formatting.Indented);
            Console.WriteLine(blockJSON);

            if (rootkitCoin.IsChainValid())
            { Console.WriteLine("Blockchain is Valid!!!"); }
            else { Console.WriteLine("Blockchain is NOT valid."); }
        }
    }
}
