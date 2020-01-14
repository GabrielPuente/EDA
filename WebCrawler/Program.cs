using System;
using Graph = ProjetoGrafos.DataStructure.Graph;
using Edge = ProjetoGrafos.DataStructure.Edge;
using Node = ProjetoGrafos.DataStructure.Node;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;

namespace WebCawler
{
    class Program
    {
        public static Graph graph = new Graph();
        public static string entrada;
        public static Queue<Node> queue = new Queue<Node>();
        public static string saida;
        public static string common = ("https://pt.wikipedia.org/wiki/");
        public static HashSet<string> urls = new HashSet<string>();

        static void Main(string[] args)
        {
            entrada = Console.ReadLine();
            saida = Console.ReadLine();
            entrada = entrada.Replace(' ', '_');
            saida = saida.Replace(' ', '_');
            if (entrada != saida)
            {
                graph.AddNode(entrada);
                queue.Enqueue(graph.Find(entrada));
                urls.Add(entrada);
                GetRequest(common + entrada);
            }
            else
                Console.WriteLine("Saida eh a entrada");

            Console.ReadLine();
        }

        public async static void GetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        WebCrawler(await content.ReadAsStringAsync());
                    }
                }
            }
        }

        #region Filter
        public static void Filter(string html)
        {
            int indice = -1;
            indice = html.IndexOf("<div class=\"mw-parser-output", indice + 1);
            Stack<int> div = new Stack<int>();
            div.Push(indice);
            int i = 0;
            string newHtml = "";

            while (div.Count > 0)
            {
                if (html[indice + i] == '<' && html[indice + i + 1] == 'd' && html[indice + i + 2] == 'i' && html[indice + i + 3] == 'v')
                    div.Push(indice);

                if (html[indice + i] == '/' && html[indice + i + 1] == 'd' && html[indice + i + 2] == 'i' && html[indice + i + 3] == 'v')
                    div.Pop();

                newHtml += html[indice + i];
                i++;
            }
            WebCrawler(newHtml);
        }
        #endregion

        public static void WebCrawler(string html)
        {
            int indice = -1;
            string aux;
            Node from = queue.Dequeue(), to;
            do
            {
                aux = "";
                indice = html.IndexOf("href=\"/wiki/", indice + 1);

                if (indice >= 0)
                {
                    int i = 12;
                    while (html[indice + i] != '\"')
                    {
                        aux += html[indice + i];
                        i++;
                    }

                    if (!urls.Contains(aux))
                    {
                        Node n = new Node();
                        n.Name = aux;
                        graph.AddNode(n.Name);
                        to = graph.nodes[graph.nodes.Count - 1];
                        graph.AddEdge(from.Name, to.Name);
                        queue.Enqueue(to);
                        to.Parent = from;
                        urls.Add(aux);
                    }
                    if (aux == saida)
                        Write();
                }
            } while (indice >= 0);
            
            GetRequest(common + queue.First());
        }

        public static void Write()
        {
            Stack<Node> s = new Stack<Node>();
            Node from = graph.Find(entrada);
            Node to = graph.Find(saida);
            Node aux = to;

            while (aux != from)
            {
                s.Push(aux);
                aux = aux.Parent;
            }

            s.Push(aux);

            do
            {
                aux = s.Pop();
                if (aux.Name != entrada)
                    Console.Write(aux.Parent.Name + " --> " + aux.Name);
                Console.WriteLine("\n");
            } while (s.Count > 0);
            Console.ReadLine();
        }
    }
}