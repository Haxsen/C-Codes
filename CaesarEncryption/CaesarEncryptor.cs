using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CesarCryptography
{
    class Encryption
    {
        private int a, k, ain;
        private int[] aa1 = new int[95], aa2 = new int[95], aa = new int[95], ainv = new int[95];
        public int A { get { return a; } set { a = value; } }
        public int Key { get { return k; } set { k = value; } }

        public void PrintValidA()
        {
            GetAArray();
            for (int i = 0; i < aa.Length; i++)
            {
                Console.Write(aa[i] + " ");
            }
        }
        public bool CheckA()
        {
            for (int i = 0; i < aa.Length; i++)
            {
                if (a == aa[i])
                {
                    ain = ainv[i];
                    Console.WriteLine("A inverse is: " + ain);
                    break;
                }
                else
                {
                    ain = 0;
                }
            }
            if (ain != 0)
                return true;
            else
                return false;
        }
        public string GetEncryptedMessage(string s)
        {
            string em = "";
            for (int i = 0; i < s.Length; i++)
            {
                int x = (int)s[i];
                Console.Write(x + " ");
                x -= 32;
                x = ((x * a) + Key) % aa1.Length;
                x += 32;
                char c = (char)x;
                em += c.ToString();
            }

            return em;
        }
        public string GetDecryptedMessage(string s)
        {
            string em = "";
            for (int i = 0; i < s.Length; i++)
            {
                int x = (int)s[i];
                x -= 32;
                x -= Key;
                if (x < 0)
                {
                    x %= -aa1.Length;
                    x = aa1.Length + x;
                }
                x *= ain;
                x %= aa1.Length;
                x += 32;
                //Console.Write(x + " ");
                char c = (char)x;
                em += c.ToString();
            }

            return em;
        }

        void GetAArray()
        {
            int x = 0;
            for (int i = 0; i < aa1.Length; i++)
            {
                for (int j = 0; j < aa2.Length; j++)
                {
                    int r = i * j;
                    r = r % aa1.Length;
                    if (r == 1)
                    {
                        int[] aax = new int[x];
                        int[] aay = new int[x];
                        for (int y = 0; y < aax.Length; y++)
                        {
                            aax[y] = aa[y];
                        }
                        for (int y = 0; y < aay.Length; y++)
                        {
                            aay[y] = ainv[y];
                        }
                        x += 1;
                        aa = new int[x];
                        for (int y = 0; y < aax.Length; y++)
                        {
                            aa[y] = aax[y];
                        }
                        aa[x - 1] = i;
                        ainv = new int[x];
                        for (int y = 0; y < aay.Length; y++)
                        {
                            ainv[y] = aay[y];
                        }
                        ainv[x - 1] = j;
                        break;
                    }
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Encryption e = new Encryption() { A = 5, Key = 5 };
        re:
            Console.WriteLine("Enter value of A from given values below:");
            e.PrintValidA();
            Console.WriteLine("");
        enterA:
            Console.Write("A = ");
            try
            {
                e.A = Convert.ToInt16(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto enterA;
            }
            if (!e.CheckA())
            {
                Console.WriteLine("Wrong input for A, please enter the value again.");
                goto enterA;
            }
        enterK:
            Console.Write("Key? ");
            try
            {
                e.Key = Convert.ToInt16(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto enterK;
            }
            string m;
            Console.WriteLine("Enter your message to encrypt: ");
            m = Console.ReadLine();
            Console.WriteLine("\nThe corresponding numbers are: ");
            string enm = e.GetEncryptedMessage(m);
            Console.WriteLine("\n\nYour encrypted message is: ");
            Console.WriteLine("\n" + enm);
            Console.WriteLine("\nYour decrypted message is: ");
            m = e.GetDecryptedMessage(enm);
            Console.WriteLine("\n" + m);
            Console.WriteLine("\nPress ESC key to exit or any other key to restart program.");
            ConsoleKeyInfo k = Console.ReadKey();
            if (k.Key != ConsoleKey.Escape)
            {
                Console.Clear();
                goto re;
            }
        }
    }
}

/*
 * Sequence of execution:
 * 1. Table print for 'a'
 * 2. Get key
 * 3. Get string
 * 4. Encrypt
 * 5. Into string
 * 6. Print encrypted string
 * 7. Decrypt
 * 8. Print decrypted
*/
