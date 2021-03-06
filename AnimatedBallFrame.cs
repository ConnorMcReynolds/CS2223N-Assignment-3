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

//Name of this file: AnimatedBallFrame
//Purpose of this file: Show the graphical area. Handles most functions



//The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. Twoanimatedobjectslogic.cs         compiles into library file Twoanimatedlogic.dll
//  2. Twoanimatedobjectsframe.cs         compiles into library file Twoobjectsframe.dll
//  3. Twoanimatedobjectsmain.cs     compiles and links with the two dll files above to create Twoobjects.exe

//Compile this file: 
//mcs -target:library AnimatedBallFrame.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:AnimatedBallLogic.dll -out:AnimatedBallFrame.dll

using System.Drawing;
using System.Windows.Forms;
using System;
using System.Timers;

public class Animatedballframe : Form
{  private const int formwidth = 1920;
   private const int formheight = 1080;
   
   // Textbox and Labels
   Label title_label = new Label();
   Label x_label = new Label();
   Label y_label = new Label();
   Label wall_collisions_title = new Label();
   Label wall_collisions = new Label();
   TextBox user_degreesTB = new TextBox();
   
   // Buttons
   private Button exit_button = new Button();
   private Point exit_button_location = new Point(1720,930);
   private Button start_button = new Button();
   private Point start_button_location = new Point(1470, 930);
   
   
   //These radiuses are used as a referenece for the real radius of the ball
   private const int ball_a_radius = 16;
   
   
   //The next number is the linear distance ball a will move each time its controlling clock tics.  A large number means faster
   //motion and more jerkiness.  A smaller number means less speed but smooth motion.
   
   //distance the ball moves every refresh (ms). Distance = Pixels
   private const double ball_a_distance_moved_per_refresh = 2.0;  //The unit of measure is 1 pixel.
  
   
   //Declaring used variables for Ball A
   private double ball_a_real_coord_x = (double)(formwidth/2 - ball_a_radius);
   private double ball_a_real_coord_y = (double)(formheight/2 - ball_a_radius);
   private double collision_ball_real_coord_x = (double)1500;
   private double collision_ball_real_coord_y = (double)400;
   //We do all our math with doubles, then at the end convert to integers
   private int ball_a_int_coord_x;  //The x-coordinate of ball a.
   private int ball_a_int_coord_y;  //The y-coordinate of ball a.
    private double ball_a_horizontal_delta;
   private double ball_a_vertical_delta;
   private double ball_a_angle_radians;
   
   // collision ball points
   private int collision_ball_int_coord_x;
   private int collision_ball_int_coord_y;
   
  
   private bool start_clicked = false;
   
   // counter for wall collisions
   private int wallCollisions = 0;
   
   //Clock declarations. 30.0 Hz = constant refresh rate during the execution of this animated program.
   private const double graphicrefreshrate = 60.0;
   private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
   private const double ball_a_update_rate = 60.0;  //Units are Hz
   private static System.Timers.Timer ball_a_control_clock = new System.Timers.Timer();
   private bool ball_a_clock_active = false;  //Initial state: The clock controll ball a is not active. ////////// Create button to pause/start clock (handless both bools)
   
   public Animatedballframe()   //The constructor of this class
   { 
	   
		//creates smooth animation
		DoubleBuffered  = true;
   
      Text = "Animated Ball";
      System.Console.WriteLine("formwidth = {0}. formheight = {1}.",formwidth,formheight);
      //Set the initial size of this form
      Size = new Size(formwidth,formheight);
      BackColor = Color.Green;
   
   wall_collisions.Location = new Point(300,963);
   wall_collisions.Size = new Size(200,33);
   wall_collisions.BackColor = Color.White;
   wall_collisions.ForeColor = Color.Black;
   wall_collisions.Font = new Font("georgia", 18);
   wall_collisions.Text = wallCollisions.ToString();
 
   wall_collisions_title.Location = new Point(300,930);
   wall_collisions_title.Size = new Size(200,33);
   wall_collisions_title.BackColor = Color.White;
   wall_collisions_title.ForeColor = Color.Black;
   wall_collisions_title.Font = new Font("Georgia", 18);
   wall_collisions_title.Text = "Collision count:";
   
   user_degreesTB.Text = "Enter degrees: ";
   user_degreesTB.Size = new Size(200,66);
   user_degreesTB.Location = new Point(0,930);
   user_degreesTB.BackColor = Color.White;
   user_degreesTB.ForeColor = Color.Black;
   user_degreesTB.Font = new Font("Georgia", 14);
        
   exit_button.Text = "Exit";
   exit_button.Size = new Size(200,66);
   exit_button.Location = exit_button_location;
   exit_button.BackColor = Color.Red;
   exit_button.Font = new Font("Georgia", 18);
   
   start_button.Text = "Start";
   start_button.Size = new Size(200,66);
   start_button.Location = start_button_location;
   start_button.BackColor = Color.LightGreen;
   start_button.Font = new Font("Georgia", 18);
      
   x_label.Location = new Point(820, 930);
   x_label.Size = new Size(200,66);
   x_label.BackColor = Color.White;
   x_label.ForeColor = Color.Black;
   x_label.BorderStyle = BorderStyle.FixedSingle;
   x_label.Font = new Font("Georgia", 14);
   x_label.Text = "X coordinates of center of ball: xx";
   
   y_label.Location = new Point(1070,930);
   y_label.Height = 66;
   y_label.Width = 200;
   y_label.BackColor = Color.White;
   y_label.ForeColor = Color.Black;
   y_label.BorderStyle = BorderStyle.FixedSingle;
   y_label.Font = new Font("Georgia", 14);
   y_label.Text = "Y coordinates of center of ball: xx";
      
   title_label.Location = new Point(0,0);
   title_label.Height = 50;
   title_label.Width = 1920;
   title_label.BackColor = Color.Blue;
   title_label.ForeColor = Color.Black;
   title_label.BorderStyle = BorderStyle.FixedSingle;
   title_label.Font = new Font("Georgia", 18);
   title_label.Text = "Animation by Connor McReynolds";
   
   
      //Set the initial coordinates of ball a.
      ball_a_int_coord_x = (int)(ball_a_real_coord_x);
      ball_a_int_coord_y = (int)(ball_a_real_coord_y);
      System.Console.WriteLine("Initial coordinates: ball_a_int_coord_x = {0}. ball_a_int_coord_y = {1}.",
                               ball_a_int_coord_x,ball_a_int_coord_y);
                               
      //Set initial coord. of collosion ball
      collision_ball_int_coord_x = (int) collision_ball_real_coord_x;
      collision_ball_int_coord_y = (int) collision_ball_real_coord_y;
    
      //Instantiate the collection of supporting algorithms
      Animatedballlogic algorithms = new Animatedballlogic();
      //Set a random angle of direction for ball a: -90.0 degrees <= ball_a_angle <= +90.0 degrees
      ball_a_angle_radians = algorithms.get_random_direction_for_a();
      System.Console.WriteLine("Direction of ball a = {0} degrees",ball_a_angle_radians*180.0/System.Math.PI);
      ball_a_horizontal_delta = ball_a_distance_moved_per_refresh*System.Math.Cos(ball_a_angle_radians);
      ball_a_vertical_delta = ball_a_distance_moved_per_refresh*System.Math.Sin(ball_a_angle_radians);

      graphic_area_refresh_clock.Enabled = false;  //Initially the clock controlling the rate of updating the display is stopped.
      //The next statement tells the clock what method to perform each time the clock makes a tic.
      graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Updatedisplay);  

      //Initialize clock of ball a.
      ball_a_control_clock.Enabled = false; //Initially the clock controlling ball a is stopped.
      //The next statement tells the clock what method to perform each time the clock makes a tick.
      ball_a_control_clock.Elapsed += new ElapsedEventHandler(Updateballa);

      Controls.Add(title_label);
      Controls.Add(x_label);
      Controls.Add(y_label);
      Controls.Add(exit_button);
      Controls.Add(start_button);
      Controls.Add(user_degreesTB);
      Controls.Add(wall_collisions);
      Controls.Add(wall_collisions_title);

      Startgraphicclock(graphicrefreshrate);  //refreshrate is how many times per second the display area is re-painted.
      Startballaclock(ball_a_update_rate);    //Set the animation rate for ball a. /////////////////////////////////////////////////////////////////
  
  
      exit_button.Click += new EventHandler(ExitProgram);
      start_button.Click += new EventHandler(StartAnimation);
   }//End of constructor

   protected override void OnPaint(PaintEventArgs ee)
   {  Graphics graph = ee.Graphics;
      graph.FillEllipse(Brushes.Yellow,ball_a_int_coord_x,ball_a_int_coord_y,2*ball_a_radius,2*ball_a_radius);
      graph.FillEllipse(Brushes.Red, collision_ball_int_coord_x, collision_ball_int_coord_y, 100, 200);
      graph.FillRectangle(Brushes.LightBlue, new Rectangle(0,880,1920,200));
      //The next statement looks like recursion, but it really is not recursion.
      //In fact, it calls the method with the same name located in the super class.
      base.OnPaint(ee);
   }

   protected void Startgraphicclock(double refreshrate)
   {   
	   if(start_clicked = true)
	   {
	   double elapsedtimebetweentics;
       if(refreshrate < 1.0) refreshrate = 1.0;  //Avoid dividing by a number close to zero.
       elapsedtimebetweentics = 1000.0/refreshrate;  //elapsedtimebetweentics has units milliseconds.
       graphic_area_refresh_clock.Interval = (int)System.Math.Round(elapsedtimebetweentics);
       graphic_area_refresh_clock.Enabled = true;  //Start clock ticking.
   }
   }

   protected void Startballaclock(double updaterate)
   {   
	   if(start_clicked = true)
	   {
	   double elapsedtimebetweenballmoves;
       if(updaterate < 1.0) updaterate = 1.0;  //This program does not allow updates slower than 1 Hz.
       elapsedtimebetweenballmoves = 1000.0/updaterate;  //1000.0ms = 1second.  elapsedtimebetweenballmoves has units milliseconds.
       ball_a_control_clock.Interval = (int)System.Math.Round(elapsedtimebetweenballmoves);
       ball_a_control_clock.Enabled = true;   //Start clock ticking.
       ball_a_clock_active = true;
   }
   }
   
   protected void Stopballaclock(double updaterate)
   {ball_a_control_clock.Enabled = false;
	   ball_a_clock_active = false;
   }
   
   protected void Countwallcollisions(int collisions)
   {  collisions = 0;
	   
	    if(ball_a_int_coord_x<=0) //Ball a has collided with the left wall
	    {
         collisions++;
         wallCollisions++;
         }
      else if(ball_a_int_coord_y<=50) //Ball a has collided with the top wall
      {
         collisions++;
         wallCollisions++;
         }
      else if(ball_a_int_coord_x+2*ball_a_radius>=formwidth-10) //Ball a has collided with the right wall
      {
         collisions++;
         wallCollisions++;
         }
      else if(ball_a_int_coord_y+2*ball_a_radius>=880) //Ball a has collided with the lower wall
      {
        collisions++;
        wallCollisions++;
         }
         
	
	//return collisions;
}

   protected void Updatedisplay(System.Object sender, ElapsedEventArgs evt)
   {  Invalidate();  //Weird: This creates an artificial event so that the graphic area will repaint itself.
      //System.Console.WriteLine("The clock ticked and the time is {0}", evt.SignalTime();  //Debug statement; remove it later.
      if(!(ball_a_clock_active))
          {graphic_area_refresh_clock.Enabled = false;
           System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
          }
   }
   
   

   protected void Updateballa(System.Object sender, ElapsedEventArgs evt)
   {  ball_a_real_coord_x = ball_a_real_coord_x + ball_a_horizontal_delta;
      //In the next statement the minus sign is used because the y-axis is upside down relative to the standard cartesian 
      //coordinate system.
      ball_a_real_coord_y = ball_a_real_coord_y - ball_a_vertical_delta;  
      ball_a_int_coord_x = (int)System.Math.Round(ball_a_real_coord_x);
      ball_a_int_coord_y = (int)System.Math.Round(ball_a_real_coord_y);
      //System.Console.WriteLine("The clock ticked for ball a and the time is {0}", evt.SignalTime);//Debug statement; remove later.
      
		 
		   if(ball_a_int_coord_x<=0) //Ball a has collided with the left wall
         {ball_a_horizontal_delta = - ball_a_horizontal_delta;
          System.Console.WriteLine("The coordinates of ball a at time of impact on left wall are ({0},{1})",ball_a_int_coord_x,ball_a_int_coord_y);//Debug statement
          Countwallcollisions(wallCollisions);
          wall_collisions.Text = wallCollisions.ToString();
           System.Console.WriteLine("Walls collided with " + wallCollisions + " times");
           System.Console.WriteLine(" ");
         }
      else if(ball_a_int_coord_y<=50) //Ball a has collided with the top wall
         {ball_a_vertical_delta = - ball_a_vertical_delta;
          System.Console.WriteLine("The coordinates of ball a at time of impact on top wall are ({0},{1})",ball_a_int_coord_x,ball_a_int_coord_y);//Debug statement
          Countwallcollisions(wallCollisions);
          wall_collisions.Text = wallCollisions.ToString();
          System.Console.WriteLine("Walls collided with " + wallCollisions + " times");
          System.Console.WriteLine(" ");
         }
      else if(ball_a_int_coord_x+2*ball_a_radius>=formwidth-10) //Ball a has collided with the right wall
         {ball_a_horizontal_delta = - ball_a_horizontal_delta;
          System.Console.WriteLine("The coordinates of ball a at time of impact on right wall are ({0},{1})",ball_a_int_coord_x,ball_a_int_coord_y);//Debug statement
          Countwallcollisions(wallCollisions);
          wall_collisions.Text = wallCollisions.ToString();
           System.Console.WriteLine("Walls collided with " + wallCollisions + " times");
           System.Console.WriteLine(" ");
         }
      else if(ball_a_int_coord_y+2*ball_a_radius>=880) //Ball a has collided with the lower wall
         {ball_a_vertical_delta = - ball_a_vertical_delta;
          System.Console.WriteLine("The coordinates of ball a at time of impact on lower wall are ({0},{1})",ball_a_int_coord_x,ball_a_int_coord_y);//Debug statement
          Countwallcollisions(wallCollisions);
          wall_collisions.Text = wallCollisions.ToString();
          System.Console.WriteLine("Walls collided with " + wallCollisions + " times");
          System.Console.WriteLine(" ");
         }
		  
   }//End of method Updateballa

   protected void StartAnimation(object sender,EventArgs events)
   {
	  start_clicked = true;
   }
   
   protected void ExitProgram(object sender,EventArgs events)
   {System.Console.WriteLine("This program will end execution.");
	   Close();
   }

}//End of class Animatedballframe

