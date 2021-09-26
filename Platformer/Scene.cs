using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.IO;
using System.Text;
namespace Platformer
{
    public class Scene
    {
        private readonly Dictionary<string, Texture> textures;
        public readonly List<Entity> entities;

        private string currentScene;
        private string nextScene;

        private Window passingWin;
        public Scene(Window window)
        {
            passingWin = window;
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
        public void Reload()
        {
            nextScene = currentScene;
        }
        public void Load(string loadString)
        {
            nextScene = loadString;
        }
        private void HandleSceneChange()
        {
            if (nextScene == null) return;
            entities.Clear();
            SpawnEntity(new Background());
            string file = $"assets/{nextScene}.txt";
            System.Console.WriteLine($"Loading scene '{file}'");

            foreach (var line in File.ReadLines(file, Encoding.UTF8))
            {
                string parsed = line.Trim();

                if (line.Length <= 0) continue; //it shouldn't be possible for it to be negative but you never know...
                int commentAt = parsed.IndexOf('#');
                if (commentAt >= 0)
                {
                    parsed = parsed.Substring(0, commentAt);
                    parsed = parsed.Trim();
                }
                string[] words = parsed.Split(" ");
                SelectSpawnObject(words);
            }

            currentScene = nextScene;
            nextScene = null;
        }
        private void SelectSpawnObject(string[] words)
        {
            switch (words[0])
            {
                case "w":
                    {
                        SpawnEntity(new Platform() { Position = new Vector2f(int.Parse(words[1]), int.Parse(words[2])) });
                        break;
                    }
                case "d":
                    {
                        SpawnEntity(new Door()
                        {
                            Position = new Vector2f(int.Parse(words[1]), int.Parse(words[2])),
                            NextRoom = words[3]
                        });
                        break;
                    }
                case "k":
                    {

                        SpawnEntity(new Key() { Position = new Vector2f(int.Parse(words[1]), int.Parse(words[2])) });
                        break;
                    }
                case "h":
                    {
                        SpawnEntity(new Hero(passingWin) { Position = new Vector2f(int.Parse(words[1]), int.Parse(words[2])) });
                        break;
                    }
            }
        }
        public void UpdateAll(float deltaTime)
        {
            HandleSceneChange();
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
