using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ScadaCore
{
    public class RealTimeDriver
    {
        public Dictionary<int, double> communicationChannel { get; set; }
        public static Dictionary<string, string> publicKeys { get; set; }
        public int numOfReservedAddresses {get; set;}
        public RealTimeDriver(int _numOfReservedAddresses)
        {
            this.numOfReservedAddresses = _numOfReservedAddresses;
            this.communicationChannel = new Dictionary<int, double>();
            publicKeys = new Dictionary<string, string>();
            for(int i = 0; i < numOfReservedAddresses; i++)
            {
                this.communicationChannel[i] = 0;
            }
        }
        public bool initialize(string id, string publicKey)
        {
            if (!publicKeys.ContainsKey(id))
            {
                publicKeys[id] = publicKey;
                return true;
            }
            return false;
        }

        public void deinitialize(string id, byte[] signature)
        {
            CspParameters csp = new CspParameters();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);
            if (!publicKeys.ContainsKey(id))
                return;

            rsa.FromXmlString(publicKeys[id]);

            rsa.PersistKeyInCsp = true;

            byte[] messageBytes = Encoding.UTF8.GetBytes(id);
            SHA256 sha = SHA256Managed.Create();
            byte[] hashValue = sha.ComputeHash(messageBytes);


            if (verifySignature(hashValue, signature, rsa))
            {
                if (publicKeys.ContainsKey(id))
                    publicKeys.Remove(id);
            }
            
        }

        public bool writeValue(string id, int address, double value, byte[] signature)
        {
            CspParameters csp = new CspParameters();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);
            if (!publicKeys.ContainsKey(id))
                return false;

            rsa.FromXmlString(publicKeys[id]);

            rsa.PersistKeyInCsp = true;

            byte[] messageBytes = Encoding.UTF8.GetBytes(id);
            SHA256 sha = SHA256Managed.Create();
            byte[] hashValue = sha.ComputeHash(messageBytes);
            

            if (verifySignature(hashValue, signature, rsa))
            {
                lock (this.communicationChannel)
                    this.communicationChannel[address] = value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public double readValue(int ioAddress)
        {
            return this.communicationChannel.ContainsKey(ioAddress) ? this.communicationChannel[ioAddress] : 0;
        }

        private bool verifySignature(byte[] hashValue, byte[] signature, RSACryptoServiceProvider rsa)
        {
            RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(rsa);
            deformatter.SetHashAlgorithm("SHA256");
            return deformatter.VerifySignature(hashValue, signature);
        }

        public override string ToString()
        {
            string result = "===================================================\n";
            lock (this.communicationChannel)
            {
                foreach (var address in communicationChannel.Keys)
                {
                    result += address + ": " + communicationChannel[address] + "\n";
                }
            }
            result += "===================================================";
            return result;
        }

    }
}