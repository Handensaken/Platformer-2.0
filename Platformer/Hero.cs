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
        public Hero(Window passingWin) : base("characters")
        {
            win = passingWin;
            sprite.TextureRect = new IntRect(0, 0, 24, 24);
            sprite.Origin = new Vector2f(12, 12);
        }
        public override void Update(Scene scene, float deltaTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                scene.TryMove(this, new Vector2f(-100 * deltaTime, 0));
                facingRight = false;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                scene.TryMove(this, new Vector2f(100 * deltaTime, 0));
                facingRight = true;
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
                }
                verticalSpeed = 0.0f;
            }
            //here i'm supposed to check if the hero is outside the screen but I can't find a way to get the bounds of the view
            

        }
        public override void Render(RenderTarget target)
        {
            sprite.Scale = new Vector2f(facingRight ? -1 : 1, 1);
            base.Render(target);
        }
    }
}
