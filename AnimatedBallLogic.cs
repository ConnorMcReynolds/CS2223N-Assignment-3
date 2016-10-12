//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**
//Author: Connor McReynolds
//Author's email: cmcreynolds@scu.fullerton.edu
//Course: CPSC223N
//Assignment number: 2
//Due date: 10/2/2017 (mm/dd/yyyy)
//Date last updated: 10/2/2017 (mm/dd/yyyy)
//Source files in this program: AnimatedBallFrame.cs, AnimatedBallLogic.cs, AnimatedBallMain.cs
//Purpose of this entire program: Create and control an animated ball
//Development status: Almost complete, runs
//Development platform: Bash using Mono on Xubuntu
//Safe software seal of approval: No Microsoft products were used in the construction of this program.

//Name of this file: AnimatedBallLogic
//Purpose of this file: Handles the math algorithms for movement of ball

//The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. Twoanimatedobjectslogic.cs         compiles into library file Twoanimatedlogic.dll
//  2. Twoanimatedobjectsframe.cs         compiles into library file Twoobjectsframe.dll
//  3. Twoanimatedobjectsmain.cs     compiles and links with the two dll files above to create Twoobjects.exe


//To compile Twoanimatedobjectslogic.cs:   
//          mcs -target:library Twoanimatedobjectslogic.cs -r:System.Drawing.dll -out:Twoanimatedlogic.dll 


public class Animatedballlogic
{   private System.Random randomgenerator = new System.Random();

    public double get_random_direction_for_a()
       {//This method returns a random angle in radians in the range: -Ï€/2 <= angle <= +Ï€/2
        double randomnumber = randomgenerator.NextDouble();
        randomnumber = randomnumber - 0.5;
        double ball_a_angle_radians = System.Math.PI * randomnumber;
        return ball_a_angle_radians;
       }


}//End of Animatedballlogic
