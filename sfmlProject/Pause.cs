using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace sfmlProject;

public class Pause
{
    // ReSharper disable once InconsistentNaming
    public bool isPaused = true; //TODO: revert to false
    public Text gui = new();
    
  public void PauseGame()
    {
        isPaused = true;
    }

    public void Update(RenderWindow window, Ball ball, Tiles tiles)
    {
        window.KeyPressed += (o, e) =>
        {
            //TODO Change space to another key it is TEMPORARY
            if (e.Code == Keyboard.Key.Space && isPaused)
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
            gui.DisplayedString = "Unfortunately you suck at the game.";
        }
        else
        {
            gui.DisplayedString = "The game is now paused!";
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