using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    class Enigma
    {
        Rotor rl, rm, rr;
        Reflector reflector;

        public Enigma(int rrnum, int rmnum, int rlnum)
        {
            rr = new Rotor((byte)rrnum, Generator.GenerateRandomByteArray(20));
            rm = new Rotor((byte)rmnum, Generator.GenerateRandomByteArray(170));
            rl = new Rotor((byte)rlnum, Generator.GenerateRandomByteArray(2));

            reflector = new Reflector(Generator.GenerateReflector(98));

            rr.SetNextRotor(rm); 
            rm.SetNextRotor(rl);
            

            rm.SetPreviousRotor(rr);
            rl.SetPreviousRotor(rm);
           
        }

        public void SetRotorsPos(int rrnum, int rmnum, int rlnum)
        {
            rr.SetRotorPos((byte)rrnum);
            rm.SetRotorPos((byte)rmnum);
            rl.SetRotorPos((byte)rlnum);
        }

        public byte[] Encrypt(byte[] message)
        {
            byte[] resmes = new byte[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                rr.Move();
                resmes[i] = rr.ForwardPass(message[i]);
                resmes[i] = reflector.Reflect(resmes[i]);
                resmes[i] = rl.BackPass(resmes[i]);      
            }

            return resmes;
        }

    }
}
