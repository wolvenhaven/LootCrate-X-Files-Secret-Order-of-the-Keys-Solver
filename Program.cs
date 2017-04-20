using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootcrateSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            //the words on the pencils
            string pencilOne = "THE TRUTH IS OUT THERE";
            string pencilTwo = "I WANT TO BELIEVE";
            string pencilThree = "TRUST NO ONE";
            string pencilFour = "THAT'S WHY THEY PUT THE 'I' IN F.B.I.";

            //making a set of them
            List<string> set = new List<string>() { pencilOne, pencilTwo, pencilThree, pencilFour };
            var includeTrimmedSet = new List<string>();

            set.ForEach(x => includeTrimmedSet.Add(x.Replace(" ", "").Replace("'", "").Replace(".", "")));

            //getting all permutations of the pencils in every order
            var result = GetPermutations(set, 4);
            var includeTrimmedResults = GetPermutations(includeTrimmedSet, 4);

            //row and column positions of the answer
            var positions = new List<KeyValuePair<int, int>> {
                new KeyValuePair<int, int>(1, 14),
                new KeyValuePair<int, int>(1, 5),
                new KeyValuePair<int, int>(2, 2),
                new KeyValuePair<int, int>(3, 3),
                new KeyValuePair<int, int>(4, 1),
                new KeyValuePair<int, int>(3, 18),
                new KeyValuePair<int, int>(4, 5),
                new KeyValuePair<int, int>(4, 17),
                new KeyValuePair<int, int>(4, 18),
                new KeyValuePair<int, int>(2, 4),
                new KeyValuePair<int, int>(2, 1),
                new KeyValuePair<int, int>(2, 2),
                new KeyValuePair<int, int>(1, 11),
                new KeyValuePair<int, int>(1, 3),
                new KeyValuePair<int, int>(1, 10),
                new KeyValuePair<int, int>(3, 4),
                new KeyValuePair<int, int>(3, 11),
                new KeyValuePair<int, int>(2, 9),
                new KeyValuePair<int, int>(3, 5),
                new KeyValuePair<int, int>(3, 19),
                new KeyValuePair<int, int>(4, 11),
                new KeyValuePair<int, int>(3, 21),
            };

            //getting the output for each combination
            var answers = GetAllOutputs(positions, result);
            var includeTrimmedAnswers = GetAllOutputs(positions, includeTrimmedResults);

            answers.ForEach(x => Console.WriteLine(x));
            includeTrimmedAnswers.ForEach(x => Console.WriteLine(x));

            Console.ReadLine();
        }

        private static List<string> GetAllOutputs(List<KeyValuePair<int, int>> keyPositions, IEnumerable<IEnumerable<string>> combos)
        {
            List<string> outputs = new List<string>();

            foreach (var combo in combos)
            {
                string output = "";
                var tempList = combo.ToList();
                var completeAnswer = true;

                foreach (var position in keyPositions)
                {
                    try
                    {
                        //the puzzle goes through an X on the page for the second letter
                        if(output.Length == 1)
                        {
                            output += "X";
                        }
                        //handle 0 vs 1 index
                        output += tempList[position.Key - 1][position.Value - 1];
                    }
                    catch(Exception ex)
                    {
                        //not a valid combination
                        completeAnswer = false;
                        break;
                    }
                    
                }
                if(completeAnswer == true)
                {
                    outputs.Add(output);
                }
            }

            return outputs;
        }

        //recursively get all combinations of the list values
        static IEnumerable<IEnumerable<T>>GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
