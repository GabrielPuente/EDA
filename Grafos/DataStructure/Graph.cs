using System;
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


        #region Atributos

        /// <summary>
        /// Lista de nós que compõe o grafo.
        /// </summary>
        private List<Node> nodes;

        #endregion

        #region Propridades

        /// <summary>
        /// Mostra todos os nós do grafo.
        /// </summary>
        public Node[] Nodes
        {
            get { return this.nodes.ToArray(); }
        }

        #endregion

        #region Construtores

        /// <summary>
        /// Cria nova instância do grafo.
        /// </summary>
        public Graph()
        {
            this.nodes = new List<Node>();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Encontra o nó através do seu nome.
        /// </summary>
        /// <param name="name">O nome do nó.</param>
        /// <returns>O nó encontrado ou nulo caso não encontre nada.</returns>
        private Node Find(string name)
        {
            foreach(Node n in nodes)
            {
                if (n.Name == name)
                    return n;
            }

            return null;
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name)
        {
            //Node node = new Node();
            //node.Name = name;
            //nodes.Add(node);

            AddNode(name, null);
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name, object info)
        {
            //Node node = new Node();
            //node.Name = name;
            //node.Info = info;

            //nodes.Add(node);

            if (Find(name) != null)
                throw new Exception("nó já existente");

            Node n = new Node(name, info);
            nodes.Add(n);
        }

        /// <summary>
        /// Remove um nó do grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser removido.</param>
        public void RemoveNode(string name)
        {
            foreach(Node n in nodes)
            {
                if (n.Name == name)
                    nodes.Remove(n);
            }
        }

        /// <summary>
        /// Adiciona o arco entre dois nós associando determinado custo.
        /// </summary>
        /// <param name="from">O nó de origem.</param>
        /// <param name="to">O nó de destino.</param>
        /// <param name="cost">O cust associado.</param>
        public void AddEdge(string from, string to, double cost)
        {
            Node nFrom = Find(from);
            Node nTo = Find(to);

            Edge edge = new Edge(nFrom, nTo, cost);

            nFrom.AddEdge(nTo, cost);
        }

        /// <summary>
        /// Obtem todos os nós vizinhos de determinado nó.
        /// </summary>
        /// <param name="node">O nó origem.</param>
        /// <returns></returns>
        public Node[] GetNeighbours(string from)
        {
            Node[] neighbours = new Node[100];
            Node nFrom = Find(from);
            int cont = 0;

            foreach (Node n in nodes)
            {
                foreach(Edge e in n.Edges)
                {
                    neighbours[cont] = e.To;
                    cont++;
                }
            }

            return neighbours;
        }

        /// <summary>
        /// Valida um caminho, retornando a lista de nós pelos quais ele passou.
        /// </summary>
        /// <param name="nodes">A lista de nós por onde passou.</param>
        /// <param name="path">O nome de cada nó na ordem que devem ser encontrados.</param>
        /// <returns></returns>
        public bool IsValidPath(ref Node[] nodes, params string[] path)
        {

            // popular a lista de nodes aqui
            string[] pathFunc = new string[100];
            int cont = 0;

            foreach(Node n in nodes)
            {
                Node[] neighbours = neighboursFromNode(n);

                foreach(Node nn in neighbours)
                {
                    if(path.Contains(nn.Name))
                    {
                        pathFunc[cont] = nn.Name;
                        cont++;
                    }
                }
            }

            return pathFunc.Equals(path);
        }


        public Node[] neighboursFromNode(Node n)
        {

            if (GetNeighbours(n.Name) != null)
                return GetNeighbours(n.Name);
            else
                return null;
        }

        #endregion

    }
}
