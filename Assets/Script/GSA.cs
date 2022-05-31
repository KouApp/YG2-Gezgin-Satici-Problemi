using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Path
{
    public int totalCost;
    public int current;
    public int prev;
    public int destinaiton;
}
public class Dijakstra
    {
        int V = 9;
        int minDistance(int[] dist,
                        bool[] sptSet)
        {
            // Initialize min value
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < dist.Length; v++)
            {
                
                    if (sptSet[v] == false && dist[v] <= min)
                    {
                        min = dist[v];
                        min_index = v;
                    }
                
            }
                
                

            return min_index;
        }
        int minDistance(Path[] dist,
                        bool[] sptSet)
        {
            // Initialize min value
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < dist.Length; v++)
            {

                if (sptSet[v] == false && dist[v].totalCost <= min)
                {
                    min = dist[v].totalCost;
                    min_index = v;
                }

            }



            return min_index;
        }

        // A utility function to print
        // the constructed distance array
        void PrintSolution(int[] dist)
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
        public void GSP(int[,] graph, int src)
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
                Path[] dist = dijkstra(graph, now);
                now = minDistance(dist, sptSet);
                path = GetPaths(path, dist, now,prev);
                sptSet[now] = true;
            }
            prev = now;
            Path[] distt = dijkstra(graph, now);
            List<Path> rpath = new List<Path>();
            rpath = GetPaths(rpath, distt, 0, prev);
            rpath.Reverse();
            path.AddRange(rpath);
            foreach (var item in path)
            {
                Debug.Log(item.current + "-");
            }
            //printSolution(path.ToArray());
        }
        Path[] dijkstra(int[,] graph, int src)
        {
            V = graph.GetLength(0);
            Path[] paths = new Path[V];
            int[] dist = new int[V]; 
            bool[] sptSet = new bool[V];

            for (int i = 0; i < V; i++)
            {
                paths[i] = new Path();
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            dist[src] = 0;

            // Find shortest path for all vertices
            for (int count = 0; count < V - 1; count++)
            {
                int u = minDistance(dist, sptSet);

                sptSet[u] = true;

                for (int v = 0; v < V; v++)
                {
                    if (!sptSet[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                    {
                        dist[v] = dist[u] + graph[u, v];
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
                Debug.Log("Destination : " + paths[i].destinaiton + " Cost : "+paths[i].totalCost +"\n");
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
        List<Path> GetPaths(List<Path> list, Path[] paths, int now,int prev)
        {
            if (now == prev)
                return list;
            list.Add(paths[now]);
            GetPaths(list, paths, paths[now].prev,prev);
            return list;
        }
    }