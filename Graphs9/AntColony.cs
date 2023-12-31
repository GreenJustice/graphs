﻿using Graph_Lab_9_2;
using GraphLab.Components;
using GraphLab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Lab_9_2
{
    internal class Pheromones
    {
        private double[][] pheromones;
        public Pheromones(int vertexCount)
        {
            pheromones = new double[vertexCount][];
            for (int i = 0; i < vertexCount; i++)
            {
                pheromones[i] = new double[i + 1];
                for (int j = 0; j < pheromones[i].Length; j++)
                {
                    pheromones[i][j] = 0.2;
                }
            }
        }
        public int GetLength { get { return pheromones.Length; } }
        public double this[int i, int j]
        {
            get
            {
                if (j >= pheromones[i].Length) return pheromones[j][i];
                else return pheromones[i][j];
            }
            set
            {
                if (j >= pheromones[i].Length) pheromones[j][i] = value;
                else pheromones[i][j] = value;
            }
        }
        public void Evaporation(double p)
        {
            for (int i = 0; i < pheromones.Length; i++)
            {
                for (int j = 0; j < pheromones[i].Length; j++)
                {
                    pheromones[i][j] = (1 - p) * pheromones[i][j];
                }
            }
        }
        public PriorityQueue<Edge, double> PheromonePriority
        {
            get
            {
                PriorityQueue<Edge, double> pq = new PriorityQueue<Edge, double>();
                for (int i = 0; i < pheromones.Length; i++)
                {
                    for (int j = 0; j < pheromones[i].Length; j++)
                    {
                        pq.Enqueue(new Edge(i, j), 1 / pheromones[i][j]);
                    }
                }
                return pq;
            }
        }
    }

}
internal class AntSystem
{
    private Graph graph;
    private double alpha = 1;
    private double beta = 3.5;
    private Edge[] OptimalPath;
    private int OptimalPathLength = int.MaxValue;
    private double p = 0.2;
    private double q = 15;
    private int beginVertex;
    private Pheromones pheromones;
    private int iterationCount = 200;
    private int EliteAntCount = 5;
    public AntSystem(Graph graph, int beginVertex)
    {
        this.graph = graph;
        pheromones = new Pheromones(graph.VertexCount);
        OptimalPath = new Edge[graph.VertexCount];
        this.beginVertex = beginVertex;
    }
    public (Edge[], int) Run()
    {
        for (int iter = 0; iter < iterationCount; iter++)
        {
            PriorityQueue<Ant, int> ants = new PriorityQueue<Ant, int>();
            int optimalPath = int.MaxValue;
            Edge[] path = new Edge[0];
            for (int i = 0; i < graph.VertexCount; i++)
            {
                Ant ant = new Ant(i);
                AntBypass(ant);
                if (ant.PathLength < optimalPath)
                {
                    optimalPath = ant.PathLength;
                    path = ant.Path.ToArray();
                }
                optimalPath = ant.PathLength;
                ants.Enqueue(ant, ant.PathLength);
            }
            if (optimalPath < OptimalPathLength)
            {
                OptimalPath = path;
                OptimalPathLength = optimalPath;
            }
            pheromones.Evaporation(p);
            for (int i = 0; i < EliteAntCount; i++)
            {
                Ant ant = ants.Dequeue();
                foreach (var edge in ant.Path)
                {
                    if (edge.vi != edge.vj) pheromones[edge.vi, edge.vj] += (double)q / (double)ant.PathLength;
                }
            }

            

        }

        return (OptimalPath.ToArray(), OptimalPathLength);
    }

    public void AntBypass(Ant ant)
    {
        do
        {
            HashSet<KeyValuePair<double, AdjacentVertex>> AttractivenessEachVertexes = new HashSet<KeyValuePair<double, AdjacentVertex>>();
            double TotalAttractiveness = 0;
            foreach (var v in graph.AdjacentList[ant.CurrentNode])
            {
                if (ant.Closed.Contains(v.Vj)) continue;
                double attractiveness = Math.Pow(pheromones[ant.CurrentNode, v.Vj], alpha) / Math.Pow(graph.Weight(ant.CurrentNode, v.Vj), beta);
                KeyValuePair<double, AdjacentVertex> pair = new(attractiveness, v);
                AttractivenessEachVertexes.Add(pair);
                TotalAttractiveness += attractiveness;
            }
            double randomProbability = Random.Shared.NextDouble();
            double cumulativeProbability = 0;
            foreach (var nextVertex in AttractivenessEachVertexes)
            {
                if (randomProbability < cumulativeProbability + nextVertex.Key / TotalAttractiveness)
                {
                    ant.Closed.Add(nextVertex.Value.Vj);
                    ant.Path.Add(new Edge(ant.CurrentNode, nextVertex.Value.Vj, nextVertex.Value.Weight));
                    ant.CurrentNode = nextVertex.Value.Vj;
                    break;
                }
                cumulativeProbability += nextVertex.Key / TotalAttractiveness;
            }
        } while (ant.Closed.Count != graph.VertexCount);
        ant.Path.Add(new Edge(ant.Path.Last().vj, ant.StartVertex, graph.Weight(ant.Path.Last().vj, ant.StartVertex)));
    }

}
internal class Ant
{
    public int CurrentNode { get; set; }
    public int StartVertex { get; set; }
    public List<Edge> Path { get; set; }
    public HashSet<int> Closed { get; set; }
    public int PathLength
    {
        get
        {
            int length = 0;
            foreach (var Edge in Path)
            {
                length += Edge.weight;
            }
            return length;
        }
    }
    public Ant(int startVertex)
    {
        this.CurrentNode = startVertex;
        this.StartVertex = startVertex;
        Path = new List<Edge>();
        Closed = new HashSet<int>() { startVertex };
    }
}
