using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;

namespace KVDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a secret client using the DefaultAzureCredential
            var client = new SecretClient(new Uri("https://vc2021.vault.azure.net/"), new DefaultAzureCredential());
            var secret = client.GetSecret("connstring");
            Console.WriteLine(secret.Value.Value);
        }
    }
}
