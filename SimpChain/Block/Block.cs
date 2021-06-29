using System;
using MerkleTools;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SimpChain
{
    public class Block
    {
        public string blockHash { get; set; }
        public int index {get;set;}
        public List<string> transactions {get;set;}
        public DateTime timestamp {get;set;}
        public string previousHash {get;set;}
        public int nonce {get;set;}

        private MerkleTree tree = new MerkleTree();

        public Block() 
        {
            this.index = 0;
            this.transactions = new List<string> { "buy", "sell" };
            this.timestamp = DateTime.Today;
            this.previousHash = "0";
            this.nonce = 0;
        }

        public string computeHash()
        {
            List<byte[]> transHashes = new List<byte[]>();
            byte[] inx = GetHash(index.ToString());
            foreach(var i in transactions) 
            {
                var val = GetHash(i);
                transHashes.Add(val);
            }
            byte[] ts = GetHash(timestamp.ToString());
            byte[] pvs = Encoding.UTF8.GetBytes(previousHash);
            byte[] nnce = GetHash(nonce.ToString());

            //Process Transaction tree hash
            tree.AddLeave(transHashes);
            byte[] trans = tree.MerkleRootHash;
            var finalHash = inx.Concat(trans).Concat(ts).Concat(pvs).Concat(nnce).ToArray();
            finalHash = GetHash(finalHash);

            string finalstr = HexEncoder.Encode(finalHash);
            tree = new MerkleTree();
            this.blockHash = finalstr;
            return finalstr;
            




        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static byte[] GetHash(byte[] inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(inputString);
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }

    
}