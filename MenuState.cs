using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockGame
{
    public class MenuState
    {
        public readonly Button newGame;
        public readonly Button howToPlay;
        public readonly Button exit;

        public MenuState()
        {
            newGame = new Button(new Point(C.screenWidth / 2, C.screenHeight / 2 - 200), "new game");
            howToPlay = new Button(new Point(C.screenWidth / 2, C.screenHeight / 2), "how to play");
            exit = new Button(new Point(C.screenWidth / 2, C.screenHeight / 2 + 200), "exit");
        }

        public void Update()
        {
            newGame.Update();
            howToPlay.Update();
            exit.Update();
        }

        public void Draw()
        {
            C.spriteBatch.Begin();

            newGame.Draw();
            howToPlay.Draw();
            exit.Draw();

            C.spriteBatch.End();
        }
    }
}
