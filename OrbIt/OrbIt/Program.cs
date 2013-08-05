using System;

namespace OrbIt
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                
                GameForm gf;
                gf = new GameForm(game);
                gf.Show();
                gf.Location = new System.Drawing.Point(1015, 0);
                game.Run();
               
            }
        }
    }
#endif
}

