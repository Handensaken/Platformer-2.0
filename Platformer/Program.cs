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



                window.SetView(new View(new Vector2f(200, 150), new Vector2f(400, 300)));
                Scene scene = new Scene(window);
            
                scene.SpawnEntity(new Background());
                scene.Load("level0");

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
