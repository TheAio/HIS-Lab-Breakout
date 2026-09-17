using SFML.Graphics;
using SFML.System;

namespace sfmlProject;

public class Powerup
{
    public Sprite sprite;
    public const float Diameter = 20f;
    public const float Radius = Diameter * 0.5f;

    public Powerup()
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("Images/ball.png");
        sprite.Color = Color.Black;
        // sätt sprite position till origin från destroyed tile
        
        Vector2f powerupTextureSize = (Vector2f)sprite.Texture.Size;
        sprite.Origin = 0.5f * powerupTextureSize;
        sprite.Scale = new Vector2f(Diameter / powerupTextureSize.X, Diameter / powerupTextureSize.Y);
    }
}