using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ClockGame
{
    public class PlayState
    {
        public bool ifWin;
        public readonly Player player;
        private readonly List<Clock> clocks;
        private readonly Camera camera;

        public PlayState()
        {
            ifWin = false;

            List<Vector2> centers = new List<Vector2>()
            {
                new Vector2(300, 0),
                new Vector2(-300, 0),
                new Vector2(0, 300),
                new Vector2(0, -300)
            };

            clocks = new List<Clock>();
            //CreateCenters(centers, 10);
            AdjustClockSizes(centers);

            player = new Player(new Vector2(0, 0), 0, 32, Keys.Up, Keys.Left, Keys.Down, Keys.Right, Keys.Space, Color.White);
            camera = new Camera();
            //camera.ifFollowPlayer = false;
        }

        public void Update(float elapsed)
        {
            foreach (Clock clock in clocks)
                clock.Update(elapsed);

            player.Update(elapsed);

            foreach (Clock clock in clocks)
                clock.Collide(player);

            ifWin = true;
            foreach (Clock clock in clocks)
            {
                if (!clock.isVisited)
                    ifWin = false;
            }

            camera.Update(player.position, player.rotation);
        }

        public void Draw()
        {
            camera.BeginDraw();

            foreach (Clock clock in clocks)
                clock.Draw();

            player.Draw();

            C.spriteBatch.End();
        }

        void CreateCenters(List<Vector2> centers, int count)
        {
            float minCoord = -1024, maxCoord = 1024;
            for (int i = 0; i < count; i++)
                centers.Add(new Vector2(C.Rand(minCoord, maxCoord), C.Rand(minCoord, maxCoord)));
        }

        void AdjustClockSizes(List<Vector2> centers)
        {
            
            for (int i = 0; i < centers.Count; i++)
            {
                float minDist = 1000000000;
                for (int j = 0; j < centers.Count; j++)
                {
                    if (i == j)
                        continue;

                    float dist = Vector2.Distance(centers[i], centers[j]);

                    if (minDist > dist)
                        minDist = dist;
                }
                Color color = new Color(C.Rand(0, 1), C.Rand(0, 1), C.Rand(0, 1));
                clocks.Add(new Clock(centers[i], minDist * 0.5f, C.Rand(0.5f, 1.5f)/*, color*/));
            }
        }
    }
}
