using System;
using SFML.Graphics;
using SFML.System;

namespace Platformer
{
    public class Door : Entity
    {
        public string NextRoom;
        public bool Unlocked;
        public Door() : base("tileset")
        {
            sprite.TextureRect = new IntRect(180, 103, 18, 23);
            sprite.Origin = new Vector2f(9, 11.5f);
        }
    }
}
