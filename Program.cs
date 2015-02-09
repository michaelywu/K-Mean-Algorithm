using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KmeanAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] xAxis = new int[8]{2, 2, 8, 5, 7, 6, 1, 4};
            int[] yAxis = new int[8]{10, 5, 4, 8, 5, 4, 2, 9};

            KMEAN algorithm = new KMEAN(xAxis, yAxis,8,3); //3 for k

            //returns a k-array size
            int []G = algorithm.GetClusters(1);// 1 = manhatten, 0 == euclidean

            System.Console.WriteLine("The first algorithm will have these corresponding cluster sets: ");
            foreach (int gval in G)
                System.Console.WriteLine(gval);

            int[] xAxis1 = new int[4] { 1,2,4,5 };
            int[] yAxis1 = new int[4] { 1,1,3,4};

            KMEAN algorithm1 = new KMEAN(xAxis1, yAxis1, 4, 2); //2 clusters and 4 points

            int[] G1 = algorithm1.GetClusters(0);// 1 = manhatten, 0 == euclidean

            System.Console.WriteLine("The first algorithm will have these corresponding cluster sets: ");
            foreach (int gval in G1)
                System.Console.WriteLine(gval);

            int[] xAxis2 = new int[20] { 65,73,59,61,75,67,68,70,62,66,77,75,74,70,61,58,66,59,68,61 };
            int[] yAxis2 = new int[20] { 220,160,110,120,150,240,230,220,130,210,190,180,170,210,110,100,230,120,210,130};

            KMEAN algorithm2 = new KMEAN(xAxis2, yAxis2, 20, 3); //2 clusters and 4 points

            int[] G2 = algorithm2.GetClusters(0);// 1 = manhatten, 0 == euclidean

            System.Console.WriteLine("The third algorithm will have these corresponding cluster sets: ");
            foreach (int gval in G2)
                System.Console.WriteLine(gval);
        }
    }
}
