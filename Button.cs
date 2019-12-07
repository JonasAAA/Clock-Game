using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ClockGame
{
    public class Button
    {
        public bool isAtcivated;

        private readonly Texture2D image;
        Rectangle rectangle;
        private readonly Vector2 position, origin;
        private Color color;
        private bool wasPressed;
        private bool canPress;

        public Button(Point position, string name, bool canPress = true)
        {
            this.canPress = canPress;
            image = C.LoadImage(name);
            rectangle = new Rectangle(position.X - image.Width / 2, position.Y - image.Height / 2, image.Width, image.Height);
            color = Color.CornflowerBlue;
            wasPressed = false;
            isAtcivated = false;
        }

        public void Update()
        {
            if (!canPress)
            {
                color = Color.Red;
                return;
            }
            bool isMouseIn = rectangle.Contains(Mouse.GetState().Position);
            bool isPressed = Mouse.GetState().LeftButton == ButtonState.Pressed && isMouseIn;

            if (isMouseIn)
                color = Color.Blue;
            else
                color = Color.CornflowerBlue;

            if (!isPressed && wasPressed)
                isAtcivated = true;
            else
                isAtcivated = false;

            wasPressed = isPressed;
        }

        public void Draw()
        {
            C.spriteBatch.Draw(image, rectangle, null, color);
        }
    }
}
