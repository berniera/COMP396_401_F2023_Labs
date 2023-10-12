using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Digraph
{
    //Nodes (or Vertices): Name [pos(x,y,z)]
    //Edges: Start Node, End Node [length]

    //Data structure to hold
    //Options:
    //Option1: Class for Node, class for Edge, List<Nodes>,List of Edges
    //Builder methods: add_vertex, add_edge

    //Option2:
    //AdjancyMatrix[N][N] where N is number of nodes

    //Option3:dictionary
    //AdjancyList representation

    //Option3.1:dictionary
    Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();
    Dictionary<char, int> distance = new Dictionary<char, int>();
    Dictionary<char, char> previous = new Dictionary<char, char>();
    public void add_vertex_Dijkstra(char vertex, Dictionary<char, int> neighbor_edges_dijkstra)
    {
        vertices[vertex] = neighbor_edges_dijkstra;
    }

    /// <summary>
    /// Get all neighbours of a node
    /// </summary>
    /// <param name="node">node - the node to get neighbors from</param>
    /// <returns> a list of nodes that are adjacent the the given node above</returns>
    public char[] GetNeighbors(char node)
    {
        char[] result = null;
        if (vertices.ContainsKey(node))
        {
            var keys = vertices[node].Keys;
            result = new char[vertices[node].Count];
            int i = 0;
            foreach (var key in keys)
            {
                result[i] = key;
                i++;
            }
        }
        return result;
    }

    public void print_array<T>(T[] array)
    {
        foreach (T v in array)
        {
            Debug.Log(v);
        }

    }

    public List<char> Find_Shortest_Path_via_Dijkstra_Algo(char start, char target)
    {
        List<char> path = new List<char>();
        initialize_single_source(start);
        List<char> Visited = new List<char>() { };
        List<char> Pending = new List<char>() { };
        foreach (char c in vertices.Keys)
        {
            Pending.Add(c);
        }

        //here starts the algorithm proper
        while (Pending.Count > 0)
        {
            char u = Extract_min(Pending);
            Visited.Add(u);
            //
            if (u == target)
            {
                //we have found the target,
                //construct the path and return it
                path.Add(u);
                while (previous[u] != '\0')
                {
                    u = previous[u];
                    path.Add(u);

                }
                path.Reverse();
                return path;
            }

            //check for no solution (no path)
            if (distance[u] == int.MaxValue)
            {
                return path; //Count 0  => indicates that no path exists between start and target
            }

            foreach (var v in vertices[u])
            {
                relax(u, v, v.Value);

            }
        }


        return path;
    }

    private void relax(char u, KeyValuePair<char, int> v, int value)
    {
        if (distance[v.Key] > distance[u] + value)
        {
            distance[v.Key] = distance[u] + value;
            previous[v.Key] = u;
        }

    }

    private char Extract_min(List<char> pending)
    {
        //we are simulating the behaviour of a Priority list with a regular list by sorting it first
        //Note: the O(...) is different

        pending.Sort((x, y) => distance[x].CompareTo(distance[y]));
        char u = pending[0];
        pending.RemoveAt(0);
        return u;
    }

    private void initialize_single_source(char start)
    {
        foreach (char k in vertices.Keys)
        {
            distance[k] = int.MaxValue; //instead of infinity
            previous[k] = '\0';
        }
        distance[start] = 0;

    }
}