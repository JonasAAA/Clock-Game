using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ClockGame
{
    public class Clock
    {
        public bool isVisited { get; private set; }
        public readonly List<ClockHand> clockHands;
        private Vector2 position;
        private readonly float radius;
        private readonly Disk disk;

        private readonly Texture2D image;
        private readonly Vector2 origin;
        private Color color;

        public Clock(Vector2 position, float radius, float maxAngVel/*, Color color*/)
        {
            color = Color.Yellow;
            isVisited = false;
            float width = 10;
            this.position = position;
            this.radius = radius;

            clockHands = new List<ClockHand>();
            float curAngVel = maxAngVel, curLength = radius * 0.8f;
            for (int i = 0; i < 2; i++)
            {
                float rotation = C.Rand(0, MathHelper.TwoPi);
                clockHands.Add(new ClockHand(position, rotation, curLength, width, curAngVel, color));
                curAngVel /= 60;
                curLength *= 0.5f;
            }

            disk = new Disk(position, width / 2, true, color);

            image = C.LoadImage("clock");
            //image = C.LoadImage("time");
            origin = C.ImageOrigin(image);
            //this.color = color;
        }

        //public Clock(Vector2 position, List<float> lengths, float maxAngVel, Color color)
        //{
        //    float width = 10;
        //    this.position = position;

        //    clockHands = new List<ClockHand>();
        //    float curAngVel = maxAngVel;
        //    foreach (int length in lengths)
        //    {
        //        float rotation = C.Rand(0, MathHelper.TwoPi);
        //        clockHands.Add(new ClockHand(position, rotation, length, width, curAngVel, color));
        //        curAngVel /= 60;
        //    }

        //    radius = lengths[0] * 1.2f;

        //    disk = new Disk(position, width / 2, false, Color.Black);

        //    image = C.LoadImage("clock");
        //    //image = C.LoadImage("time");
        //    origin = C.ImageOrigin(image);
        //    this.color = color;
        //}

        public void Update(float elapsed)
        {
            foreach (ClockHand clockHand in clockHands)
                clockHand.Update(elapsed);

            disk.Update(position);
        }
        
        public void Collide(Player player)
        {
            disk.Collide(player);

            foreach (ClockHand clockHand in clockHands)
            {
                clockHand.Collide(player);
                if (clockHand.isVisited)
                    Visit();
            }

            if (disk.isVisited)
                Visit();
        }

        public void Visit()
        {
            isVisited = true;
            foreach (ClockHand clockHand in clockHands)
                clockHand.Visit();
            color = Color.Green;
            disk.Visit();
        }

        public void Draw()
        {
            C.spriteBatch.Draw(image, position, null, color * 0.5f, 0, origin, 2 * radius / image.Width, SpriteEffects.None, 0);
            //C.spriteBatch.Draw(C.LoadImage("clock"), position, null, color, 0, origin, 2 * radius / image.Width, SpriteEffects.None, 0);

            foreach (ClockHand clockHand in clockHands)
                clockHand.Draw();

            disk.Draw();
        }
    }
}
