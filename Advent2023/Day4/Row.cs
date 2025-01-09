using AdventOfCode.Advent2023.Day2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4
{
    public class Row
    {
        private int[] Winning, Extracted;
        public Row(string input)
        {
            if (input == "")
                return;
            var split = input.Split(':');
            
            var split2 = split[1].Split("|");
            var winningstr = split2[0];
            var extractedstr = split2[1];
            
            Winning = FromStringToList(winningstr);
            Extracted = FromStringToList(extractedstr);
        }

        private int[] FromStringToList(string input)
        {
            var split = input.Split(" ");
            var l = new List<int>();
            foreach (var item in split)
            {
                var i = item.Trim();
                if (i != null && i != "")
                    l.Add(Int32.Parse(i));
            }
            return l.ToArray();
        }

        public int CountMatches()
        {
            if (Winning == null) return 0;
            int counter = 0;
            foreach (var item in Winning)
            {
                // int c = counter;
                foreach (var item1 in Extracted)
                {
                    if (item == item1)
                    {
                        counter++;
                        break;
                    }
                }

                /* RIMUOVERE COMMENTO PER AGGIUNGERE LA SINGOLA OCCORRENZA SU OGNI MEMBRO DI WINNING 
                 * if (c != counter)
                 *      break;
                */
            }

            return counter;
        }
    }
}
