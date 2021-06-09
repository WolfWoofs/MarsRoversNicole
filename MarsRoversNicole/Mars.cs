using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversNicole
{
    class Mars
    {
        static int movementGrid_x;
        static int movementGrid_y;
        public void SetMovementGrid(int column, int row)
        {
            movementGrid_x = column;
            movementGrid_y = row;
        }

        /// <summary>
        /// Method used to move the rover. Takes in the starting x position, the starting y position, the starting rotation, and the string of instructions for movement.
        /// SetMovementGrid() is called before this method to establish the grid dimensions.
        /// </summary>
        /// <returns>Tuple<int, int, Rotation, bool>: roverlocation, roverrotation, successful movement (if the rover was successfully able to complete its movement in full)</returns>
        public Tuple<int, int, Rotation, bool> MoveRover(int startColumn, int startRow, Rotation startRotation, string movementInstructions) {
            MarsRover marsRoverCreated = new MarsRover(startColumn, startRow, startRotation);
            Tuple<int, int, Rotation> roverInformation;
            if (!marsRoverCreated.CalculateMovement(movementInstructions))
            {
                roverInformation = marsRoverCreated.getRoverCurrentPositionRotation();
                return new Tuple<int, int, Rotation, bool>(roverInformation.Item1, roverInformation.Item2, roverInformation.Item3, false);
            }
            roverInformation = marsRoverCreated.getRoverCurrentPositionRotation();
            return new Tuple<int, int, Rotation, bool>(roverInformation.Item1, roverInformation.Item2, roverInformation.Item3, true);
        }

        class MarsRover
        {
            int columnCurrentPosition;
            int rowCurrentPosition;
            Rotation currentRotation;
            public MarsRover(int columnPostion, int rowPositon, Rotation rotation)
            {
                columnCurrentPosition = columnPostion;
                rowCurrentPosition = rowPositon;
                currentRotation = rotation;
            }

            public Tuple<int, int, Rotation> getRoverCurrentPositionRotation()
            {
                return new Tuple<int, int, Rotation>(columnCurrentPosition, rowCurrentPosition, currentRotation);
            }
            Rotation getRoverCurrentRotation()
            {
                return currentRotation;
            }
            private bool setRoverCurrentPosition(int column, int row)
            {
                if (!validRoverPosition(column, row))
                {
                    return false;
                }
                columnCurrentPosition = column;
                rowCurrentPosition = row;
                return true;
            }
            private void setRoverCurrentRotation(int rotation)
            {
                currentRotation = (Rotation)rotation;
            }

            /// <summary>
            /// Compares the rover position to the grid. If the desired position would cause the rover to move off of the grid, it returns false.
            /// </summary>
            public bool validRoverPosition(int column, int row)
            {
                if (column > movementGrid_x || column < 0 || row > movementGrid_y || row < 0)
                {
                    return false;
                }
                return true;
            }
            /// <summary>
            /// Moves the rover based on the inputted string parameter.
            /// </summary>
            /// <returns>bool: was this movement successful in full</returns>
            public bool CalculateMovement(string movementInstructionString)
            {
                Tuple<int, int, Rotation> roverPosition = getRoverCurrentPositionRotation();
                if (!validRoverPosition(roverPosition.Item1, roverPosition.Item2))
                {
                    return false;
                }
                foreach (char instruction in movementInstructionString.Trim().ToLower())
                {

                    if (instruction == 'r')
                    {
                        if (getRoverCurrentRotation() == Rotation.W)
                        {
                            setRoverCurrentRotation(0);
                        }
                        else
                        {
                            setRoverCurrentRotation((int)getRoverCurrentRotation() + 1);
                        }
                    }
                    else if (instruction == 'l')
                    {
                        if (getRoverCurrentRotation() == Rotation.N)
                        {
                            setRoverCurrentRotation(3);
                        }
                        else
                        {
                            setRoverCurrentRotation((int)getRoverCurrentRotation() - 1);
                        }
                    }
                    else if (instruction == 'm')
                    {
                        bool movementSuccessful = false;
                        switch (roverPosition.Item3)
                        {
                            case Rotation.N:
                                movementSuccessful = setRoverCurrentPosition(roverPosition.Item1, roverPosition.Item2 + 1);
                                break;
                            case Rotation.E:
                                movementSuccessful = setRoverCurrentPosition(roverPosition.Item1 + 1, roverPosition.Item2);
                                break;
                            case Rotation.S:
                                movementSuccessful = setRoverCurrentPosition(roverPosition.Item1, roverPosition.Item2 - 1);
                                break;
                            case Rotation.W:
                                movementSuccessful = setRoverCurrentPosition(roverPosition.Item1 - 1, roverPosition.Item2);
                                break;
                        }
                        if (!movementSuccessful)
                        {
                            return false;
                        }
                    }
                    roverPosition = getRoverCurrentPositionRotation();
                   
                }
                return true;
            }






        }

       
    }

}


