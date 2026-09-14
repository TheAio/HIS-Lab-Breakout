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
    
    public void Update(float deltaTime)
    {
        
    }
    public void Draw(RenderTarget target)
    {
        target.Draw(sprite);
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