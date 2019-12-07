using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ClockGame
{
    public class Camera
    {
        public bool ifFollowPlayer;

        private Matrix transform;
        private float scale;

        public Camera()
        {
            transform = Matrix.Identity;
            scale = 1;
            ifFollowPlayer = true;
        }

        public void Update(Vector2 center, float rotation)
        {
            if (C.IsKeyDown(Keys.LeftControl))
                scale *= 0.99f;
            if (C.IsKeyDown(Keys.LeftShift))
                scale *= 1.01f;

            if (ifFollowPlayer)
            {
                transform =
                    Matrix.CreateTranslation(-center.X, -center.Y, 0)
                    * Matrix.CreateRotationZ(-rotation)
                    * Matrix.CreateScale(scale)
                    * Matrix.CreateTranslation(C.screenWidth * 0.5f, C.screenHeight * 0.5f, 0);
            }
            else
            {
                transform = Matrix.CreateTranslation(C.screenWidth * 0.5f, C.screenHeight * 0.5f, 0);
            }
        }

        public void BeginDraw()
        {
            C.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, transform);
        }
    }
}
