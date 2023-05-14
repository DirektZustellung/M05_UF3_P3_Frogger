using M05_UF3_P3_Frogger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace M05_UF3_P3_Frogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.CursorVisible = false;
            Console.WindowWidth = Utils.MAP_WIDTH;
            Console.WindowHeight = Utils.MAP_HEIGHT;
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;

            
            List<Lane> lanes = new List<Lane>();
            Lane lanestemp;
            
            
            for (int i = 0; i < Utils.MAP_HEIGHT; i++)
            {
                ConsoleColor background;
                
                char elementsChar;
                
                bool speedPlayer = false;
                bool damageElements = true;
                bool damageBackground = false;
                const float percent = 0.05f;
                float elementsPercent = (Utils.MAP_HEIGHT - i) * percent;
                List<ConsoleColor> colorsElements = new List<ConsoleColor>(Utils.colorsCars);
                if (i == 0 || i == ((Utils.MAP_HEIGHT-1)*0.5) || i == (Utils.MAP_HEIGHT - 1)) 
                {
                    background = ConsoleColor.DarkGreen;
                    elementsChar = ' ';
                    damageElements = false;
                    damageBackground = false;
                    speedPlayer = false;
                }else if (i > 0 && i < ((Utils.MAP_HEIGHT - 1)*0.5))
                {
                    elementsChar = Utils.charLogs;
                    background = ConsoleColor.Blue;
                    speedPlayer = true;
                    damageElements = false;
                    damageBackground = true;
                    colorsElements = new List<ConsoleColor>(Utils.colorsLogs);
                }else
                {
                    elementsChar = Utils.charCars;
                    background = ConsoleColor.Black;
                    colorsElements = new List<ConsoleColor>(Utils.colorsCars) ;
                }
               
                lanestemp = new Lane(i, speedPlayer, background, damageElements, damageBackground, elementsPercent, elementsChar, colorsElements);
                lanes.Add(lanestemp);

              
            }

            Vector2Int startPos = new Vector2Int(Utils.MAP_WIDTH / 2, Utils.MAP_HEIGHT - 1);
            Player player = new Player();
            

            Utils.GAME_STATE state = Utils.GAME_STATE.RUNNING;
        

            while (state == Utils.GAME_STATE.RUNNING)
            {
                Vector2Int input = Utils.Input();
             

                foreach (Lane lane in lanes)
                {
                    lane.Update();
                    lane.Draw();
                    player.Draw(ConsoleColor.Black);
                  
                }
                    state =player.Update(input, lanes);
                
            
                TimeManager.NextFrame();
            }

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(state == Utils.GAME_STATE.WIN ? "YOU WIN!" : "YOU LOOSE!");
           
            Console.ReadKey();

            
        }
    }
}

    

