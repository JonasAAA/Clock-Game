using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ClockGame
{
    public static class C
    {
        public static int screenWidth, screenHeight;
        public static ContentManager Content;
        public static SpriteBatch spriteBatch;
        //public static Matrix tranform;
        
        private static Random random;

        static C()
        {
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            random = new Random();
        }

        public static Texture2D LoadImage(string name)
            => Content.Load<Texture2D>(name);

        public static Vector2 ImageOrigin(Texture2D image)
            => new Vector2(image.Width * 0.5f, image.Height * 0.5f);

        public static bool IsKeyDown(Keys key)
            => Keyboard.GetState().IsKeyDown(key);

        public static float Rand(float min, float max)
            => min + (float)random.NextDouble() * (max - min);
    }
}
