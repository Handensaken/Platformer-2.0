using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
namespace Platformer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(800, 600), "Platformer"))
            {
                window.Closed += (o, e) => window.Close();
                Clock clock = new Clock();
                Scene scene = new Scene();


                scene.SpawnEntity(new Background());
                for (int i = 0; i < 10; i++)
                {
                    scene.SpawnEntity(new Platform { Position = new Vector2f(18 + i * 18, 288) });
                }
                scene.SpawnEntity(new Hero() { Position = new Vector2f(50, 100) });

                window.SetView(new View(new Vector2f(200, 150), new Vector2f(400, 300)));

                while (window.IsOpen)
                {
                    window.DispatchEvents();
                    float deltaTime = clock.Restart().AsSeconds();

                    //UPDATES
                    scene.UpdateAll(deltaTime);

                    window.Clear(Color.White);

                    //DRAWS

                    scene.RenderAll(window);

                    window.Display();
                }


            }
        }
    }
}
