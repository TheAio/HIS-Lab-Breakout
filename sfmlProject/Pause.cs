using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace sfmlProject;

public class Pause
{
    // ReSharper disable once InconsistentNaming
    public bool isPaused;
    public Text gui = new();
    
  public void PauseGame(Paddle paddle)
    {
        paddle.sprite.Position = new Vector2f((GraphicsSettings.ScreenW / 2f), 650f);
        isPaused = true;
    }

    public void Update(RenderWindow window, Ball ball, Tiles tiles)
    {
        window.KeyPressed += (o, e) =>
        {
            if (e.Code == Keyboard.Key.B && isPaused)
            {
                ball.health = 3;
                ball.score = 0;
                tiles.CreateTiles();
                
                isPaused = false;
            }
        };
    }

    public void Draw(RenderTarget target, Ball ball)
    {
        if (ball.health <= 0)
        {
            gui.DisplayedString = $"Game over!\nFinal score: {ball.score}";
        }
        else
        {
            gui.DisplayedString = $"You win!\nFinal score: {ball.score}";
        }
        gui.Position = new Vector2f(12, 8);
        target.Draw(gui);
    }

    public Pause()
    {
        gui = new Text();
        gui.CharacterSize = 24;
        gui.Font = new Font("assets/future.ttf");
    }
}