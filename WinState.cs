using Microsoft.Xna.Framework;

namespace ClockGame
{
    public class WinState
    {
        public Button back;
        private Button youWin;

        public WinState()
        {
            back = new Button(new Point(C.screenWidth / 2, C.screenHeight / 2 + 200), "back");
            youWin = new Button(new Point(C.screenWidth / 2, C.screenHeight / 2 - 200), "you win", false);
        }

        public void Update()
        {
            back.Update();
            youWin.Update();
        }

        public void Draw()
        {
            C.spriteBatch.Begin();

            back.Draw();

            youWin.Draw();

            C.spriteBatch.End();
        }
    }
}
