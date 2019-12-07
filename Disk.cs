using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClockGame
{
    public class Disk
    {
        public bool isVisited { get; private set; }
        private Vector2 position;
        private float radius;
        private bool isDeadly;

        private readonly Texture2D image;
        private readonly Vector2 origin;
        private Color color;

        public Disk(Vector2 position, float radius, bool isDeadly, Color color)
        {
            isVisited = false;
            this.position = position;
            this.radius = radius;
            this.isDeadly = isDeadly;

            image = C.LoadImage("disk");
            origin = C.ImageOrigin(image);
            this.color = color;
            if (isDeadly)
                this.color = Color.Red;
        }

        public void Update(Vector2 position)
        {
            this.position = position;
        }

        public void Collide(Player player)
        {
            if (player.radius + radius <= Vector2.Distance(player.position, position))
                return;

            if (isDeadly)
            {
                player.Die();
                return;
            }

            Visit();

            Vector2 direction = player.position - position;
            direction.Normalize();
            player.position = position + direction * (player.radius + radius);
            player.velocity -= direction * Vector2.Dot(player.velocity, direction);
        }

        public void Visit()
        {
            isVisited = true;
            if (isVisited && !isDeadly)
                color = Color.Green;
        }

        public void Draw()
        {
            C.spriteBatch.Draw(image, position, null, color, 0, origin, 2 * radius / image.Width, SpriteEffects.None, 0);
        }
    }
}
