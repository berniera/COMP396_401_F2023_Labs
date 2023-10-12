using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDigraph : MonoBehaviour
{
    Digraph mygraph;
    Digraph CreateGraph1()
    {
        mygraph = new Digraph();

        mygraph.add_vertex_Dijkstra('A', new Dictionary<char, int>() { { 'B', 10 }, { 'C', 12 }, { 'D', 6 }, { 'E', 2 } });
        mygraph.add_vertex_Dijkstra('B', new Dictionary<char, int>() { { 'C', 2 }, { 'D', 4 }, { 'F', 5 } });
        mygraph.add_vertex_Dijkstra('C', new Dictionary<char, int>() { { 'B', 6 }, { 'F', 2 } });
        mygraph.add_vertex_Dijkstra('D', new Dictionary<char, int>() { { 'B', 3 }, { 'E', 3 } });
        mygraph.add_vertex_Dijkstra('E', new Dictionary<char, int>() { { 'D', 3 }, { 'F', 11 } });
        mygraph.add_vertex_Dijkstra('F', new Dictionary<char, int>() { });
        return mygraph;

    }
    Digraph CreateGraph2()
    {
        mygraph = new Digraph();
        //TODO: Create the graph in https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
        //Note: This is not a digraph (directed graph).
        //To make it a digraph, you have to add both edges;
        //f.e. from a -> 6 with length 14 and from 6 to a with also length 14


        return mygraph;

    }
    // Start is called before the first frame update
    void Start()
    {
        mygraph = CreateGraph1();

        char[] neighbors = mygraph.GetNeighbors('A');
        print("Neighbors of A:" + neighbors);
        mygraph.print_array<char>(neighbors);

        List<char> path = mygraph.Find_Shortest_Path_via_Dijkstra_Algo('A', 'F');
        mygraph.print_array(path.ToArray());
    }





    // Update is called once per frame
    void Update()
    {

    }
}
