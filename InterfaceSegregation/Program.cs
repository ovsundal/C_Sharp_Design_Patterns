using System;
using System.Reflection.PortableExecutable;

namespace InterfaceSegregation
{
    public class Document
    {
        
    }
    //Interface segregation principle: "People should not pay for things they don't need" -
    //classes should not implement interfaces which have methods they don't need

    //If you have an interface with too much stuff, just break it apart into smaller interfaces
    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }

        public void Fax(Document d)
        {
            //
        }
    }

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    //interface implements single interfaces
    public interface IMultiFunctionDevice : IScanner, IPrinter //...
    {

    }

    public class MultiFunctionMachine : IMultiFunctionDevice
    {
        private IPrinter _printer;
        private IScanner _scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            _printer = printer;
            _scanner = scanner;
        }

        public void Scan(Document d)
        {
            _scanner.Scan(d);
        } // decorator

        public void Print(Document d)
        {
            _printer.Print(d);
        }
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    //old printer doesn't have scanner and fax - these interface methods should therefore be in separate interfaces
    public class OldFashionedPrinter: IMachine
    {
        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }

        public void Fax(Document d)
        {
            //
        }
    }
}
