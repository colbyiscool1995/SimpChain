using System;


namespace SimpChain
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Blockchain chain = new Blockchain();
            Console.WriteLine("Hello World!");
            chain.addTRansaction("words");
            Block blk = new Block();
            string oldvalue = blk.computeHash();
            string value = chain.proofOfWork(blk);
            chain.addBlock(blk);
            Console.WriteLine(chain.isValidProof(blk, value));

            Console.WriteLine(value);
            Console.ReadLine();
        }
    }
}
