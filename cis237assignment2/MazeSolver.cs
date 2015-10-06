using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace cis237assignment2
{
    /// <summary>
    /// This class is used for solving a char array maze.
    /// You might want to add other methods to help you out.
    /// A print maze method would be very useful, and probably neccessary to print the solution.
    /// If you are real ambitious, you could make a seperate class to handle that.
    /// </summary>
    class MazeSolver
    {
        public static bool applyDrawnEffect = false;        // This is set in Program.cs.

        /// <summary>
        /// Class level member variable for the mazesolver class
        /// </summary>
        char[,] maze;
        int xStart;
        int yStart;
        bool mazeIsSolved = false;

        /// <summary>
        /// Default Constuctor to setup a new maze solver.
        /// </summary>
        public MazeSolver()
        {}


        /// <summary>
        /// This is the public method that will allow someone to use this class to solve the maze.
        /// Feel free to change the return type, or add more parameters if you like, but it can be done
        /// exactly as it is here without adding anything other than code in the body.
        /// </summary>
        public void SolveMaze(char[,] maze, int xStart, int yStart)
        {
            // Assign class variables
            this.maze = maze;
            this.xStart = xStart;
            this.yStart = yStart;

            // Use these to traverse the maze.
            mazeTraversal(xStart, yStart, xStart, yStart);              // Recursive function
        }


        // Marks the called spot as "?" meaning that it was traversed but not yet sure if the space is part of the successful path.
        // If the mazeTraversal method lands on a spot where the maze would be considered solved, it is then considered solved.
        // If the maze is solved, it will no longer search for new spots and will instead mark the current spot with a 'O'
        //      meaning the spot is part of the successful path, and will then remove itself from the stack.
        private void mazeTraversal(int currentX, int currentY, int lastX, int lastY)
        {
            // Mark this new, current spot as "traversed but not known if viable" with a ? mark
            maze[currentX, currentY] = '?';

            // Print the maze
            PrintMaze();

            // Is our current coordinate the winning one (at the edge)? Dimension 0 represents x, dimension 1 represents y.
            if (currentX == 0 || currentX == maze.GetUpperBound(0) || currentY == 0 || currentY == maze.GetUpperBound(1))
            {
                mazeIsSolved = true;
            }
            
            // Is the space on the left okay to move to?
            if (isSpaceTraversable(currentX - 1, currentY, lastX, lastY))
            {
                // Move to the new spot, and tell mazeTraversal the spot they came from is the current spot we are in right now, pre method call 
                mazeTraversal(currentX - 1, currentY, currentX, currentY);
            }

            // Is the space above okay to move to?
            if (isSpaceTraversable(currentX, currentY + 1, lastX, lastY)) 
            {
                mazeTraversal(currentX, currentY + 1, currentX, currentY);
            }

            // Is the space on the right okay to move to?
            if (isSpaceTraversable(currentX + 1, currentY, lastX, lastY)) 
            {
                mazeTraversal(currentX + 1, currentY, currentX, currentY);
            }

            // Is the space below okay to move to?
            if (isSpaceTraversable(currentX, currentY - 1, lastX, lastY)) 
            {
                mazeTraversal(currentX, currentY - 1, currentX, currentY);
            }

            // Is our current coordinate the winning one (at the edge)? Dimension 0 represents x, dimension 1 represents y.
            if (currentX == 0 || currentX == maze.GetUpperBound(0) || currentY == 0 || currentY == maze.GetUpperBound(1))
            {
                mazeIsSolved = true;
            }

            // Mark current spot as "part of the path that is the solution" if maze is solved
            if (mazeIsSolved)
            {
                maze[currentX, currentY] = 'O';
            }
            else
            {
                maze[currentX, currentY] = 'X';
            }

            // Here is where the method ends and hands off control to the method that called it.
            //      The method that called this method could be anywhere in the midst of these if statements.
        }

        // Some things to ask before moving to a new space:
        //  - Have we already solved the maze? If so, no, don't move.
        //  - Did we just come from that space? If so, don't move to that space.
        //  - Okay, so finally, is the space a "." (meaning it is open)? If so, then yes, the space is traversable.
        private bool isSpaceTraversable(int destinationX, int destinationY, int previousX, int previousY)
        {
            bool spaceIsTraversable = false;

            // is the maze solved?
            if (!mazeIsSolved)
            {
                // is the destination and the previous location the same?
               if (! (destinationX == previousX && destinationY == previousY))
                {
                    // alright, so finally, is that space open? (a ".")
                    if (maze[destinationX, destinationY] == '.')
                    {
                        // Space is traversable!
                        spaceIsTraversable = true;
                    }
                    else
                    {
                        // Space isn't traversable.
                        spaceIsTraversable = false;
                    }
                }
                else
                {
                    // Space isn't traversable.
                    spaceIsTraversable = false;
                }
            }
            else
            {
                // Space isn't traversable.
                spaceIsTraversable = false;
            }

            // Note: I know there's a ton of redundant code in this method but it was more for my own facilitation of
            //  understanding what exactly is being checked

            return spaceIsTraversable;
        }

        // Drawn effect applies here. Prints the maze out.
        public void PrintMaze()
        {
            if (applyDrawnEffect)
            {
                Console.Clear();
            }
            // Reference for the first dimension, the array that contains the array
            for (int firstIndex = 0; firstIndex <= maze.GetUpperBound(0); firstIndex++)
            {
                // Actually get the values here
                for (int secondIndex = 0; secondIndex <= maze.GetUpperBound(1); secondIndex++)
                {

                    // Write the maze character. If it is a "O" then change the background of the terminal to highlight the path.
                    if (maze[firstIndex, secondIndex] == 'O')
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(maze[firstIndex, secondIndex]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(maze[firstIndex, secondIndex] + " ");
                    }
                }

                // Go to a new line
                Console.WriteLine();
            }

            // Add two spaces after each PrintMaze
            Console.WriteLine(Environment.NewLine + Environment.NewLine);
        }
    }
}
