using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace Platformer
{
    public class Hero : Entity
    {
        private bool facingRight = false;
        public Hero() : base("characters")
        {
            sprite.TextureRect = new IntRect(0, 0, 24, 24);
            sprite.Origin = new Vector2f(12, 12);
        }
        public override void Update(Scene scene, float deltaTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                Position -= new Vector2f(100 * deltaTime, 0);
                facingRight = false;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                Position += new Vector2f(100 * deltaTime, 0);
                facingRight = true;
            }
        }
        public override void Render(RenderTarget target)
        {
            sprite.Scale = new Vector2f(facingRight ? -1 : 1, 1);
            base.Render(target);
        }
    }
}
