using System;

namespace Medium4._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Interpret interpret = new Interpret();

            string UserInput = Console.ReadLine();
            interpret.InterpretInput(UserInput);
        }
    }

    class Interpret
    {
        private ISymbol _symbol;

        public void InterpretInput(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                _symbol = new EmptyInterpret();
                if (input[i] == '#')
                {
                    _symbol = new SharpInterpret();
                }
                else if (input[i] == '*')
                {
                    _symbol = new SequenceInterpret();
                }
                _symbol.Сonduct(i + 1, input);
            }
        }
    }

    class SequenceInterpret: ISymbol
    {
        void ISymbol.Сonduct(int symbolNumber, string input)
        {
            int endSymbol = 0;
            int amount = 0;
            for (int i = symbolNumber; i < input.Length; i++)
            {
                if (input[i] == ';')
                {
                    endSymbol = i;
                    break;
                }
                else
                {
                    amount += Convert.ToInt32(input[i]);
                }
            }
            if(amount % 2 == 0)
            {
                Console.WriteLine("Номер начала последовательности:" + symbolNumber);
                Console.WriteLine("Номер конца последовательности:" + endSymbol);
            }
        }
    }

    class SharpInterpret : ISymbol
    {
        void ISymbol.Сonduct(int symbolNumber, string input)
        {
            Console.WriteLine("Номер символа решётки:" + symbolNumber);
        }
    }

    class EmptyInterpret : ISymbol
    {
        void ISymbol.Сonduct(int symbolNumber, string input)
        {

        }
    }

    interface ISymbol
    {
        void Сonduct(int symbolNumber, string input);
    }
}
