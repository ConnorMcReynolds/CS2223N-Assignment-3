//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**
//To the 223N class:  This program is intended to teach a few basic concepts of elementary animation.  This example uses three
//clocks each running at different rates.  Clock rates are measured in Herz (Hz).  One clock controls the graphical refresh
//rate.  Another clock controls the update of the coordinates of the ball traveling east.  The third clock controls the
//coordinates of the clock traveling west.  Distinct clocks are needed because the two balls travel at different speeds. 


//Author: F. Holliday
//Author's email: holliday@fullerton.edu
//Course: CPSC223N
//Assignment number: 12
//Due date: 07/18/2017 (mm/dd/yyyy)
//Date last updated: 10/03/2016 (mm/dd/yyyy)
//Source files in this program: Twoanimatedobjectsmain.cs, Twoanimatedobjectsframe.cs, Twoanimatedobjectslogic.cs
//Purpose of this entire program:  Demonstrate how to animate two independent objects.
//This program shows two balls moving linearly in independent directions at speeds different from each other. 
//
//Development status.  This program is done.  It fulfills its purpose of teaching about animation.
//Here "program" means all three modules in the set, namely: Movingballs, Twoobjectsframe, Twoanimatedlogic
//
//Mame of this file: Twoanimatedobjectsmain.cs
//Purpose of this file: Launch the window showing the form where the graphical images will be displayed area.
//
//
//The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. Twoanimatedobjectslogic.cs         compiles into library file Twoanimatedlogic.dll
//  2. Twoanimatedobjectsframe.cs         compiles into library file Twoobjectsframe.dll
//  3. Twoanimatedobjectsmain.cs     compiles and links with the two dll files above to create Twoobjects.exe

//
//Compile (& link) this file: 
//mcs Twoanimatedobjectsmain.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Twoobjectsframe.dll -out:Twoobjects.exe

//Hardcopy: this source code is best viewed in 7 point monospaced font using portrait orientation.




using System;
using System.Windows.Forms;            //Needed for "Application" near the end of Main function.
public class Movingballs
{  public static void Main()
   {  System.Console.WriteLine("The animated ball moving program will begin now.");
      Twoobjectsframe motionapplication = new Twoobjectsframe();
      Application.Run(motionapplication);
      System.Console.WriteLine("This animated program has ended.  Bye.");
   }//End of Main function
}//End of Movingballs class
