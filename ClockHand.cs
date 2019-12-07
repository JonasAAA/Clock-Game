using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ClockGame
{
    public class ClockHand
    {
        public bool isVisited { get; private set; }
        public float rotation, angVel;
        public readonly Vector2 center;
        public Vector2 dir, orthDir;
        public readonly float length;

        private readonly float width;
        private readonly Disk disk;

        private readonly Texture2D image;
        private readonly Vector2 origin;
        private Color color;

        public ClockHand(Vector2 center, float rotation, float length, float width, float angVel, Color color)
        {
            isVisited = false;
            this.center = center;
            this.rotation = rotation;
            this.length = length;
            this.width = width;
            this.angVel = angVel;
            dir = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            orthDir = new Vector2(-dir.Y, dir.X);
            if (angVel < 0)
                orthDir *= -1;

            disk = new Disk(center + dir * length, width * 0.5f, false, color);

            image = C.LoadImage("pixel");
            origin = C.ImageOrigin(image);
            this.color = color;
        }

        public void Update(float elapsed)
        {
            rotation += angVel * elapsed;
            dir = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            orthDir = new Vector2(-dir.Y, dir.X);
            if (angVel < 0)
                orthDir *= -1;

            disk.Update(center + dir * length);

            if (disk.isVisited)
                Visit();
        }

        public void Collide(Player player)
        {
            disk.Collide(player);

            // where center is (0, 0) and hand is with angle (rotation)
            Vector2 playerRelPos = player.position - center;
            // where cenetr is (0, 0) and hand is with angle 0
            Vector2 relPos = new Vector2(Vector2.Dot(playerRelPos, dir), Vector2.Dot(playerRelPos, orthDir));

            if (0 <= relPos.X && relPos.X <= length && Math.Abs(relPos.Y) < player.radius + width / 2)
            {
                if (relPos.Y > 0)
                    relPos.Y = player.radius + width / 2;
                else
                    relPos.Y = -(player.radius + width / 2);

                if (!player.isFree && player.relPos.Y != relPos.Y)
                {
                    player.Die();
                    return;
                }
                Visit();
                player.Attach(this, relPos);
            }
        }

        public void Visit()
        {
            isVisited = true;
            color = Color.Green;
            disk.Visit();
        }

        public void Draw()
        {
            C.spriteBatch.Draw(image, center + dir * length * 0.5f, null, color, rotation, origin, new Vector2(length, width), SpriteEffects.None, 0);
            disk.Draw();
            //C.spriteBatch.Draw(diskImage, center, null, color, 0, diskOrigin, width / diskImage.Width, SpriteEffects.None, 0);
            //C.spriteBatch.Draw(diskImage, center + dir * length, null, color, 0, diskOrigin, width / diskImage.Width, SpriteEffects.None, 0);
        }
    }
}
