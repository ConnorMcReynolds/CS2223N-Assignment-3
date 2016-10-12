#!/bin/bash
#In the official documemtation the line above always has to be the first line of any script file.  But, students have 
#told me that script files work correctly without that first line.

#Author: Michelle Hernandez
#Course: CPSC223n
#Semester: Fall 2016
#Assignment: 1
#Due: September 10, 2016.

#This is a bash shell script to be used for compiling, linking, and executing the C sharp files of this assignment.
#Execute this file by navigating the terminal window to the folder where this file resides, and then enter the command: ./build.sh

#System requirements: 
#  A Linux system with BASH shell (in a terminal window).
#  The mono compiler must be installed.  If not installed run the command "sudo apt-get install mono-complete" without quotes.
#  The three source files and this script file must be in the same folder.
#  This file, build.sh, must have execute permission.  Go to the properties window of build.sh and put a check in the 
#  permission to execute box.

echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo Compile AnimatedBallLogic.cs to create the file: AnimatedBallLogic.dll
mcs -target:library -out:AnimatedBallLogic.dll AnimatedBallLogic.cs

echo Compile AnimatedBallFrame.cs to create the file:AnimatedBallFrame.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -r:AnimatedBallLogic.dll -out:AnimatedBallFrame.dll AnimatedBallFrame.cs

echo Compile AnimatedBallMain.cs and link the two previously created dll files to create an executable file. 
mcs -r:System -r:System.Windows.Forms -r:AnimatedBallFrame.dll -out:Animated_Ball.exe AnimatedBallMain.cs

echo View the list of files in the current folder
ls -l

echo Run the Assignment 1 program.
./Animated_Ball.exe

echo The script has terminated.











