using System;
using SFML.Graphics;
using SFML.System;
namespace Platformer
{
    public class Entity
    {
        private string textureName;
        protected Sprite sprite;
        public bool IsDead;

        public enum states
        {
            ToBeSpawned,
            Dead,
            ToBeDespawned
        }
        public states State;
        public virtual bool Solid => false;

        protected Entity(string textureName)
        {
            sprite = new Sprite();
            this.textureName = textureName;
        }
        public Vector2f Position
        {
            get { return sprite.Position; }
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
            if (IsDead) return;
            target.Draw(sprite);
        }
    }
}
