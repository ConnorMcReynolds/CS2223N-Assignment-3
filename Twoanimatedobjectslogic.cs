//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**
//
//Author: F. Holliday
//Author's email: holliday@fullerton.edu
//Course: CPSC223N
//Assignment number: 12
//Due date: 07/18/2018 (mm/dd/yyyy)
//Date last updated: 10/03/2016 (mm/dd/yyyy)
//Source files in this program: Twoanimatedobjectsmain.cs, Twoanimatedobjectsframe.cs, Twoanimatedobjectslogic.cs
//Purpose of this entire program:  Demonstrate how to animate two independent objects.
//This program shows two balls moving linearly in independent directions at speeds different from each other. 
//
//Development status.  This program is done.  It fulfills its purpose of teaching about animation.
//Here "program" means all three modules in the set, namely: Movingballs, Twoobjectsframe, Twoanimatedlogic
//
//Mame of this file: Twoanimatedobjectslogic.cs
//Purpose of this file: Perform algorithm operations in support of the two flying balls displayed in a graphic area.
//To 223N class: Generally it is good software design to separate different kinds of functionality in to distinct files of source
//code.  This partitioning of the source code is good for understanding the original product as well as for maintaining the 
//product.  In this specific software project it was hard to identify algorithms to be placed in this class of business algorithms.
//Nevertheless, in keeping with the rules of partitioning the source there are two simple methods to place in this class of
//business logic.
//Date last modified: October 3, 2016
//
//
//The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. Twoanimatedobjectslogic.cs         compiles into library file Twoanimatedlogic.dll
//  2. Twoanimatedobjectsframe.cs         compiles into library file Twoobjectsframe.dll
//  3. Twoanimatedobjectsmain.cs     compiles and links with the two dll files above to create Twoobjects.exe
//
//
//To compile Twoanimatedobjectslogic.cs:   
//          mcs -target:library Twoanimatedobjectslogic.cs -r:System.Drawing.dll -out:Twoanimatedlogic.dll 
//
//


public class Twoanimatedlogic
{   private System.Random randomgenerator = new System.Random();

    public double get_random_direction_for_a()
       {//This method returns a random angle in radians in the range: -Ï€/2 <= angle <= +Ï€/2
        double randomnumber = randomgenerator.NextDouble();
        randomnumber = randomnumber - 0.5;
        double ball_a_angle_radians = System.Math.PI * randomnumber;
        return ball_a_angle_radians;
       }//End of computefibonaccinumber

    public double get_random_direction_for_b()
       {//This method returns a random angle in radians in the range: +Ï€/2 <= angle <= +3*Ï€/2
        double randomnumber = randomgenerator.NextDouble();
        randomnumber = randomnumber + 0.5;
        double ball_b_angle_radians = System.Math.PI * randomnumber;
        return ball_b_angle_radians;
       }//End of computefibonaccinumber

}//End of Twoanimatedlogic
