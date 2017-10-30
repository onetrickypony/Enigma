using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    public class Generator
    {

        public static byte[] GenerateRandomByteArray(byte startx)
        {
            byte[] rotor = new byte[256];

            rotor = SetNaturalNums(rotor);
            rotor = Mix(rotor, startx);

            return rotor;
        }

        private static byte[] SetNaturalNums(byte[] rotor)
        {
            for (int i = 0; i < rotor.Length; i++)
            {
                rotor[i] = (byte)i;
            }

            return rotor;
        }

        private static byte[] Mix(byte[] rotor, byte startx) // Тасование Фишера — Йетса
        {
            byte j = startx;

            for (int i = rotor.Length - 1; i > 0; i--)
            {
                byte temp;

                j = Congruential(j, (byte)i);

                temp = rotor[i];
                rotor[i] = rotor[j];
                rotor[j] = temp;
            }

            return rotor;
        }

        private static byte Congruential(byte x, byte m) // функция генерации псевдослучайных чисел
        {
                               // генерация псевдослучайных чисел в диапазоне значений от 0 до 255 
            const byte a = 31; // множитель (0 <= a <= m)
            const byte inc = 176; // инкрементирующее значение (0 <= inc <= m)

            x = (byte)(((a * x) + inc) % m); // формула линейного конгруэнтного метода генерации псевдослучайных чисел  

            return x;
        }


        public static byte[] GenerateReflector(byte startx)
        {
            byte[] reflector = new byte[256];
            Random rand = new Random(startx);
            int x1, x2;

            bool[] bool_ref = new bool[256];
            for (int i = 0; i < bool_ref.Length; i++)
            {
                bool_ref[i] = false;
            }



            while (NotFullTrue(bool_ref))
            {
                x1 = rand.Next() % 256;
                x2 = rand.Next() % 256;

                if (x1 != x2 && bool_ref[x1] == false && bool_ref[x2] == false)
                {
                    reflector[x1] = (byte)x2;
                    reflector[x2] = (byte)x1;
                    bool_ref[x1] = true;
                    bool_ref[x2] = true;
                }
            }

            return reflector;
        }


        private static bool NotFullTrue(bool[] bool_array)
        {
            for (int i = 0; i < bool_array.Length; i++)
            {
                if (bool_array[i] == false)
                    return true;
            }

            return false;
        }

    }
}
