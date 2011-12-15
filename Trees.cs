using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeGenSolve
{
    class Tree<T>
    {
        public readonly Node<T> FirstNode; 
        public Tree(T data)
        {
            FirstNode = new Node<T>(data);
        }
        public class Node<T>
        {
            public readonly T data;
            public readonly Node<T> Parent;
            public readonly List<Node<T>> Children;
            public bool HasChildren
            {
                get { return Children.Count > 0; }
                private set { }
            }

            public bool HasParent;
            internal Node(T arg )
            {
                data = arg;
                Children = new List<Node<T>>();
            }
            internal Node(T arg, Node<T> parent) : this(arg)
            {
                this.Parent = parent;
                HasParent = true;
            }
        }
    }
}
