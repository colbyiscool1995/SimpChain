using System;
using System.Collections.Generic;
using System.Text;

namespace SimpChain
{
    public class Blockchain
    {
        List<Block> chain { get; set; }
        List<string> unconfirmed_transactions { get; set; }

        public int diff { get; set; }

        public Blockchain() 
        {
            chain = new List<Block>();
            unconfirmed_transactions = new List<string>();
        }

        public string addTRansaction(string transaction) 
        {
            unconfirmed_transactions.Add(transaction);
            return "Transaction Added";
        }

        public string addBlock(Block blk) 
        {
            chain.Add(blk);
            return null;
        }

        public string proofOfWork(Block blk) 
        {
            
            string diffs = "0".PadLeft(diff, '0');
            while (!blk.blockHash.StartsWith("0".PadLeft(diff, '0'))) 
            {
                blk.nonce += 1;
                blk.blockHash = blk.computeHash();
            }
            return blk.blockHash;
        }

        public bool isValidProof(Block blk, string hash) 
        {
            return (blk.blockHash.StartsWith("0".PadLeft(diff, '0')) && (blk.computeHash() == hash));
        }
    }
}
