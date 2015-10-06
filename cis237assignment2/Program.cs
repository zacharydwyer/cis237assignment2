using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237assignment2
{

    class Program
    {

        static void Main(string[] args)
        {

            // Ask user if they want the drawn effect.
            MazeSolver.applyDrawnEffect = !printNormally();

            // Starting Coords
            const int X_START = 1;
            const int Y_START = 1;
          
            // Create new Maze Solver
            MazeSolver mazeSolver = new MazeSolver();

            // Assign new maze
            char[,] maze1 = getNewMaze();

            // Tell maze solver to solve the given maze, then print it.
            mazeSolver.SolveMaze(maze1, X_START, Y_START);
            mazeSolver.PrintMaze();
            Console.WriteLine("Maze solved! (\"O\" indicates the successful path.) Press any key to solve the transposed version.");
            Console.ReadKey(true);

            // Create new maze solver
            mazeSolver = new MazeSolver();

            // Reset the maze.
            maze1 = getNewMaze();

            // Create the second maze by transposing the first maze
            char[,] maze2 = transposeMaze(maze1);

            // Solve the transposed maze
            mazeSolver.SolveMaze(maze2, X_START, Y_START);

            // Print the maze
            mazeSolver.PrintMaze();

            Console.WriteLine("Maze solved! (\"O\" indicates the successful path.) Press any key to continue.");
            Console.ReadKey(true);

        }
    
        // Transposes a given maze and returns it.
        static char[,] transposeMaze(char[,] mazeToTranspose)
        {

            // Create an array that will store the new transposed version of the given maze
            char[,] transposedVersion = new char[mazeToTranspose.GetLength(0), mazeToTranspose.GetLength(1)];

            for (int secondDimensionIndex = 0; secondDimensionIndex <= mazeToTranspose.GetUpperBound(1); secondDimensionIndex++)
            {
                for (int firstDimensionIndex = 0; firstDimensionIndex <= mazeToTranspose.GetUpperBound(0); firstDimensionIndex++)
                {
                    // Transpose positions
                    transposedVersion[secondDimensionIndex, firstDimensionIndex] = mazeToTranspose[firstDimensionIndex, secondDimensionIndex];
                }
            }

            return transposedVersion;
        }

        static char[,] getNewMaze()
        {
            char[,] maze = 
            { { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', '.', '.', '.', '#', '.', '.', '.', '.', '.', '.', '#' },
            { '#', '.', '#', '.', '#', '.', '#', '#', '#', '#', '.', '#' },
            { '#', '#', '#', '.', '#', '.', '.', '.', '.', '#', '.', '#' },
            { '#', '.', '.', '.', '.', '#', '#', '#', '.', '#', '.', '.' },
            { '#', '#', '#', '#', '.', '#', '.', '#', '.', '#', '.', '#' },
            { '#', '.', '.', '#', '.', '#', '.', '#', '.', '#', '.', '#' },
            { '#', '#', '.', '#', '.', '#', '.', '#', '.', '#', '.', '#' },
            { '#', '.', '.', '.', '.', '.', '.', '.', '.', '#', '.', '#' },
            { '#', '#', '#', '#', '#', '#', '.', '#', '#', '#', '.', '#' },
            { '#', '.', '.', '.', '.', '.', '.', '#', '.', '.', '.', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' } };

            return maze;
        }

        static bool printNormally()
        {
            Console.WriteLine("Clear screen every time maze updates for a \"drawn\" effect? (y/n)");

            bool printLikeNormal = true;

            bool keepRepeating = true;

            while (keepRepeating)
            {
                // Get key pressed 
                string key = Console.ReadKey(true).KeyChar.ToString();

                if (key != "y" && key != "n" && key != "Y" && key != "N")
                {
                    Console.WriteLine("Huh? Hit y or n.");
                    keepRepeating = true;
                }
                else
                {
                    if (key == "y" || key == "Y")
                    {
                        printLikeNormal = false;
                        keepRepeating = false;
                    }
                    else
                    {
                        if (key == "n" || key == "N")
                        {
                            printLikeNormal = true;
                            keepRepeating = false;
                        }
                    }
                }
            }

            return printLikeNormal;
            
        }
    }
}
