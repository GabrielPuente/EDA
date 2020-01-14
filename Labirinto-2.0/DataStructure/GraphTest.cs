using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoGrafos.DataStructure;

namespace Labirinto.Tests
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void AddNodeTest()
        {
            Graph g = new Graph();

            string nodeName = "TestNodeName";
            g.AddNode(nodeName);

            Assert.AreEqual(g.Nodes.Count, 1);
            Assert.AreEqual(g.Nodes[0].Name, nodeName);
        }
        
        [TestMethod]
        public void AddEdgeTest()
        {
            Graph g = new Graph();

            string nodeName = "TestNodeName";
            string nodeName2 = "TestNodeName2";

            g.AddNode(nodeName);
            g.AddNode(nodeName2);

            g.AddEdge(nodeName, nodeName2, 42);

            Assert.AreEqual(g.Nodes.Count, 2);
            Assert.AreEqual(g.Nodes[0].Name, nodeName);
            Assert.AreEqual(g.Nodes[1].Name, nodeName2);

            Assert.AreEqual(g.Nodes[0].Edges.Count, 1);
            Assert.AreEqual(g.Nodes[0].Edges[0].From.Name, nodeName);
            Assert.AreEqual(g.Nodes[0].Edges[0].To.Name, nodeName2);
            Assert.AreEqual(g.Nodes[0].Edges[0].Cost, 42);
        }

        /// <summary>
        /// O método RemoveNode não foi implementado em sala.
        /// A sua assinatura do método deve ser:
        ///    public void RemoveNode(string name)
        /// Ele deve remover o nó, suas aretas e todas a arestas dos outros nós que tem ele como destino.
        /// </summary>
        [TestMethod]
        public void RemoveNodeTest()
        {
            Graph g = new Graph();

            string nodeName = "TestNodeName";
            string nodeName2 = "TestNodeName2";

            g.AddNode(nodeName);
            g.AddNode(nodeName2);

            g.RemoveNode(nodeName);

            Assert.AreEqual(g.Nodes.Count, 1);
            Assert.AreEqual(g.Nodes[0].Name, nodeName2);
        }
        
        /// <summary>
        /// O método GetNeighbours não foi implementado em sala.
        /// A sua assinatura do método deve ser:
        ///   public Node[] GetNeighbours(string name)
        /// Ele deve retornar os vizinhos do nó cujo nome foi passado como parâmetro
        /// </summary>
        [TestMethod]
        public void GetNeighboursTest()
        {
            Graph g = new Graph();

            string nodeName = "TestNodeName";
            string nodeName2 = "TestNodeName2";
            string nodeName3 = "TestNodeName3";

            g.AddNode(nodeName);
            g.AddNode(nodeName2);
            g.AddNode(nodeName3);


            g.AddEdge(nodeName, nodeName2, 1);
            g.AddEdge(nodeName2, nodeName3, 1);
            g.AddEdge(nodeName, nodeName3, 1);

            var neighboursNode1 = g.GetNeighbours(nodeName);

            Assert.AreEqual(neighboursNode1.Length, 2);
            Assert.AreEqual(neighboursNode1[0].Name, nodeName2);
            Assert.AreEqual(neighboursNode1[1].Name, nodeName3);

            var neighboursNode2 = g.GetNeighbours(nodeName2);

            Assert.AreEqual(neighboursNode2.Length, 1);
            Assert.AreEqual(neighboursNode2[0].Name, nodeName3);

            var neighboursNode3 = g.GetNeighbours(nodeName3);

            Assert.AreEqual(neighboursNode3.Length, 0);

        }
        
        /// <summary>
        /// A assinatura do método ClearVisited deve ser:
        ///   public void ClearVisited()
        /// Ele deve atribuir o valor falso ao atributo Visited e nulo ao atributo Parent
        /// de todos os nós do grafo
        /// </summary>
        [TestMethod]
        public void ClearVisitedTest()
        {
            Graph g = new Graph();

            string nodeName = "TestNodeName";
            string nodeName2 = "TestNodeName2";
            string nodeName3 = "TestNodeName3";

            g.AddNode(nodeName);
            g.AddNode(nodeName2);
            g.AddNode(nodeName3);

            foreach (var node in g.Nodes)
            {
                node.Visited = true;
                Assert.IsTrue(node.Visited);
            }

            g.ClearVisited();

            foreach (var node in g.Nodes)
            {
                Assert.IsFalse(node.Visited);
            }

        }

        [TestMethod]
        public void BreadthFirstSearchTest()
        {
            Graph g = new Graph();

            string nodeNameA = "TestNodeNameA";
            string nodeNameB = "TestNodeNameB";
            string nodeNameC = "TestNodeNameC";
            string nodeNameD = "TestNodeNameD";
            string nodeNameE = "TestNodeNameE";
            string nodeNameF = "TestNodeNameF";
            string nodeNameG = "TestNodeNameG";
            string nodeNameH = "TestNodeNameH";

            g.AddNode(nodeNameA);
            g.AddNode(nodeNameB);
            g.AddNode(nodeNameC);
            g.AddNode(nodeNameD);
            g.AddNode(nodeNameE);
            g.AddNode(nodeNameF);
            g.AddNode(nodeNameG);
            g.AddNode(nodeNameH);
            
            g.AddEdge(nodeNameA, nodeNameB, 1);
            g.AddEdge(nodeNameA, nodeNameC, 1);
            g.AddEdge(nodeNameA, nodeNameG, 1);
            g.AddEdge(nodeNameB, nodeNameE, 1);
            g.AddEdge(nodeNameE, nodeNameG, 1);
            g.AddEdge(nodeNameF, nodeNameH, 1);
            g.AddEdge(nodeNameG, nodeNameF, 1);
            g.AddEdge(nodeNameC, nodeNameF, 1);
            g.AddEdge(nodeNameC, nodeNameD, 1);
            g.AddEdge(nodeNameD, nodeNameH, 1);
            
            g.AddEdge(nodeNameB, nodeNameA, 1);
            g.AddEdge(nodeNameC, nodeNameA, 1);
            g.AddEdge(nodeNameG, nodeNameA, 1);
            g.AddEdge(nodeNameE, nodeNameB, 1);
            g.AddEdge(nodeNameG, nodeNameE, 1);
            g.AddEdge(nodeNameH, nodeNameF, 1);
            g.AddEdge(nodeNameF, nodeNameG, 1);
            g.AddEdge(nodeNameF, nodeNameC, 1);
            g.AddEdge(nodeNameD, nodeNameC, 1);
            g.AddEdge(nodeNameH, nodeNameD, 1);
            var bfs = g.BreadthFirstSearch(nodeNameA);

            Assert.AreEqual(g.Nodes.Count, bfs.Count);

            //Primeiro deve ser o A
            Assert.AreEqual(bfs[0].Name, nodeNameA);

            //Ultimo deve ser o H
            Assert.AreEqual(bfs[bfs.Count - 1].Name, nodeNameH);

            //Nos de nivel 2 devem ser C, B, G (em qualquer ordem), porem depois de A
            var level2Nodes = bfs.Skip(1).Take(3).ToList();

            Assert.IsTrue(level2Nodes.Any(n => n.Name == nodeNameC));
            Assert.IsTrue(level2Nodes.Any(n => n.Name == nodeNameB));
            Assert.IsTrue(level2Nodes.Any(n => n.Name == nodeNameG));

            //Nos de nivel 3 devem ser F, D, E (em qualquer ordem), porem antes de H 
            //e depois de todos os nos de nivel2

            var level3Nodes = bfs.Skip(4).Take(3).ToList();

            Assert.IsTrue(level3Nodes.Any(n => n.Name == nodeNameF));
            Assert.IsTrue(level3Nodes.Any(n => n.Name == nodeNameD));
            Assert.IsTrue(level3Nodes.Any(n => n.Name == nodeNameE));
        }

        [TestMethod]
        public void DepthFirstSearchTest()
        {
            Graph g = new Graph();

            string nodeNameA = "A";
            string nodeNameB = "B";
            string nodeNameC = "C";
            string nodeNameD = "D";
            string nodeNameE = "E";
            string nodeNameF = "F";
            string nodeNameG = "G";
            string nodeNameH = "H";

            g.AddNode(nodeNameA);
            g.AddNode(nodeNameB);
            g.AddNode(nodeNameC);
            g.AddNode(nodeNameD);
            g.AddNode(nodeNameE);
            g.AddNode(nodeNameF);
            g.AddNode(nodeNameG);
            g.AddNode(nodeNameH);


            g.AddEdge(nodeNameA, nodeNameB, 1);
            g.AddEdge(nodeNameA, nodeNameC, 1);
            g.AddEdge(nodeNameA, nodeNameG, 1);
            g.AddEdge(nodeNameB, nodeNameE, 1);
            g.AddEdge(nodeNameE, nodeNameG, 1);
            g.AddEdge(nodeNameF, nodeNameH, 1);
            g.AddEdge(nodeNameG, nodeNameF, 1);
            g.AddEdge(nodeNameC, nodeNameF, 1);
            g.AddEdge(nodeNameC, nodeNameD, 1);
            g.AddEdge(nodeNameD, nodeNameH, 1);
            
            g.AddEdge(nodeNameB, nodeNameA, 1);
            g.AddEdge(nodeNameC, nodeNameA, 1);
            g.AddEdge(nodeNameG, nodeNameA, 1);
            g.AddEdge(nodeNameE, nodeNameB, 1);
            g.AddEdge(nodeNameG, nodeNameE, 1);
            g.AddEdge(nodeNameH, nodeNameF, 1);
            g.AddEdge(nodeNameF, nodeNameG, 1);
            g.AddEdge(nodeNameF, nodeNameC, 1);
            g.AddEdge(nodeNameD, nodeNameC, 1);
            g.AddEdge(nodeNameH, nodeNameD, 1);

            var dfs = g.DepthFirstSearch(nodeNameA);

            Assert.AreEqual(g.Nodes.Count, dfs.Count);

            var validSolutions = new List<List<string>>();

            validSolutions.Add(new List<string> { "A", "G", "E", "B", "F", "C", "D", "H" });
            validSolutions.Add(new List<string> { "A", "G", "E", "B", "F", "H", "D", "C" });
            validSolutions.Add(new List<string> { "A", "B", "E", "G", "F", "H", "D", "C" });
            validSolutions.Add(new List<string> { "A", "B", "E", "G", "F", "C", "D", "H" });
            validSolutions.Add(new List<string> { "A", "B", "E", "G", "F", "H", "D", "C" });
            validSolutions.Add(new List<string> { "A", "G", "F", "H", "D", "C", "E", "B" });
            validSolutions.Add(new List<string> { "A", "G", "F", "C", "D", "H", "E", "B" });
            validSolutions.Add(new List<string> { "A", "C", "D", "H", "F", "G", "E", "B" });
            validSolutions.Add(new List<string> { "A", "C", "F", "H", "D", "G", "E", "B" });

            Assert.IsTrue(validSolutions.Any(solution => solution.SequenceEqual(dfs.Select(n => n.Name))));

        }
        
        [TestMethod]
        public void MSTTest()
        {
            Graph g = new Graph();

            string nodeNameA = "A";
            string nodeNameB = "B";
            string nodeNameC = "C";
            string nodeNameD = "D";
            string nodeNameE = "E";
            string nodeNameF = "F";
            string nodeNameG = "G";

            g.AddNode(nodeNameA);
            g.AddNode(nodeNameB);
            g.AddNode(nodeNameC);
            g.AddNode(nodeNameD);
            g.AddNode(nodeNameE);
            g.AddNode(nodeNameF);
            g.AddNode(nodeNameG);

            g.AddEdge(nodeNameA, nodeNameB, 7);
            g.AddEdge(nodeNameB, nodeNameA, 7);

            g.AddEdge(nodeNameA, nodeNameD, 5);
            g.AddEdge(nodeNameD, nodeNameA, 5);

            g.AddEdge(nodeNameB, nodeNameC, 8);
            g.AddEdge(nodeNameC, nodeNameB, 8);

            g.AddEdge(nodeNameB, nodeNameD, 9);
            g.AddEdge(nodeNameD, nodeNameB, 9);

            g.AddEdge(nodeNameB, nodeNameE, 5);
            g.AddEdge(nodeNameE, nodeNameB, 5);

            g.AddEdge(nodeNameC, nodeNameE, 7);
            g.AddEdge(nodeNameE, nodeNameC, 7);

            g.AddEdge(nodeNameD, nodeNameE, 15);
            g.AddEdge(nodeNameE, nodeNameD, 15);

            g.AddEdge(nodeNameD, nodeNameF, 6);
            g.AddEdge(nodeNameF, nodeNameD, 6);

            g.AddEdge(nodeNameE, nodeNameF, 8);
            g.AddEdge(nodeNameF, nodeNameE, 8);

            g.AddEdge(nodeNameE, nodeNameG, 9);
            g.AddEdge(nodeNameG, nodeNameE, 9);

            g.AddEdge(nodeNameF, nodeNameG, 11);
            g.AddEdge(nodeNameG, nodeNameF, 11);

            Graph primTree = g.Prim(nodeNameA);
            ValidateMST(primTree, g.Nodes.Count, nodeNameA, 78.0);
            Graph kruskalTree = g.Kruskal();
            ValidateMST(kruskalTree, g.Nodes.Count, nodeNameA, 78.0);
        }

        /// <summary>
        /// Valida a árvore gerada por Kruskal e Prim.
        /// Assume que a classe Graph possui uma propriedade chamada Nodes que é a lista de nós do grafo.
        /// </summary>
        /// <param name="mst">Árvore a ser avaliada</param>
        /// <param name="nodeCount">A quantidade de nós que a árvore deve ter</param>
        /// <param name="nodeName">Node de um nó que deve existir na árvore</param>
        /// <param name="cost">Custo que a árvore deve ter</param>
        void ValidateMST(Graph mst, int nodeCount, string nodeName, double cost)
        {
            Assert.IsNotNull(mst, null);

            Assert.AreEqual(mst.Nodes.Count, nodeCount);

            var sum = 0.0;
            mst.Nodes.ForEach(n => n.Edges.ForEach(e => sum += e.Cost));
            Assert.AreEqual(sum, cost);

            mst.BreadthFirstSearch(nodeName);
            var allVisited = true;
            mst.Nodes.ForEach(n => allVisited &= n.Visited);
            Assert.IsTrue(allVisited);
        }

        /// <summary>
        /// Teste o algoritmo de caminho mínimo.
        /// Assume que a função retorna a sequência de nós visitados e que o Parent de cada nó está
        /// preenchido com o nó a partir do qual ele foi atingido.
        /// </summary>
        [TestMethod]
        public void ShortestPathTest()
        {
            Graph g = new Graph();

            string nodeNameA = "A";
            string nodeNameB = "B";
            string nodeNameC = "C";
            string nodeNameD = "D";
            string nodeNameE = "E";
            string nodeNameF = "F";
            string nodeNameG = "G";
            string nodeNameH = "H";

            g.AddNode(nodeNameA);
            g.AddNode(nodeNameB);
            g.AddNode(nodeNameC);
            g.AddNode(nodeNameD);
            g.AddNode(nodeNameE);
            g.AddNode(nodeNameF);
            g.AddNode(nodeNameG);
            g.AddNode(nodeNameH);

            g.AddEdge(nodeNameA, nodeNameB, 4);
            g.AddEdge(nodeNameA, nodeNameC, 3);
            g.AddEdge(nodeNameA, nodeNameG, 1);
            g.AddEdge(nodeNameB, nodeNameE, 3);
            g.AddEdge(nodeNameE, nodeNameG, 6);
            g.AddEdge(nodeNameF, nodeNameH, 7);
            g.AddEdge(nodeNameG, nodeNameF, 2);
            g.AddEdge(nodeNameC, nodeNameF, 5);
            g.AddEdge(nodeNameC, nodeNameD, 8);
            g.AddEdge(nodeNameD, nodeNameH, 5);

            g.AddEdge(nodeNameB, nodeNameA, 4);
            g.AddEdge(nodeNameC, nodeNameA, 3);
            g.AddEdge(nodeNameG, nodeNameA, 1);
            g.AddEdge(nodeNameE, nodeNameB, 3);
            g.AddEdge(nodeNameG, nodeNameE, 6);
            g.AddEdge(nodeNameH, nodeNameF, 7);
            g.AddEdge(nodeNameF, nodeNameG, 2);
            g.AddEdge(nodeNameF, nodeNameC, 5);
            g.AddEdge(nodeNameD, nodeNameC, 8);
            g.AddEdge(nodeNameH, nodeNameD, 5);

            g.ShortestPath(nodeNameA, nodeNameB).Select(n => n.Name).ToList();
            var shortestPathAB = GetPath(g, nodeNameB);
            var solutionShortestPathAB = new List<string> { "A", "B" };
            CollectionAssert.AreEqual(solutionShortestPathAB, shortestPathAB);

            g.ShortestPath(nodeNameA, nodeNameC).Select(n => n.Name).ToList();
            var shortestPathAC = GetPath(g, nodeNameC);
            var solutionShortestPathAC = new List<string> { "A", "C" };
            CollectionAssert.AreEqual(solutionShortestPathAC, shortestPathAC);

            g.ShortestPath(nodeNameA, nodeNameD).Select(n => n.Name).ToList();
            var shortestPathAD = GetPath(g, nodeNameD);
            var solutionShortestPathAD = new List<string> { "A", "C", "D" };
            CollectionAssert.AreEqual(solutionShortestPathAD, shortestPathAD);

            g.ShortestPath(nodeNameA, nodeNameE).Select(n => n.Name).ToList();
            var shortestPathAE = GetPath(g, nodeNameE);
            var solutionShortestPathAE = new List<string> { "A", "B", "E" };
            var solutionShortestPathAE2 = new List<string> { "A", "G", "E" };
            Assert.IsTrue(solutionShortestPathAE.SequenceEqual(shortestPathAE) || solutionShortestPathAE2.SequenceEqual(solutionShortestPathAE2));

            g.ShortestPath(nodeNameA, nodeNameF).Select(n => n.Name).ToList();
            var shortestPathAF = GetPath(g, nodeNameF);
            var solutionShortestPathAF = new List<string> { "A", "G", "F" };
            CollectionAssert.AreEqual(solutionShortestPathAF, shortestPathAF);

            g.ShortestPath(nodeNameA, nodeNameG).Select(n => n.Name).ToList();
            var shortestPathAG = GetPath(g, nodeNameG);
            var solutionShortestPathAG = new List<string> { "A", "G" };
            CollectionAssert.AreEqual(solutionShortestPathAG, shortestPathAG);

            g.ShortestPath(nodeNameA, nodeNameH).Select(n => n.Name).ToList();
            var shortestPathAH = GetPath(g, nodeNameH);
            var solutionShortestPathAH = new List<string> { "A", "G", "F", "H" };
            CollectionAssert.AreEqual(solutionShortestPathAH, shortestPathAH);
        }

        public List<string> GetPath(Graph g, string endNode)
        {
            Node end = g.FindNode(endNode);
            List<string> path = new List<string>();
            while (end != null)
            {
                path.Add(end.Name);
                end = end.Parent;
            }
            path.Reverse();
            return path;
        }

    }
}
