#!/bin/bash
#In the official documemtation the line above always has to be the first line of any script file.  But, students have 
#told me that script files work correctly without that first line.

#Author: Michelle Hernandez
#Course: CPSC223n
#Semester: Fall 2016
#Assignment: 1
#Due: September 10, 2018.

#This is a bash shell script to be used for compiling, linking, and executing the C sharp files of this assignment.
#Execute this file by navigating the terminal window to the folder where this file resides, and then enter the command: ./build.sh

#System requirements: 
#  A Linux system with BASH shell (usually in a terminal window).
#  The C# compiler must be installed.  If not installed run the command "sudo apt-get install mono-complete" without quotes.
#  The three source files and this script file must be in the same folder.
#  This file, build.sh, must have execute permission.  Go to the properties window of build.sh and put a check in the 
#  permission to execute box.

##Any student submitting homework with the professor's comments or no comments receives a low grade.  It is a requirement that
##you have to change the comments in file to reflect your own homework assignment.  Your name is not Michelle Hernandez: you fix
##the comments to be your own statements.

echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo Compile Twoanimatedobjectslogic.cs to create the file: Twoanimatedlogic.dll
mcs -target:library Twoanimatedobjectslogic.cs -r:System.Drawing.dll -out:Twoanimatedlogic.dll


echo Compile Twoanimatedobjectsframe.cs to create the file: Twoobjectsframe.dll
mcs -target:library Twoanimatedobjectsframe.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Twoanimatedlogic.dll -out:Twoobjectsframe.dll


echo Compile Twoanimatedobjectsmain.cs and link the two previously created dll files to create an executable file.
mcs Twoanimatedobjectsmain.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Twoobjectsframe.dll -out:Twoobjects.exe


echo View the list of files in the current folder
ls -l


echo Run the Bouncing Ball program
./Twoobjects.exe


echo The script has terminated.  Enjoy your output.












