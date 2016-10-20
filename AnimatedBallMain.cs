//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**
//Author: Connor McReynolds
//Author's email: cmcreynolds@scu.fullerton.edu
//Course: CPSC223N
//Assignment number: 3
//Due date: 10/22/2016 (mm/dd/yyyy)
//Date last updated: 10/22/2016 (mm/dd/yyyy)
//Source files in this program: AnimatedBallFrame.cs, AnimatedBallLogic.cs, AnimatedBallMain.cs
//Purpose of this entire program: Create and control an animated ball with collision detection
//Development status: Almost complete, runs
//Development platform: Bash using Mono on ubuntu
//Safe software seal of approval: No Microsoft products were used in the construction of this program.

//Name of this file: AnimatedBallLogic
//Purpose of this file: Launch the window showing the form where the graphical images will be displayed.

//
//The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. Twoanimatedobjectslogic.cs         compiles into library file Twoanimatedlogic.dll
//  2. Twoanimatedobjectsframe.cs         compiles into library file Twoobjectsframe.dll
//  3. Twoanimatedobjectsmain.cs     compiles and links with the two dll files above to create Twoobjects.exe

//
//Compile (& link) this file: 
//mcs Twoanimatedobjectsmain.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Twoobjectsframe.dll -out:Twoobjects.exe


using System;
using System.Windows.Forms;            //Needed for "Application" near the end of Main function.
public class Movingball
{  public static void Main()
   {  System.Console.WriteLine("The animated ball moving program will begin now.");
      Animatedballframe motionapplication = new Animatedballframe();
      Application.Run(motionapplication);
      System.Console.WriteLine("This animated program has ended.  Bye.");
   }//End of Main function
}//End of Movingballs class
