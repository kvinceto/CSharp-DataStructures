using _01.Two_Three;

namespace Demo
{
    using System;

    class Program
    {
        static void Main()
        {
            var tree = new TwoThreeTree<string>();

            tree.Insert("A");
            tree.Insert("B");
            tree.Insert("C");
            Console.WriteLine("B " + Environment.NewLine +
                              "A " + Environment.NewLine +
                              "C");
            Console.WriteLine(tree.ToString().Trim());
        }
    }
}
