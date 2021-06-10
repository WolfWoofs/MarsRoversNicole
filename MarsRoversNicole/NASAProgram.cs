using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoversNicole
{
    public class NASAProgram
    {
        static void Main(string[] args)
        {
            StartCommunications();
        }

        static List<string> roverLocations = new List<string>();
        static Tuple<int, int, Rotation, bool> newRoverLocation;
        static int gridX;
        static int gridY;
        static List<int> roverX = new List<int>();
        static List<int> roverY = new List<int>();
        static List<Rotation> roverRotation = new List<Rotation>();
        static List<string> movementInstructions = new List<string>();


        /// <summary>
        /// Handles resetting all variables needed for a new iteration.
        /// </summary>
        static void ResetVariables()
        {
            roverLocations = new List<string>();
            newRoverLocation = null;
            gridX = 0;
            gridY = 0;
            roverX = new List<int>();
            roverY = new List<int>();
            roverRotation = new List<Rotation>();
            movementInstructions = new List<string>();
        }

        static void StartCommunications()
        {
            Mars marsConnection = new Mars();
            try
            {
                startOver:
                ResetVariables();
                if (getGrid())
                {
                    marsConnection.SetMovementGrid(gridX, gridY);
                    if (newRover())
                    {
                        //All data inputs have been collect, time to calculate the movement of the rover per rover.
                        for (int roverCount = 0; roverCount < roverX.Count; roverCount++)
                        {
                            newRoverLocation = marsConnection.MoveRover(roverX[roverCount], roverY[roverCount], roverRotation[roverCount], movementInstructions[roverCount]);
                            //Item4 of the returned Tuple determines if the rover was successfully able to complete its movement. In other words, stopping it before it moves off of the grid.
                            if (!newRoverLocation.Item4)
                            {
                                Console.WriteLine("Rover was unable to move any further with the instructions given.");
                            }
                            roverLocations.Add(newRoverLocation.Item1 + " " + newRoverLocation.Item2 + " " + newRoverLocation.Item3);
                        }
                    }
                }

                foreach (var roverMovementLocations in roverLocations)
                {
                    Console.WriteLine(roverMovementLocations);
                }

                Console.WriteLine("--------");
                Console.WriteLine("Press Enter to begin anew. Press Escape to exit.");
                var nextInput = Console.ReadKey();
                if (nextInput.Key != ConsoleKey.Escape)
                {
                    goto startOver;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("An exception was thrown: " + err.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves user input for grid dimensions.
        /// </summary>
        /// <returns>bool: did user prompt to exit</returns>
        static bool getGrid()
        {
            try
            {
                bool continueCheck = false;
                while (!continueCheck)
                {
                    Console.Write("Coordinate grid dimensions (x y): ");
                    string gridDimensions = Console.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(gridDimensions))
                    {
                        Console.WriteLine("END");
                        return false;
                    }
                    string[] splited = gridDimensions.Split(" ");
                    if (splited.Length != 2 || gridDimensions.Any(char.IsLetter) || (splited.All(e => e.Equals("0"))))
                    {
                        Console.WriteLine("Invalid input. Please use format \"x y\"");
                        continueCheck = false;
                    }
                    else
                    {
                        gridX = Int32.Parse(splited[0]);
                        gridY = Int32.Parse(splited[1]);
                        continueCheck = true;
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("An exception was thrown at grid dimensions input: " + err.StackTrace);
                throw;
            }

            return true;
        }

        /// <summary>
        /// Retrieves user input for a new rover. 1. Rover starting position and 2. Movement instructions for that rover.
        /// </summary>
        /// <returns>bool: did user prompt to exit</returns>
        static bool newRover(string newString = "")
        {
            try
            {
                bool continueCheck = false;
                while (!continueCheck)
                {
                    string roverStartPosition = newString;
                    if (string.IsNullOrEmpty(newString))
                    {
                        Console.Write("Rover starting position (x y r): ");
                        roverStartPosition = Console.ReadLine().Trim().ToLower();
                    }

                    if (string.IsNullOrWhiteSpace(roverStartPosition))
                    {
                        Console.WriteLine("END");
                        return false;
                    }

                    string[] splited = roverStartPosition.Split(" ");
                    if (splited.Length != 3 || !(roverStartPosition.Contains("n") || roverStartPosition.Contains("e") || roverStartPosition.Contains("s") || roverStartPosition.Contains("w")))
                    {
                        Console.WriteLine("Invalid input. Please use format \"x y rotation\" with rotation as N, E, S, or W.");
                        continueCheck = false;
                        newString = "";
                    }else if (Int32.Parse(splited[0]) > gridX || Int32.Parse(splited[0]) < 0 || Int32.Parse(splited[1]) > gridY || Int32.Parse(splited[1]) < 0)
                    {
                        Console.WriteLine("Invalid input. Starting position of the rover must be within the grid dimensions.");
                        continueCheck = false;
                        newString = "";
                    }
                    else
                    {
                        roverX.Add(Int32.Parse(splited[0]));
                        roverY.Add(Int32.Parse(splited[1]));

                        switch (splited[2])
                        {
                            case "n":
                                roverRotation.Add(Rotation.N);
                                break;
                            case "e":
                                roverRotation.Add(Rotation.E);
                                break;
                            case "s":
                                roverRotation.Add(Rotation.S);
                                break;
                            case "w":
                                roverRotation.Add(Rotation.W);
                                break;
                        }
                        continueCheck = true;
                    }
                }

            }
            catch (Exception err)
            {
                Console.WriteLine("An exception was thrown at Rover start position and rotation: " + err.StackTrace);
                throw;
            }
            try
            {
                bool continueCheck = false;
                while (!continueCheck)
                {
                    Console.Write("Rover movement: ");
                    string roverInstructions = Console.ReadLine().Trim();
                    string[] splited = roverInstructions.Split(" ");
                    if (splited.Length != 1)
                    {
                        Console.WriteLine("Invalid input.");
                        continueCheck = false;
                    }
                    else
                    {
                        movementInstructions.Add(splited[0]);
                        continueCheck = true;
                    }
                }

            }
            catch (Exception err)
            {
                Console.WriteLine("An exception was thrown at Rover movement: " + err.StackTrace);
                throw;
            }
            //Finished creating a rover with starting location and rotation and movement instructions.
            Console.WriteLine("ROVER END");
            Console.Write("Rover starting position (x y r): ");
            string nextInput = Console.ReadLine().Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(nextInput))
            {
                newRover(nextInput);
            }
            return true;
        }
            
    }
}

