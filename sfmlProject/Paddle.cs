using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
namespace sfmlProject;

public class Paddle
{
    public Sprite sprite;
    public const float Diameter = 20f;
    public const float Radius = Diameter * 0.5f;
    
    Clock timer = new Clock();
    private float startTimer = 0;
    private bool isPowerup = false;

    private int j = 0;
    
    public Vector2f size;
    
    //TODO: THE BALL KEEPS DIRECTION WHEN PLACED ON PADDLE AFTER LOSS
    

    public void Update (Ball ball, float deltaTime, Powerup powerup)
    {
        float speed = deltaTime * 300f;
        Vector2f newPos = sprite.Position;
        
        HandleInput(ref newPos, speed);

        if (Collision.CircleRectangle(ball.sprite.Position, Ball.Radius, this.sprite.Position, size, out Vector2f hit))
        {
            ball.sprite.Position += hit;
            ball.Reflect(hit.Normalized());
        }

        foreach (Powerup pW in powerup.powerups)
        {
            if (Collision.CircleRectangle(pW.sprite.Position, Powerup.Radius, sprite.Position, size, out Vector2f shit))
            {
                Console.WriteLine("shtu the fck up" + j);
                startTimer = timer.Restart().AsSeconds();
                if (true)
                {
                    sprite.Scale *= 2f;
                }
            }   
            // restart timer
            // have if with bool for powerup inside here
            // check elapsed time outside
            // when elapsed time reaches greater than 4 turn off powerup

        }
        
    }

    public void Draw(RenderTarget target)
    {
        target.Draw(sprite);
    }

    public void HandleInput(ref Vector2f newPos, float speed)
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            if (sprite.Position.X - Diameter * 2.3f >= 0)
            {
                newPos.X -= speed;
            }
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            if (!(sprite.Position.X + Diameter * 2.3f > GraphicsSettings.ScreenW))
            {
                newPos.X += speed;
            }
        }
        
        sprite.Position = newPos;
    }
    
    public Paddle()
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("assets/paddle.png");
        sprite.Position = new Vector2f((GraphicsSettings.ScreenW / 2f), 650f);
        
        Vector2f paddleTextureSize = (Vector2f) sprite.Texture.Size;
        sprite.Origin = 0.5f * paddleTextureSize;
        sprite.Scale = new Vector2f(Diameter / paddleTextureSize.Y, Diameter / paddleTextureSize.Y);
        
        size = new Vector2f(sprite.GetGlobalBounds().Width, sprite.GetGlobalBounds().Height);
    }
}