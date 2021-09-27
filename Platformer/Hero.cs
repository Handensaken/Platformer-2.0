using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace Platformer
{
    public class Hero : Entity
    {
        public const float WalkSpeed = 100.0f;
        public const float GravityForce = 400.0f;
        public const float JumpForce = 250f;

        private float verticalSpeed;
        private bool isGrounded;
        private bool isUpPressed;
        private bool facingRight = false;
        private Window win;
        IntRect[] textures = new IntRect[2];
        private int indexTracker;

        public Hero(Window passingWin) : base("characters")
        {
            win = passingWin;
            textures[0] = new IntRect(0, 0, 24, 24);
            textures[1] = new IntRect(24, 0, 24, 24);
            sprite.TextureRect = textures[1];
            sprite.Origin = new Vector2f(12, 12);
            indexTracker = 0;
        }
        public override FloatRect Bounds
        {
            get
            {
                var bounds = base.Bounds;
                bounds.Left += 3;
                bounds.Width -= 6;
                bounds.Top += 3;
                bounds.Height -= 3;
                return bounds;
            }
        }
        Clock clock = new Clock();
        float timer;
        public override void Update(Scene scene, float deltaTime)
        {
            timer += clock.Restart().AsSeconds(); ;
            bool moving = false;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                scene.TryMove(this, new Vector2f(-100 * deltaTime, 0));
                facingRight = false;
                moving = true;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                scene.TryMove(this, new Vector2f(100 * deltaTime, 0));
                facingRight = true;
                moving = true;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                if (isGrounded && !isUpPressed)
                {
                    verticalSpeed = -JumpForce;
                    isUpPressed = true;
                }
                else isUpPressed = false;
            }


            verticalSpeed += GravityForce * deltaTime;
            if (verticalSpeed > 500.0f) verticalSpeed = 500.0f;
            isGrounded = false;
            Vector2f velocity = new Vector2f(0, verticalSpeed * deltaTime);
            if (scene.TryMove(this, velocity))
            {
                if (verticalSpeed > 0.0f)
                {
                    isGrounded = true;
                    verticalSpeed = 0.0f;
                }
                else
                {
                    verticalSpeed = 0.5f;
                }
            }

            if (moving && isGrounded)
            {
                if (timer >= 0.2f)
                {
                    sprite.TextureRect = textures[indexTracker];
                    indexTracker++;
                    if (indexTracker > textures.Length - 1) { indexTracker = 0; }
                    timer = 0;
                }
            }
            else
            {
                sprite.TextureRect = textures[0];
            }

            if (sprite.Position.X > 400 || sprite.Position.X < 0 || sprite.Position.Y > 300 || sprite.Position.Y < 0)
            {
                scene.Reload();
            }
        }
        public override void Render(RenderTarget target)
        {
            sprite.Scale = new Vector2f(facingRight ? -1 : 1, 1);
            base.Render(target);
        }
    }
}
