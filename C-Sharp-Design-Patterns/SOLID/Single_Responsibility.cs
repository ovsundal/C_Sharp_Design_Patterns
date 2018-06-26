using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace C_Sharp_Design_Patterns
{
    public class Single_Responsibility
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I cried today");
            j.AddEntry("I ate a bug");
            Console.WriteLine(j);

            var p = new Persistence();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(j, filename, true);
            Process.Start(filename);
        }
    }
    //violation of single responsibility principle because we're adding too much
    //responsibility to the Journal class. Journal is concerned with keeping a 
    //bunch of entries, persistence is concerned with saving whatever object it's being fed
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count;   //memento
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        //public void Save(string filename)
        //{
        //    File.WriteAllText(filename, ToString());
        //}

        //public static Journal Load(string filename)
        //{
           
        //}
    }

    //class saves all sorts of objects - takes responsibility of persistence
    public class Persistence
    {
        public void SaveToFile(Journal j, string filename, bool overwrite)
        {
            if(overwrite || !File.Exists(filename))
                File.WriteAllText(filename, j.ToString());
        }
    }


}
