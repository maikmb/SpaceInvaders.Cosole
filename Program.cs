using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;

namespace ConsoleApplication8
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            #region Variaveis

            int score = 0,
                maxscore = 0,
                count = 0,
                speed = 0,
                speed2 = 50;

            int PlayerX = 20,
                PlayerY = 40,
                Shoot1X = 0,
                Shoot1Y = 0,
                Shoot2X = 0,
                Shoot2Y = 0,
                Enemy1Y = int.MinValue,
                Enemy1X = 0;

            bool CanShoot1 = true,
                 CanShoot2 = true;

            Random rand = new Random();

            #endregion

            #region GameInit

            Console.SetWindowSize(40, 40);
            Console.SetCursorPosition(PlayerX, PlayerY);
            Console.WriteLine("@");

            #endregion

            #region Movimentação

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }
                    if (keyPressed.Key == ConsoleKey.LeftArrow)
                    {
                        if (PlayerX > 0)
                        {
                            PlayerX -= 1;
                        }
                    }
                    if (keyPressed.Key == ConsoleKey.RightArrow)
                    {
                        if (PlayerX < Console.WindowWidth - 6)
                        {
                            PlayerX += 1;
                        }
                    }
                    if (keyPressed.Key == ConsoleKey.UpArrow && (CanShoot1 == true || CanShoot2 == true))
                    {
                        if (CanShoot1 == true)
                        {
                            Shoot1X = PlayerX;
                            Shoot1Y = PlayerY + 1;
                            CanShoot1 = false;
                        }
                        else if (CanShoot2 == true)
                        {
                            Shoot2X = PlayerX + 4;
                            Shoot2Y = PlayerY + 1;
                            CanShoot2 = false;
                        }
                    }
                }

                #endregion

                #region Update

                Console.Clear();

                if (Enemy1Y == int.MinValue)
                {
                    Enemy1Y = rand.Next(0, 34);
                    speed2 -= 1;
                    count += 1;
                    if (score > maxscore)
                        maxscore = score;
                }
                if (Enemy1X < 40)
                {
                    Console.SetCursorPosition(Enemy1Y, Enemy1X);
                    Console.WriteLine("\\_||_/");
                    Enemy1X += 1 + speed;
                    if(count > 5)
                    {
                        //speed += 1;
                        count = 0;

                    }
                }
                else
                {
                    Enemy1X = 0;
                    Enemy1Y = int.MinValue;
                }

                #region Shoots

                if (CanShoot1 == false)
                {
                    Console.SetCursorPosition(Shoot1X, Shoot1Y);
                    Console.WriteLine("*");
                    if (Shoot1Y != 0)
                        Shoot1Y -= 1;
                    else
                    {
                        CanShoot1 = true;
                    }
                }

                if (CanShoot2 == false)
                {
                    Console.SetCursorPosition(Shoot2X, Shoot2Y);
                    Console.WriteLine("*");
                    if (Shoot2Y != 0)
                        Shoot2Y -= 1;
                    else
                    {
                        CanShoot2 = true;
                    }
                }

                #endregion

                #region Colision 

                for (int i = 0; i <= 6; i++)
                {
                    if (PlayerY == Enemy1X)
                    {
                        if (PlayerX == Enemy1Y + i || PlayerX + 1 == Enemy1Y + i || PlayerX + 2 == Enemy1Y + i || PlayerX + 3 == Enemy1Y + i || PlayerX + 4 == Enemy1Y + i)
                        {
                            score = 0;
                            speed = 0;
                            break;
                        }
                    }

                    if (Shoot1Y == Enemy1X || Shoot1Y == Enemy1X + 1 || Shoot2Y == Enemy1X || Shoot2Y == Enemy1X + 1)
                    {
                        if(Shoot1X == Enemy1Y + i || Shoot2X == Enemy1Y + i)
                        {
                            Enemy1X = 0;
                            Enemy1Y = int.MinValue;
                            speed2 -= 3;
                        }

                    }
                }

                

                #endregion

                Console.SetCursorPosition(PlayerX, PlayerY);
                Console.WriteLine("|_@_|");
                Console.SetCursorPosition(1, 1);
                Console.WriteLine("SCORE: " + score);
                Console.SetCursorPosition(1,2);
                Console.WriteLine("Max Score: " + maxscore);
                Thread.Sleep(speed2);

                #endregion


            }
        }
    }
}

