using System;
using System.Collections.Generic;

namespace BreadthFirstSearch
{
    class MainClass
    {
        private static readonly int MaxId = 9;
        private static readonly int MaxVerticesPerLvl = 3;
        private static readonly int MaxLvl = 3;

        static Random random = new Random();

        public static void Main(string[] args)
        {
            var graph = new Vertex();
            InitializeVertices(graph, MaxId, MaxVerticesPerLvl, MaxLvl);

            int input = Convert.ToInt32(Console.ReadLine());
            var result = FindVertexById(graph, input);
        }

        public static void InitializeVertices(Vertex vertex, int maxId, int maxVerticesPerLvl, int maxLvl)
        {
            vertex.Id = random.Next(maxId);
            vertex.Vertices = new List<Vertex>();

            if (maxLvl == 0)
            {
                return;
            }

            for (int i = 0; i < random.Next(1, maxVerticesPerLvl); i++)
            {
                vertex.Vertices.Add(new Vertex()
                {
                    Id = random.Next(maxId),
                    Vertices = new List<Vertex>()
                });
            }

            foreach (var item in vertex.Vertices)
            {
                InitializeVertices(item, maxId, maxVerticesPerLvl, maxLvl - 1);
            }
        }

        public static Vertex FindVertexById(Vertex vertex, int id)
        {
            Queue<Vertex> queue = new Queue<Vertex>();
            queue.Enqueue(vertex);

            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();

                if (!currentVertex.Searched)
                {
                    currentVertex.Searched = true;

                    Console.WriteLine(currentVertex.Id);

                    if (currentVertex.Id == id)
                    {
                        return currentVertex;
                    }
                    else
                    {
                        currentVertex.Vertices.ForEach(vertex => queue.Enqueue(vertex));
                    }
                }
            }

            return null;
        }
    }
}

public class Vertex
{
    public int Id { get; set; }
    public List<Vertex> Vertices { get; set; }
    public bool Searched { get; set; }
}
