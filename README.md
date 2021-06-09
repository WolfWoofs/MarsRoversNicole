# MarsRoversNicole
Code Submission for DealerOn Software Engineer Position - Mars Rovers
-Nicole Christian 6/10/2021

The approach I took for this project was to separate it into two major parts - 1 for Mars (which includes the Mars Rover) and 1 for NASA/Earth. I assumed that the NASA program would be sending inputted instructions to the Mars counterpart.
In the NASA/Earth portion, all inputs are gathered from the user and sent to the Mars file where it will create the dimensions of the grid and Mars Rover object with a starting position, rotation and a given string of movement instructions.
Within the Mars portion, a Calculate Movement function is called per the Mars Rover object. From there the instructions passed through from NASA are read. L & R designating rotation movement and M to step forward 1 position.
Before the move is completed, it is validated that this move is a valid action based on the location of the rover, the rotation to move, and the size of the movement grid. If the move is valid, it proceeds to take that action and reads the next character input.
If the move is invalid, it breaks from it's movement and returns back to NASA reporting that it was unable to continue the full move instructions if was given for that rover.
Once the entire instruction string has been read and executed, the Mars portion replies back to NASA the current location and rotation of all rover/s.

How to use:
A minimum of 3 inputs are needed to run one rover.
1. Grid dimensions
2. A rover's starting position and rotation
3. The rover's movement instructions

Once the current rover's instructions are inputted, number 2 and 3 is repeated until no starting position is given. (Press Enter on rover's starting position prompt.)
If you are done entering in new rovers by pressing Enter at the starting position prompt, the calculation of movement will begin and the new positions of the rovers will be printed. At this time, pressing Enter will restart the prompts back to 1., allowing you to put in new grid dimensions.
