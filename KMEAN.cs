using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KmeanAlgorithm
{
    class KMEAN
    {
        int []xAxis;
        int []yAxis;
        int []G;
        double[] prevClusterCenterX;
        double[] prevClusterCenterY;
       double[] clusterCenterX;
       double[] clusterCenterY;
        int kAmount = 0; // how many clusters
        int nAmount = 0; // how many x,y values
        public KMEAN(int []x, int[] y, int n, int k)// x axis, y axis, cluster amount
        {
            set_values(x, y, n , k);
        }
        public void set_values(int []x, int []y, int n, int k) //set the x and y axis arrays
        {
            xAxis = x;
            yAxis = y;
            nAmount = n;
            kAmount = k;
            clusterCenterX = new double[k]; //sets array size
            clusterCenterY = new double[k];
            prevClusterCenterX = new double[k];
            prevClusterCenterY= new double[k];
            G = new int[nAmount];
            
        }
        private void execute_algorithm(int method)
        {
            // if method = 0 then it is euclidean
            // if method = 1 then it is Manhattan
          
            //This algorithm will select the first clusters by choosing the first ones
            for (int i = 0; i < kAmount; i++)
            {
                clusterCenterX[i] = xAxis[i];
                clusterCenterY[i] = yAxis[i];
            }

            if (recursive_iterate(method, 0))
            {
               // System.Console.WriteLine("Algorithm Complete");
                //foreach (int gval in G)
               // {
               //     System.Console.WriteLine(gval);
              //  }
            }
            /*
             //To test the output of the cluster center
             foreach(double x in clusterCenterX)
                System.Console.WriteLine(x);
            foreach (double x in clusterCenterY)
                System.Console.WriteLine(x);*/ 
           
        }
        public int[] GetClusters(int method)
        {
            int[] gOutput = G;
            //execute
            execute_algorithm(method);

            for (int i = 0; i < nAmount; i++) //add a one to each cluster
                gOutput[i] = gOutput[i] + 1;
            return gOutput;

        }
        private bool recursive_iterate(int method, int iteration)
        {
            //use xAxis and yAxis to determine the distances
            //create a 2D distance list
            double [,] D = new double[kAmount,nAmount];

            //create the D matrix in manhattan or euclidean values
            for (int row = 0; row < kAmount; row++)// row corresponds the cluster #
            {
                for (int col = 0; col < nAmount; col++)
                {
                    if (method == 1)
                    {
                        D[row, col] = calc_manhattan(clusterCenterX[row], clusterCenterY[row], xAxis[col], yAxis[col]);
                    }
                    else
                    {
                        D[row, col] = calc_euclidean(clusterCenterX[row], clusterCenterY[row], xAxis[col], yAxis[col]);
                    }
                }
            }

            // find g values
            for (int col = 0; col < nAmount; col++) // iterate through col
            {
                double min = D[0, col];
                int lowkVal = 0;
                for (int row = 0; row < kAmount; row++) // go through the row and find the minimum number
                {
                    if (min > D[row, col])
                    {
                        min = D[row, col];
                        lowkVal = row; // the smallest cluster center #
                    }
                }
                G[col] = lowkVal;
      
            }

            bool loop = false;
            update_cluster();

            if (iteration == 0) //first iteration
            {
                return recursive_iterate(method, ++iteration);
               // return true;
            }
            else
            { // after the first iteration
                
                int gtempVal = 0;
                foreach (double xVal in prevClusterCenterX) //checks if the previous G equals the current G
                {
                    if (xVal != clusterCenterX[gtempVal])
                        loop = true;
                    ++gtempVal;
                }
                gtempVal = 0;
                foreach (double yVal in prevClusterCenterY) //checks if the previous G equals the current G
                {
                    if (yVal != clusterCenterY[gtempVal])
                        loop = true;
                    ++gtempVal;
                }

                if (loop == true)
                {

                    System.Console.WriteLine("Iteration");
                    return recursive_iterate(method, ++iteration);
                }
                else
                {
                    return true; // done with recursive function
                }
            }

        }
        private double calc_manhattan(double x1, double y1, double x2, double y2) //calculates the manhattan distance of two xy points
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
        private double calc_euclidean(double x1, double y1, double x2, double y2) //calculates the euclidean distance of two xy points
        {
            return Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
        }
        private void update_cluster() //uses G list to update the list cluster
        {
          
            //copy the current one into the previous  arrays (will be compared later
            prevClusterCenterX = clusterCenterX;
            prevClusterCenterY = clusterCenterY;

            int index = 0;
            double [,] matrix = new double[3,kAmount]; // row 1 is x axis row 2 is y axis row 3 is amount added
            foreach (int clusterNum in G)// for each value in G
            {
                //System.Console.WriteLine(clusterNum);
                matrix[0, clusterNum] = matrix[0, clusterNum] + xAxis[index]; // adds the clusterX value to the according clusters
                matrix[1, clusterNum] = matrix[1, clusterNum] + yAxis[index];
                matrix[2, clusterNum] = matrix[2, clusterNum] + 1;
                ++index;
            }

            for (int i = 0; i < kAmount; i++)
            {
                matrix[0, i] = matrix[0, i] / matrix[2, i];
                matrix[1, i] = matrix[1, i] / matrix[2, i];// get the cluster average

                clusterCenterX[i] = matrix[0, i]; // put the new cluster centers into the class variables
                clusterCenterY[i] =matrix[1, i];
            }

        }

    }
}
