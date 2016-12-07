# BattleShip
First C# program.  A console based game of BattleShip.
The battleship.cs file can be used to create an executable file in Windows to launch and begin playing.

Create the exe file:
You should be able to create a working .exe file via the following command (assuming you have the same .NET FRAMEWORK version installed):
cd <Directory with battleship.cs>
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:exe battleship.cs

You may need to launch the newly created exe file from File Explorer (not from the console).

The game has a menu with multiple difficulties to choose from (even some hidden options).  

No computer AI, just you and a limited amount of rockets to shoot at a randomly generated Battleship layout.  
All 5 ship types are included: 
 - Carrier
 - Battleship
 - Destroyer
 - Submarine
 - Patrol Boat
 
Upon victory/loss the ship layout will be revealed.

Empty ' ' values indicate shots you have missed, 'x' values indicate hits, and '#' indicate open locations.
You cannot shoot in the same place twice.

(this will look terrible unless you view it in RAW format on GitHub)
There is colour but here's a representation of what it looks like:

j7 HIT!
You sunk the Submarine!
   0  1  2  3  4  5  6  7  8  9
A     #  #  #  #  #  #  #  #  #
B  #  #  #  #  #  #  #  #  #  #
C     #  #  #  #  #  #  #  #  #
D  #  x  #  #  #  #  #  #  #  #
E     x  #  #     #     #  #  #
F     x  #  #  #  #  #  #  #  #
G  x  x     #  #  #  #  #  #  #
H  x  x  #  #  #  #  #  #  #  #
I        #  #  #  #  #  #  #  #
J  #  #     #  #  #  #  x  x  x
Rockets left: 30
Enter in the coordinates for your shot:


Victory screen
  ----------------------------
 | 01011001 01001111 01010101 |
 | 01010111 01001111 01001110 |
  ----------------------------
   0  1  2  3  4  5  6  7  8  9
A
B
C
D     C
E     C
F     C     B
G  P  C     B
H  P  C     B  D  D  D
I           B
J                       S  S  S

CONGRATULATIONS!
You won with 37 Rockets
Press Enter to exit to menu

The victory screen will show you only the ship locations and clear out the rest of the screen.
'P' is Patrol Boat, 'B' is Battleship, 'S' is submarine, 'C' is Carrier, 'D' is Destroyer.

Game Over screen

GAME OVER
Better luck next time.
   0  1  2  3  4  5  6  7  8  9
A           #  #  #     #  D  #
B  x  #  #     #     #     D
C  x  #  #     #  x  #  #  D  #
D           #  #  x  #     #  #
E     #  S  #  #  x  #  #  #  #
F     #  x  #  #  B  #  #     #
G  #     x     #  #  #  #  #  #
H  #  #  #  #  #     #     #  #
I  #     #  #  #  #  #  #     #
J  #  #  x  x  x  x  x     #  #
Press Enter to exit to menu

The Game Over screen will keep all your shots visible (including hits as "x"'s) and reveal the remaining 'hitable' locations.

There will surely be bugs so let me know what you find.
