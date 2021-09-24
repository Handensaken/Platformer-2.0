using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
namespace Platformer
{
    public class Scene
    {
        private readonly Dictionary<string, Texture> textures;
        public readonly List<Entity> entities;
        public Scene()
        {
            textures = new Dictionary<string, Texture>();
            entities = new List<Entity>();
        }
        public void SpawnEntity(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }
        public Texture LoadTexture(string name)
        {
            if (textures.TryGetValue(name, out Texture found))
            {
                return found;
            }
            string fileName = $"assets/{name}.png";
            Texture texture = new Texture(fileName);
            textures.Add(name, texture);
            return texture;
        }
        public bool TryMove(Entity entity, Vector2f movement)
        {
            entity.Position += movement;
            bool collided = false;
            for (int i = 0; i < entities.Count; i++)
            {
                Entity other = entities[i];
                if (!other.Solid) continue;
                if (other == entity) continue;
                if (Collision.RectangleRectangle(entity.Bounds, other.Bounds, out Collision.Hit hit))
                {
                    entity.Position += hit.Normal * hit.Overlap;
                    i = -1;
                    collided = true;
                }
            }
            return collided;
        }
        public void UpdateAll(float deltaTime)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(this, deltaTime);
                if (entities[i].State == Entity.states.Dead) entities.RemoveAt(i);
            }
        }
        public void RenderAll(RenderTarget target)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Render(target);
            }
        }
    }
}
