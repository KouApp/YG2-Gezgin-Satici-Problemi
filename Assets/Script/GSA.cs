using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Path
{
    public float totalCost;
    public int current;
    public int prev;
    public float destinaiton;
}

public class GSA
{
    private int MinDistance(float[] dist, bool[] sptSet)
    {
        // Initialize min value
        float min = float.MaxValue;
        int minIndex = -1;

        for (int v = 0; v < dist.Length; v++)
        {

            if (sptSet[v] == false && dist[v] <= min)
            {
                min = dist[v];
                minIndex = v;
            }

        }
        return minIndex;
    }

    private int MinDistance(Path[] dist, bool[] sptSet)
    {
        // Initialize min value
        float min = float.MaxValue;
        int minIndex = -1;

        for (int v = 0; v < dist.Length; v++)
        {

            if (sptSet[v] == false && dist[v].totalCost <= min)
            {
                min = dist[v].totalCost;
                minIndex = v;
            }

        }
        return minIndex;
    }

    // A utility function to print
    // the constructed distance array
    private void PrintSolution(int[] dist)
    {
        Debug.Log("Vertex \t\t Distance "
                  + "from Source\n");
        for (int i = 0; i < dist.Length; i++)
            Debug.Log(i + " \t\t " + dist[i] + "\n");
    }

    private bool AllIsTrue(bool[] sptSet)
    {
        for (int i = 0; i < sptSet.Length; i++)
        {
            if (sptSet[i] == false)
            {
                return true;
            }
        }
        return false;
    }

    // Function that implements Dijkstra's
    // single source shortest path algorithm
    // for a graph represented using adjacency
    // matrix representation
    public Path[] GSP(float[][] graph, int src)
    {
        int nodeCount = graph.GetLength(0);
        bool[] sptSet = new bool[nodeCount];
        List<Path> path = new List<Path>();
        for (int i = 0; i < nodeCount; i++)
        {
            sptSet[i] = false;
        }

        int prev, now;
        now = src;
        prev = now;
        path.Add(new Path());
        while (AllIsTrue(sptSet))
        {
            prev = now;
            Path[] dist = Dijkstra(graph, now);
            now = MinDistance(dist, sptSet);
            List<Path> temp = new List<Path>();
            temp = GetPaths(dist, now, prev);
            temp.Reverse();
            path.AddRange(temp);
            sptSet[now] = true;
        }
        
        prev = now;
        Path[] distt = Dijkstra(graph, now);
        List<Path> rpath = new List<Path>();
        rpath = GetPaths(distt, 0, prev);
        rpath.Reverse();
        path.AddRange(rpath);
        foreach (var item in path)
        {
            Debug.Log(item.current + "-");
        }

        return path.ToArray();
        //printSolution(path.ToArray());
    }

    private Path[] Dijkstra(float[][] graph, int src)
    {
        int length = graph.GetLength(0);
        Path[] paths = new Path[length];
        float[] dist = new float[length];
        bool[] sptSet = new bool[length];

        for (int i = 0; i < length; i++)
        {
            paths[i] = new Path();
            dist[i] = float.MaxValue;
            sptSet[i] = false;
        }

        dist[src] = 0;

        // Find shortest path for all vertices
        for (int count = 0; count < length - 1; count++)
        {
            int u = MinDistance(dist, sptSet);

            sptSet[u] = true;

            for (int v = 0; v < length; v++)
            {
                if (!sptSet[v] && graph[u][v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u][v] < dist[v])
                {
                    dist[v] = dist[u] + graph[u][v];
                    paths[v].current = v;
                    paths[v].prev = u;
                    paths[v].destinaiton = v;
                    paths[v].totalCost = dist[v];
                }
            }
        }

        return paths;
    }

    void PrintPath(Path[] paths)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            Debug.Log("Destination : " + paths[i].destinaiton + " Cost : " + paths[i].totalCost + "\n");
            PrintPath(paths, i);
        }
    }

    void PrintPath(Path[] paths, int i)
    {
        if (paths[i].totalCost == 0)
            return;
        Debug.Log("" + paths[i].prev + " ");
        PrintPath(paths, paths[i].prev);
    }

    List<Path> GetPaths(Path[] paths, int now,int prev)
    {
        List<Path> list = new List<Path>();
        if (now == prev)
            return list;
        list.Add(paths[now]);
        list.AddRange(GetPaths(paths, paths[now].prev,prev));
        return list;
    }
}
