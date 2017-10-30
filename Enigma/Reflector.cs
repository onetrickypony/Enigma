using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    class Reflector
    {
        private byte[] byte_sequence;

        public Reflector(byte[] byte_sequence)
        {
            this.byte_sequence = byte_sequence;
        }

        public byte Reflect(byte b)
        {
            return byte_sequence[b];
        }
    }
}
