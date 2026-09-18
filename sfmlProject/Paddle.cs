using System;
using System.Timers;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
namespace sfmlProject;

public class Paddle
{
    public Sprite sprite;
    public const float Diameter = 20f;
    public const float Radius = Diameter * 0.5f;
    private DateTime timer1;
    private DateTime timer2;
    
    private bool isPoweredUp;
    
    public Vector2f size;
    
    //TODO: THE BALL KEEPS DIRECTION WHEN PLACED ON PADDLE AFTER LOSS
    //TODO: DONT KNOW HOW TO SOLVE
    

    public void Update (Ball ball, float deltaTime, Powerup powerup, Paddle paddle)
    {
        float speed = deltaTime * 300f;
        Vector2f newPos = sprite.Position;
        
        sprite.Scale = new Vector2f(sprite.Scale.X, sprite.Scale.Y);
        size = new Vector2f(sprite.GetGlobalBounds().Width, sprite.GetGlobalBounds().Height);
        
        HandleInput(ref newPos, speed);

        if (Collision.CircleRectangle(ball.sprite.Position, Ball.Radius, this.sprite.Position, size, out Vector2f hit))
        {
            ball.sprite.Position += hit;
            ball.Reflect(hit.Normalized());
        }
        

        foreach (Powerup pW in powerup.powerups.ToList())
        {
            if (Collision.CircleRectangle(pW.sprite.Position, Powerup.Radius, sprite.Position, size, out Vector2f shit))
            {
                powerup.powerups.Remove(pW);
                if (!isPoweredUp)
                {
                    IncreasePaddleSize(paddle);
                    isPoweredUp = true;
                    timer1 = DateTime.Now.AddSeconds(1);
                }
            }
        }

        timer2 = DateTime.Now.AddSeconds(1);
        double timer = (timer2 - timer1).TotalSeconds;
        
        if ((timer > 4) && isPoweredUp)
        {
            ResetPaddleSize();
            isPoweredUp = false;
        }
        
            
        void IncreasePaddleSize(Paddle paddle)
        {
            paddle.sprite.Scale = new Vector2f(paddle.sprite.Scale.X * 2f, paddle.sprite.Scale.Y);
        }

        void ResetPaddleSize()
        {
            Vector2f paddleTextureSize = (Vector2f) sprite.Texture.Size;
            sprite.Scale = new Vector2f(Diameter / paddleTextureSize.Y, Diameter / paddleTextureSize.Y);
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