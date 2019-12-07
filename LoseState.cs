using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockGame
{
    class LoseState
    {
        public Button back;
        private Button youLose;

        public LoseState()
        {
            back = new Button(new Point(C.screenWidth / 2, C.screenHeight / 2 + 200), "back");
            youLose = new Button(new Point(C.screenWidth / 2, C.screenHeight / 2 - 200), "you lose", false);
        }

        public void Update()
        {
            back.Update();
            youLose.Update();
        }

        public void Draw()
        {
            C.spriteBatch.Begin();

            back.Draw();

            youLose.Draw();

            C.spriteBatch.End();
        }
    }
}
