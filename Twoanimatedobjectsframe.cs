//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**
//Author: F. Holliday
//Author's email: holliday@fullerton.edu
//Course: CPSC223N
//Assignment number: 12
//Due date: 11/18/2019 (mm/dd/yyyy)
//Date last updated: 10/03/2016 (mm/dd/yyyy)
//Source files in this program: Twoanimatedobjectsmain.cs, Twoanimatedobjectsframe.cs, Twoanimatedobjectslogic.cs
//Purpose of this entire program:  Demonstrate how to animate two independent objects.
//This program shows two balls moving linearly in independent directions at speeds different from each other. 
//
//Development status.  This program is done.  It fulfills its purpose of teaching about animation.
//Here "program" means all three modules in the set, namely: Movingballs, Twoobjectsframe, Twoanimatedlogic
//
//Development platform: Bash using Mono4.2.1.102 on Xubuntu16.4 

//Safe software seal of approval: No Microsoft products were used in the construction of this program.

//Known bug: When the author of this program tested it on a 2.0 GHz machine there was noticeable flicker in each of the two balls.
//I believe this is probably caused by the fact that the animation run-time system repaints the entire 1920x1080 image alternating
//between the two balls.  That means the entire graphic area with only the red ball is painted, the entire graphic area with only
//the yellow ball is painted.  This alternating display of two balls is suppose to happen so fast that the viewer will smooth 
//animation.  However, the current refresh rate is too slow.  One obvious fix to this problem is run the program using a higher
//refresh rate (and perhaps a faster CPU and perhaps a faster video card).  There is a better fix for this problem, and that is show
//both balls simultaneously with every repaint of the graphics area.  Students of CPSC223n will find the latter solution utilized
//in another of the sample programs posted for this class.

//Improvements are possible.  This program controls the travel of each ball by using the coordinates of the respective upper left
//corners.  To the casual observer everything appears fine.  But a more refined program will use the center of the ball as the point
//of reference controling the motion of the ball.  Someone should re-organize this program around using the center of a ball as its
//reference point.

//A place for research.  The statement "Size = new Size(formwidth,formheight);" is suppose to set the size of the frame (or form).
//But does it really set the size of the graphic area in the frame to exactly formwidth x formheight?  I don't think it is exact.

//Name of this file: Twoanimatedobjectsframe.cs
//Purpose of this file: Show the graphical area.  Show two balls moving in distinct directions are distinct speeds.
//
//
//The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. Twoanimatedobjectslogic.cs         compiles into library file Twoanimatedlogic.dll
//  2. Twoanimatedobjectsframe.cs         compiles into library file Twoobjectsframe.dll
//  3. Twoanimatedobjectsmain.cs     compiles and links with the two dll files above to create Twoobjects.exe

//Compile this file: 
//mcs -target:library Twoanimatedobjectsframe.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Twoanimatedlogic.dll -out:Twoobjectsframe.dll

//Hardcopy: this source code is best viewed in 7 point monospaced font using portrait orientation.
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class Twoobjectsframe : Form
{  private const int formwidth = 1280;   //1920;
   private const int formheight = 720;   //1080; 
   //In 223n you should use the largest possible graphical area supported by your monitor and still maintain the 16:9 ratio.
   //For 223n projects a graph area of size 1920x1080 looks very nice; even 1600x900 is nice.
   private const int ball_a_radius = 10;
   private const int ball_b_radius = 14;
   private const int horizontaladjustment = 8;  //Does anybody have any idea what the purpose of horizontaladjustment is?
   private const int verticaladjustment = 2*ball_b_radius;
   //The next number is the linear distance ball a will move each time its controlling clock tics.  A large number means faster
   //motion and more jerkiness.  A smaller number means less speed but smooth motion.
   private const double ball_a_distance_moved_per_refresh = 1.6;  //The unit of measure is 1 pixel.
   private const double ball_b_distance_moved_per_refresh = 5.9;  //The unit of measure is 1 pixel.
   private double ball_a_real_coord_x = 0.0;
   private double ball_a_real_coord_y = (double)(formheight/2 - ball_a_radius);
   private double ball_b_real_coord_x = (double)(formwidth - 2*ball_b_radius - horizontaladjustment);
   private double ball_b_real_coord_y = (double)(formheight/2 - ball_b_radius);
   private int ball_a_int_coord_x;  //The x-coordinate of ball a in upper left corner.
   private int ball_a_int_coord_y;  //The y-coordinate of ball a in upper left corner.
   private int ball_b_int_coord_x;  //The x-coordinate of ball b in upper left corner.
   private int ball_b_int_coord_y;  //The y-coordinate of ball b in upper left corner
   private double ball_a_horizontal_delta;
   private double ball_a_vertical_delta;
   private double ball_b_horizontal_delta;
   private double ball_b_vertical_delta;
   private double ball_a_angle_radians;
   private double ball_b_angle_radians;
   private const double graphicrefreshrate = 30.0;  //30.0 Hz = constant refresh rate during the execution of this animated program.
   private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
   private const double ball_a_update_rate = 18.5;  //Units are Hz
   private static System.Timers.Timer ball_a_control_clock = new System.Timers.Timer();
   private bool ball_a_clock_active = false;  //Initial state: The clock controll ball a is not active.
   private const double ball_b_update_rate = 27.75;  //Units are Hz
   private static System.Timers.Timer ball_b_control_clock = new System.Timers.Timer();
   private bool ball_b_clock_active = false;
   
   public Twoobjectsframe()   //The constructor of this class
   {  //Set the title of this form.
      Text = "Two Animated Balls";
      System.Console.WriteLine("formwidth = {0}. formheight = {1}.",formwidth,formheight);
      //Set the initial size of this form called Twoobjectsframe.
      //The professor thinks that sometimes the size of the frame is off by a few pixels.  Someone needs to investigate what is the
      //true height and the true width of the created frame.
      Size = new Size(formwidth,formheight);
      //Set the background color of this form
      BackColor = Color.Green;
      //Set the initial coordinates of ball a.
      ball_a_int_coord_x = (int)(ball_a_real_coord_x);
      ball_a_int_coord_y = (int)(ball_a_real_coord_y);
      System.Console.WriteLine("Initial coordinates: ball_a_int_coord_x = {0}. ball_a_int_coord_y = {1}.",
                               ball_a_int_coord_x,ball_a_int_coord_y);
      //Set the initial coordinates of ball b.
      ball_b_int_coord_x = (int)(ball_b_real_coord_x);
      ball_b_int_coord_y = (int)(ball_b_real_coord_y);
      System.Console.WriteLine("Initial coordinates: ball_b_int_coord_x = {0}. ball_b_int_coord_y = {1}.",
                               ball_b_int_coord_x,ball_b_int_coord_y);
      //Instantiate the collection of supporting algorithms
      Twoanimatedlogic algorithms = new Twoanimatedlogic();
      //Set a random angle of direction for ball a: -90.0 degrees <= ball_a_angle <= +90.0 degrees
      ball_a_angle_radians = algorithms.get_random_direction_for_a();
      System.Console.WriteLine("Direction of ball a = {0} degrees",ball_a_angle_radians*180.0/System.Math.PI);
      ball_a_horizontal_delta = ball_a_distance_moved_per_refresh*System.Math.Cos(ball_a_angle_radians);
      ball_a_vertical_delta = ball_a_distance_moved_per_refresh*System.Math.Sin(ball_a_angle_radians);

      //Set a random angle of direction for ball b:  +90.0 degrees <= ball_b_angle <= +270.0 degrees
      ball_b_angle_radians = algorithms.get_random_direction_for_b();
      System.Console.WriteLine("Direction of ball b = {0} degrees",ball_b_angle_radians*180.0/System.Math.PI);
      ball_b_horizontal_delta = ball_b_distance_moved_per_refresh*System.Math.Cos(ball_b_angle_radians);
      ball_b_vertical_delta = ball_b_distance_moved_per_refresh*System.Math.Sin(ball_b_angle_radians);

      graphic_area_refresh_clock.Enabled = false;  //Initially the clock controlling the rate of updating the display is stopped.
      //The next statement tells the clock what method to perform each time the clock makes a tic.
      graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Updatedisplay);  

      //Initialize clock of ball a.
      ball_a_control_clock.Enabled = false; //Initially the clock controlling ball a is stopped.
      //The next statement tells the clock what method to perform each time the clock makes a tick.
      ball_a_control_clock.Elapsed += new ElapsedEventHandler(Updateballa);

      //Initialize clock of ball b.
      ball_b_control_clock.Enabled = false; //Initially the clock controlling ball a is stopped.
      //The next statement tells the clock what method to perform each time the clock makes a tick.
      ball_b_control_clock.Elapsed += new ElapsedEventHandler(Updateballb);

      Startgraphicclock(graphicrefreshrate);  //refreshrate is how many times per second the display area is re-painted.
      Startballaclock(ball_a_update_rate);    //Set the animation rate for ball a.
      Startballbclock(ball_b_update_rate);    //Set the animation rate for ball b.
      DoubleBuffered = true;
   }//End of constructor

   protected override void OnPaint(PaintEventArgs ee)
   {  Graphics graph = ee.Graphics;
      graph.FillEllipse(Brushes.Yellow,ball_a_int_coord_x,ball_a_int_coord_y,2*ball_a_radius,2*ball_a_radius);
      graph.FillEllipse(Brushes.Red,ball_b_int_coord_x,ball_b_int_coord_y,2*ball_b_radius,2*ball_b_radius);
      //The next statement looks like recursion, but it really is not recursion.
      //In fact, it calls the method with the same name located in the super class.
      base.OnPaint(ee);
   }

   protected void Startgraphicclock(double refreshrate)
   {   double elapsedtimebetweentics;
       if(refreshrate < 1.0) refreshrate = 1.0;  //Avoid dividing by a number close to zero.
       elapsedtimebetweentics = 1000.0/refreshrate;  //elapsedtimebetweentics has units milliseconds.
       graphic_area_refresh_clock.Interval = (int)System.Math.Round(elapsedtimebetweentics);
       graphic_area_refresh_clock.Enabled = true;  //Start clock ticking.
   }

   protected void Startballaclock(double updaterate)
   {   double elapsedtimebetweenballmoves;
       if(updaterate < 1.0) updaterate = 1.0;  //This program does not allow updates slower than 1 Hz.
       elapsedtimebetweenballmoves = 1000.0/updaterate;  //1000.0ms = 1second.  elapsedtimebetweenballmoves has units milliseconds.
       ball_a_control_clock.Interval = (int)System.Math.Round(elapsedtimebetweenballmoves);
       ball_a_control_clock.Enabled = true;   //Start clock ticking.
       ball_a_clock_active = true;
   }

   protected void Startballbclock(double updaterate)
   {   double elapsedtimebetweenballmoves;
       if(updaterate < 1.0) updaterate = 1.0;  //This program does not allow updates slower than 1 Hz.
       elapsedtimebetweenballmoves = 1000.0/updaterate;
       ball_b_control_clock.Interval = (int)System.Math.Round(elapsedtimebetweenballmoves);
       ball_b_control_clock.Enabled = true;   //Start clock ticking.
       ball_b_clock_active = true;
   }

   protected void Updatedisplay(System.Object sender, ElapsedEventArgs evt)
   {  Invalidate();  //Weird: This creates an artificial event so that the graphic area will repaint itself.
      //System.Console.WriteLine("The clock ticked and the time is {0}", evt.SignalTime);  //Debug statement; remove it later.
      if(!(ball_a_clock_active || ball_b_clock_active))
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
      //Determine if ball a has passed beyond the graphic area.
      if(ball_a_int_coord_x >= formwidth || ball_a_int_coord_y + 2*ball_a_radius <= 0 || ball_a_int_coord_y >= formheight)
          {ball_a_clock_active = false;
           ball_a_control_clock.Enabled = false;
           System.Console.WriteLine("The clock controlling ball a has stopped.");
          }
   }//End of method Updateballa

   protected void Updateballb(System.Object sender, ElapsedEventArgs evt)
   {  ball_b_real_coord_x = ball_b_real_coord_x + ball_b_horizontal_delta;
      //In the next statement the minus sign is used because the y-axis is upside down relative to the standard cartesian 
      //coordinate system.  The users only know the standard coordinate system.
      ball_b_real_coord_y = ball_b_real_coord_y - ball_b_vertical_delta;  
      ball_b_int_coord_x = (int)System.Math.Round(ball_b_real_coord_x);
      ball_b_int_coord_y = (int)System.Math.Round(ball_b_real_coord_y);
      //System.Console.WriteLine("The clock ticked for ball b and the time is {0}", evt.SignalTime);//Debug statement; remove later.
//Begin section of old statements no longer needed.
//    //Determine if ball b has passed beyond the graphic area.
//      if(ball_b_int_coord_x + 2*ball_b_radius <= 0 || ball_b_int_coord_y + 2*ball_b_radius <= 0 || ball_b_int_coord_y >= formheight)
//          {ball_b_clock_active = false;
//           ball_b_control_clock.Enabled = false;
//           System.Console.WriteLine("The clock controling ball b has stopped.");
//          }
//End section of old statements no longer needed.
      if(ball_b_int_coord_x<=0) //Ball b has collided with the left wall
         {ball_b_horizontal_delta = - ball_b_horizontal_delta;
          System.Console.WriteLine("The coordinates of ball b at time of impact on left wall are ({0},{1})",ball_b_int_coord_x,ball_b_int_coord_y);//Debug statement
         }
      else if(ball_b_int_coord_y<=0) //Ball b has collided with the top wall
         {ball_b_vertical_delta = - ball_b_vertical_delta;
          System.Console.WriteLine("The coordinates of ball b at time of impact on top wall are ({0},{1})",ball_b_int_coord_x,ball_b_int_coord_y);//Debug statement
         }
      else if(ball_b_int_coord_x+2*ball_b_radius>=formwidth-horizontaladjustment) //Ball b has collided with the right wall
         {ball_b_horizontal_delta = - ball_b_horizontal_delta;
          System.Console.WriteLine("The coordinates of ball b at time of impact on right wall are ({0},{1})",ball_b_int_coord_x,ball_b_int_coord_y);//Debug statement
         }
      else if(ball_b_int_coord_y+2*ball_b_radius+2*ball_b_radius>=formheight) //Ball b has collided with the lower wall
         {ball_b_vertical_delta = - ball_b_vertical_delta;
          System.Console.WriteLine("The coordinates of ball b at the time of impact on lower wall are ({0},{1})",ball_b_int_coord_x,ball_b_int_coord_y);//Debug statement
         }
   }//End of method Updateballb

}//End of class Twoobjectsframe



