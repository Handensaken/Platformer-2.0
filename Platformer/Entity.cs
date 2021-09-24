using System;
using SFML.Graphics;
using SFML.System;
namespace Platformer
{
    public class Entity
    {
        private string textureName;
        protected Sprite sprite;
        public bool isDead;

        protected Entity(string textureName)
        {
            sprite = new Sprite();
            this.textureName = textureName;
        }
        public Vector2f Position
        {
            get { return this.Position; }
            set
            {
                sprite.Position = value;
            }
        }
        public virtual FloatRect Bounds => sprite.GetGlobalBounds();
        public virtual void Create(Scene scene)
        {
            sprite.Texture = scene.LoadTexture(textureName);
        }
        public virtual void Update(Scene scene, float deltaTime)
        {

        }
        public virtual void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }
    }
}
