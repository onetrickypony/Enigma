using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Enigma
{
    class Program
    {
        static void Main(string[] args)
        {
            String filename = "JokerAndTheThief.mp3";            
            byte[] buff = File.ReadAllBytes(filename);

            Enigma enigma = new Enigma(243, 56, 9);//74, 3, 239);
            byte[] res = new byte[buff.Length];

            res = enigma.Encrypt(buff);
            File.WriteAllBytes("encrypted_" + filename, res);

            enigma.SetRotorsPos(243, 56, 9);//74, 3, 239);
            byte[] nres = new byte[buff.Length];

            nres = enigma.Encrypt(res);
            File.WriteAllBytes("decrypted_" + filename, nres);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
 