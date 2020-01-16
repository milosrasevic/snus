using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace RealTimeUnit
{
    class Program
    {
        public static CspParameters csp = new CspParameters();
        public static RSACryptoServiceProvider rsa;
        public static string id = "";
        public static ServiceReference1.RealTimeUnitClient proxy = new ServiceReference1.RealTimeUnitClient();
        static void Main(string[] args)
        {
            csp.KeyContainerName = "KeyContainerName";
            csp.Flags = CspProviderFlags.UseMachineKeyStore;
            rsa = new RSACryptoServiceProvider(csp);
            rsa.PersistKeyInCsp = true;

            string publicKey = rsa.ToXmlString(false);

            Console.WriteLine("Unesite ID ovog RTU: ");
            id = Console.ReadLine();
            
            if (proxy.initialize(id, publicKey))
            {
                SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

                Console.WriteLine("Unesite adresu na koju ovaj RTU upisuje: ");
                int address = int.Parse(Console.ReadLine());
                Console.WriteLine("Unesite maksimalnu vrednost koju ovaj RTU moze da ocita: ");
                int maxValue = int.Parse(Console.ReadLine());
                Random rng = new Random();
                double number = 0;
                while (true)
                {
                    
                    number = rng.NextDouble() * maxValue;

                    SHA256 sha = SHA256Managed.Create();
                    byte[] hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(id));
                    RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(rsa);
                    formatter.SetHashAlgorithm("SHA256");
                    byte[] potpis = formatter.CreateSignature(hashValue);

                    if(proxy.writeValue(id, address, number, potpis))
                        Console.WriteLine("Upisao: " + number);
                    Thread.Sleep(2500);
                }
            }
            else
            {
                Console.WriteLine("Vec postoji RTU sa tim ID-em.");
                Console.ReadKey();
            }

            

        }

        // Declare the SetConsoleCtrlHandler function
        // as external and receiving a delegate.
        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        // A delegate type to be used as the handler routine 
        // for SetConsoleCtrlHandler.
        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        // An enumerated type for the control messages
        // sent to the handler routine.
        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            SHA256 sha = SHA256Managed.Create();
            byte[] hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(id));
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(rsa);
            formatter.SetHashAlgorithm("SHA256");
            byte[] potpis = formatter.CreateSignature(hashValue);

            proxy.deinitialize(id, potpis);
            Environment.Exit(0);
            return true;
        }
        
        
    }
}
