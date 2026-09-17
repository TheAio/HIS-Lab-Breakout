using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace sfmlProject;

class Program
{
    static void Main(string[] args)
    {
        using (var window = new RenderWindow(new VideoMode(GraphicsSettings.ScreenW, GraphicsSettings.ScreenH), "breakout"))
        {
            window.Closed += (o, e) => window.Close();
            Clock clock = new();
            Ball ball = new();
            Paddle paddle = new();
            Tiles tiles = new();
            Pause pause = new();
            while (window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();
                window.DispatchEvents();
                if (!pause.isPaused)
                {
                    ball.Update(deltaTime, paddle, window, tiles, pause);
                    paddle.Update(ball, deltaTime);
                    tiles.Update(tiles, ball, deltaTime, pause);
                    window.Clear(new Color(131, 197, 235));
                    ball.Draw(window);
                    paddle.Draw(window);
                    tiles.Draw(window);
                }
                else
                {
                    pause.Update(window, ball, tiles);
                    window.Clear(new Color(101, 167, 205));
                    pause.Draw(window, ball);
                }

                window.Display();
            }
        }
    }
}