using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
namespace sfmlProject;

public class Ball
{
    public Sprite sprite;
    public const float Diameter = 20f;
    public const float Radius = Diameter * 0.5f;
    
    public Vector2f direction = new Vector2f(1,1) / MathF.Sqrt(2.0f);
    
    public void Update(float deltaTime)
    {
        float speed = deltaTime * 100.0f;
        Vector2f newPos = sprite.Position;
        newPos += direction * speed;
        CheckAndReflect(ref newPos);
        sprite.Position = newPos;
    }

    public void CheckAndReflect(ref Vector2f newPos)
    {
        // Paddle physics
        /*
        paddleBoundsMaxX = paddle.X + paddle.radius
        paddleBoundsMinX = paddle.X - paddle.radius
        if (newPos.X > paddleBoundsMinX && newPos.X < paddleBoundsMaxX){
            
        }
         */
        // Map boundaries
        if (newPos.X > GraphicsSettings.ScreenW - Radius)
        {
            newPos.X = GraphicsSettings.ScreenW - Radius;
            Reflect(new Vector2f(-1, 0));
        } else if (newPos.X < Radius)
        {
            newPos.X = Radius;
            Reflect(new Vector2f(1, 0));
        } else if (newPos.Y > GraphicsSettings.ScreenH - Radius)
        {
            newPos.Y = GraphicsSettings.ScreenH - Radius;
            Reflect(new Vector2f(0, -1));
        }
        else if (newPos.Y < Radius)
        {
            newPos.Y = Radius;
            Reflect(new Vector2f(0, 1));
        }
    }

    public void Draw(RenderTarget target)
    {
        target.Draw(sprite);
    }

    public void Reflect(Vector2f normal)
    {
        direction -= normal * ( 2 * (direction.X * normal.X + direction.Y * normal.Y) );
        /*
         Calculates the mathematically correct reflection vector based on the normal vector,
         in a box this is overkill, as there are only 2 predictable reflections but this is mathematically correct.
         */
    }
    
    public Ball()
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("assets/ball.png");
        sprite.Position = new Vector2f(250, 300);
        Vector2f ballTextureSize = (Vector2f) sprite.Texture.Size;
        sprite.Origin = 0.5f * ballTextureSize;
        sprite.Scale = new Vector2f(Diameter / ballTextureSize.X, Diameter / ballTextureSize.Y);
        
    }
}