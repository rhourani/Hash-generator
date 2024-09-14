using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        //Generate file 
        string filePath = "C:\\Users\\Admin\\source\\repos\\Hash\\simple.txt";
        string text = "Cryptography is the building block of Security Protocols";

        // Write the text to the file
        File.WriteAllText(filePath, text);

        Console.WriteLine($"File '{filePath}' has been created with the following content:\n{text}");

        //Generate hash
        string hash = ComputeSHA256(filePath);
        Console.WriteLine($"SHA-256 Digest for {filePath}: {hash}");

        //Check integrity
        string receivedFilePath = "C:\\Users\\Admin\\source\\repos\\Hash\\input.txt";// received file 
        string providedHash = "8d509e555734face2589ea2567435f84e85b2691837a34226a8229d8baec6617";  // Received hash in base64 jVCeVVc0+s4lieolZ0NfhOhbJpGDejQiaoIp2LrsZhc=

        VerifyFileIntegrity(receivedFilePath, providedHash);

    }
    public static string ComputeSHA256(string filePath)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                byte[] hashValue = sha256.ComputeHash(fileStream);
                return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            }
        }
    }

    public static void VerifyFileIntegrity(string filePath, string providedHash)
    {
        string computedHash = ComputeSHA256(filePath);
        if (computedHash == providedHash)
        {
            Console.WriteLine("The file is intact.");
        }
        else
        {
            Console.WriteLine("The file has been modified!");
        }
    }
}