using Microsoft.Xna.Framework;

namespace ClockGame
{
    public class ExplainState
    {
        public Button back;
        private Button explanation;

        public ExplainState()
        {
            back = new Button(new Point(C.screenWidth / 2, C.screenHeight / 2 + 400), "back");
            explanation = new Button(new Point(C.screenWidth / 2, C.screenHeight / 2 - 200), "explanation", false);
        }

        public void Update()
        {
            back.Update();
            explanation.Update();
        }

        public void Draw()
        {
            C.spriteBatch.Begin();

            back.Draw();

            explanation.Draw();

            C.spriteBatch.End();
        }
    }
}
