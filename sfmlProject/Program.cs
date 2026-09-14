using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace sfmlProject;

class Program
{
    static void Main(string[] args)
    {
        using (var window = new RenderWindow(new VideoMode(500, 700), "breakout"))
        {
            window.Closed += (o, e) => window.Close();
            Clock clock = new Clock();
            Ball ball = new Ball();
            while (window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();
                window.DispatchEvents();
                ball.Update(deltaTime);
                // TODO: Updates
                window.Clear(new Color(131, 197, 235));
                ball.Draw(window);
                // TODO: Drawing
                window.Display();
            }
        }
    }
}