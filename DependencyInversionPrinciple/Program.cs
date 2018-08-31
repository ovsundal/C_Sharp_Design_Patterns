using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace DependencyInversionPrinciple
{
    //High level parts of the system should not depend on low level parts of the system directly,
    //should instead depend on some kind of abstraction (interface). 
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    //High level part of the system
    public class Person
    {
        public string Name;
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    //Low level part of the system
    public class Relationships : IRelationshipBrowser
    {
        //data structure can be changed because it is never exposed to the high level modules which
        //are actually consuming it 
        private List<(Person, Relationship, Person)> _relations
        = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            _relations.Add((parent, Relationship.Parent, child));
            _relations.Add((child, Relationship.Child, parent));
        }

        public List<(Person, Relationship, Person)> Relations => _relations;

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            foreach (var r in _relations.Where(
                x => x.Item1.Name == name &&
                     x.Item2 == Relationship.Parent
            ))
            {
                yield return r.Item3;
            }
        }
    }
    //High level part
    public class Research
    {
        // BAD WAY - accesses low level part here
        //public Research(Relationships relationships)
        //{
        //    var relations = relationships.Relations;
        //    foreach (var r in relations.Where(
        //        x => x.Item1.Name == "John" &&
        //             x.Item2 == Relationship.Parent
        //    ))
        //    {
        //        Console.WriteLine($"John has a child called {r.Item3.Name}");
        //    }
        //}

        //GOOD WAY - not dependant on low level implementation. _relations is abstracted
        //away into an interface which is used as argument in this constructor
        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
            {
                Console.WriteLine($"John has a child called {p.Name}");
            }
        }

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }
    }
}
