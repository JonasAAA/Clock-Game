using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ClockGame
{
    public class Player
    {
        public ClockHand clockHand;
        public Vector2 position, relPos, velocity;
        public float rotation;
        public readonly float radius;
        public bool isFree, isAlive;

        private float angVel, relRot, speed, jumpPow, rotCorSpeed, rotTarget;
        private int freeJumpMax, freeJumpCount;
        private readonly float gravConst, angAccel;

        private readonly Keys up, left, down, right, jump;
        
        private readonly Texture2D image;
        private readonly Vector2 origin;
        private Color color;

        public Player(Vector2 position, float rotation, float radius, Keys up, Keys left, Keys down, Keys right, Keys jump, Color color)
        {
            this.position = position;
            this.rotation = rotation;
            this.radius = radius;
            angVel = 0;
            relRot = 0;
            speed = 200;
            jumpPow = 150;
            relPos = Vector2.Zero;
            velocity = Vector2.Zero;
            isFree = true;
            isAlive = true;
            rotCorSpeed = 1;
            rotTarget = 0;
            freeJumpMax = 1;
            freeJumpCount = 0;

            gravConst = 100;
            angAccel = 0.5f;

            this.up = up;
            this.left = left;
            this.down = down;
            this.right = right;
            this.jump = jump;

            image = C.LoadImage("smile");
            origin = C.ImageOrigin(image);
            this.color = color;
        }

        //public Player(ClockHand clockHand, Vector2 relPos, float relRot, float radius, Keys up, Keys left, Keys down, Keys right, Keys jump, Color color)
        //{
        //    this.clockHand = clockHand;
        //    position = clockHand.center + clockHand.dir * relPos.X + clockHand.orthDir * relPos.Y;
        //    this.relPos = relPos;
        //    rotation = clockHand.rotation + relRot;
        //    this.relRot = relRot;
        //    this.radius = radius;
        //    velocity = Vector2.Zero;
        //    angVel = 0;
        //    speed = 200;
        //    isFree = false;
        //    jumpPow = 100;

        //    this.up = up;
        //    this.left = left;
        //    this.down = down;
        //    this.right = right;
        //    this.jump = jump;

        //    image = C.LoadImage("wheel");
        //    origin = C.ImageOrigin(image);
        //    this.color = color;
        //}

        public void Update(float elapsed)
        {
            if (!isAlive)
                return;

            if (isFree)
            {
                if (C.IsKeyDown(left))
                    angVel -= angAccel * elapsed;
                if (C.IsKeyDown(right))
                    angVel += angAccel * elapsed;

                if (C.IsKeyDown(jump) && freeJumpCount < freeJumpMax)
                {
                    freeJumpCount++;
                    velocity += new Vector2((float)Math.Cos(rotation - MathHelper.PiOver2), (float)Math.Sin(rotation - MathHelper.PiOver2)) * jumpPow;
                }

                //velocity.Y += gravConst * elapsed;
                position += velocity * elapsed;
                rotation += angVel * elapsed;
            }
            else
            {
                if (relRot >= rotCorSpeed * elapsed)
                    relRot -= rotCorSpeed * elapsed;
                else
                {
                    if (relRot <= -rotCorSpeed * elapsed)
                        relRot += rotCorSpeed * elapsed;
                    else
                        relRot = 0;
                }

                if (C.IsKeyDown(left))
                {
                    if (relPos.Y < 0)
                        relPos.X -= speed * elapsed;
                    else
                        relPos.X += speed * elapsed;
                }

                if (C.IsKeyDown(right))
                {
                    if (relPos.Y < 0)
                        relPos.X += speed * elapsed;
                    else
                        relPos.X -= speed * elapsed;
                }

                position = clockHand.center + clockHand.dir * relPos.X + clockHand.orthDir * relPos.Y;
                rotation = clockHand.rotation + relRot + rotTarget;
                velocity = clockHand.orthDir * relPos.X * System.Math.Abs(clockHand.angVel);
                //velocity = clockHand.orthDir * relPos.X * clockHand.angVel;
                angVel = clockHand.angVel;

                if (relPos.X < 0 || relPos.X > clockHand.length)
                {
                    isFree = true;
                    //position += velocity * elapsed;
                }

                if (C.IsKeyDown(jump))
                {
                    isFree = true;
                    Vector2 velChange = jumpPow * clockHand.orthDir;
                    if (relPos.Y < 0)
                        velChange *= -1;
                    velocity += velChange;
                    position += velocity * elapsed;
                }
            }
            if (Math.Abs(position.X) > 2000 || Math.Abs(position.Y) > 2000)
                isAlive = false;
        }

        public void Attach(ClockHand clockHand, Vector2 relPos)
        {
            this.clockHand = clockHand;
            isFree = false;
            this.relPos = relPos;
            //relRot = 0;
            if (relPos.Y * clockHand.angVel < 0)
                rotTarget = 0;
            else
                rotTarget = MathHelper.Pi;
            relRot = rotation - clockHand.rotation - rotTarget;
            relRot %= MathHelper.TwoPi;
            if (relRot > MathHelper.Pi)
                relRot -= MathHelper.TwoPi;
            if (relRot < -MathHelper.Pi)
                relRot += MathHelper.TwoPi;

            freeJumpCount = 0;
        }

        public void Die()
        {
            isAlive = false;
        }

        public void Draw()
        {
            C.spriteBatch.Draw(image, position, null, Color.AntiqueWhite /*color*/, rotation, origin, 2 * radius / image.Width, SpriteEffects.None, 0);
        }
    }
}
