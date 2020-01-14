using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.DataStructure
{
    /// <summary>
    /// Classe que representa um grafo.
    /// </summary>
    public class Graph
    {
        List<Node> nos = new List<Node>();

        public List<Node> ShortestPath(string begin, string end)
        {
            Graph q = new Graph();
            Node n = new Node(begin, 0)
            {
                Parent = null
            };
            q.nos.Add(n);
            
            while (!n.Name.Equals(end))
            {
                n = null;
                foreach (Node node in q.nos)
                {
                    Node externo = this.SearchnNode(node.Name);
                    foreach (Edge e in externo.Edges)
                    {
                        Node interno = q.SearchnNode(e.To.Name);
                        if (interno == null)
                        {
                            if (n == null || (Convert.ToDouble(node.Info) + e.Cost) < Convert.ToDouble(n.Info))
                            {
                                n = new Node(e.To.Name, Convert.ToDouble(node.Info) + e.Cost)
                                {
                                    Parent = node
                                };
                            } 
                        }
                    }
                }
                q.nos.Add(n);
                q.AddEdge(n.Name, n.Parent.Name, 0);
            }
            return q.DepthFirstSearch(end);
        }

        public List<Node> BreadthFirstSearch(string begin)
        {
            Queue<Node> queue = new Queue<Node>();
            Node n = SearchnNode(begin);
            queue.Enqueue(n);
            List<Node> nos = new List<Node>();

            while (queue.Count > 0)
            {
                Node no = queue.Dequeue();
                foreach (Edge ed in no.Edges)
                {
                    if (no.Visited != true)
                    {
                        nos.Add(no);
                        no.Visited = true;
                    }   
                    if (ed.To.Visited != true)
                    {
                        ed.To.Parent = no;
                        queue.Enqueue(ed.To);
                    }
                }
            }
            return nos;
         }

        public List<Node> DepthFirstSearch(string begin)
        {
            /*
            Stack<Node> stack = new Stack<Node>();
            Node n = SearchnNode(begin);
            stack.Push(n);
            List<Node> nos = new List<Node>();

            while (stack.Count > 0)
            {
                Node no = stack.Pop();

                foreach (Edge ed in no.Edges)
                {
                    if (no.Visited != true)
                    {
                        nos.Add(no);
                        no.Visited = true;
                    }
                        if (ed.To.Visited != true)
                    {
                        ed.To.Parent = no;
                        stack.Push(ed.To); 
                    }
                }
                
            }
            return nos;
            */
            
            Node n = SearchnNode(begin);
            List<Node> nodes = new List<Node>();
            nodes=Recursividade(n, nodes);
            return nodes;
            
        }
        
        public List<Node> Recursividade(Node no, List<Node> nodes)
        {
            foreach (Edge ed in no.Edges)
            {
                if (no.Visited != true)
                {
                    nodes.Add(no);
                    no.Visited = true;
                }
                if (ed.To.Visited != true)
                {
                    ed.To.Parent = no;
                    Recursividade(ed.To,nodes);
                }
            }
            return nodes;
        }

        public Node SearchnNode(string info)
        {
            foreach (Node no in nos)
            {
                if (no.Name.Equals(info))
                    return no;
            }
            return null;
        }

        public void AddNode(string name, object info)
        {
            Node n = new Node();
            n.Info = info;
            n.Name = name;
            nos.Add(n);
        }

        public void AddEdge(string nameFrom, string nameTo, double cost)
        {
            Node from = SearchnNode(nameFrom);
            Node to = SearchnNode(nameTo);
            from.AddEdge(to, cost);
        }
    }
}