using System.Security.Cryptography;
using System.Text;

public class Program
{
    public static void Main()
    {
        DigitalSignature digitalSignature = new DigitalSignature();
        SocketClient socketClient = new SocketClient();

        Console.WriteLine("Enter message: ");

        string message = Console.ReadLine();

        Console.WriteLine("Message: " + message);

        byte[] signature = digitalSignature.SignMessage(message);
        RSAParameters publicKey = digitalSignature.GetPublicKey();

        // Serialize public key to XML
        string publicKeyXml;
        using (RSA rsa = RSA.Create())
        {
            rsa.ImportParameters(publicKey);
            publicKeyXml = rsa.ExportPublicKeyToXml();
        }

        socketClient.SendData("127.0.0.1", 9000, Encoding.UTF8.GetBytes(publicKeyXml));
        socketClient.SendData("127.0.0.1", 9000, Encoding.UTF8.GetBytes(message));
        socketClient.SendData("127.0.0.1", 9000, signature);
    }
}

