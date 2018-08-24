using System;
using System.Collections.Generic;

namespace C_Sharp_Design_Patterns
{
    public class LiskovSubstitution
    {
        static public int Area(Rectangle r) => r.Width * r.Height;

        static Random rand = new Random();
        static IEnumerable<int> GetRandomNumbers(int count)
        {
            for (int i = 0; i < count; i++)
                yield return rand.Next();
        }

        static void Main(string[] args)
        {
            foreach (int num in GetRandomNumbers(10))
                Console.WriteLine(num);



        }
    }

    public class Rectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle()
        {
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }


}
