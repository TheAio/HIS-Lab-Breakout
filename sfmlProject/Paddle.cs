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
    

    public void Update (float deltaTime)
    {
        float speed = deltaTime * 300f;
        Vector2f newPos = sprite.Position;
        
        HandleInput(ref newPos, speed);
    }

    public void Draw(RenderTarget target)
    {
        target.Draw(sprite);
    }

    //TODO: RE-EVALUATE POSITION CONDITIONS
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
    }
}