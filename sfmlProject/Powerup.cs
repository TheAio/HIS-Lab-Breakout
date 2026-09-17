using SFML.Graphics;
using SFML.System;

namespace sfmlProject;

public class Powerup
{
    public Sprite sprite;
    public const float Diameter = 20f;
    public const float Radius = Diameter * 0.5f;
    Vector2f size;
    Random seed = new();

    private List<Powerup> powerups = new();

    public Powerup()
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("assets/paddle.png");
        // sprite.Color = Color.Black;
        // sätt sprite position till origin från destroyed tile
        
        Vector2f powerupTextureSize = (Vector2f)sprite.Texture.Size;
        sprite.Origin = 0.5f * powerupTextureSize;
        sprite.Scale = new Vector2f(Diameter / powerupTextureSize.X, Diameter / powerupTextureSize.Y);
        sprite.Position = new Vector2f();
        
        size = new Vector2f(sprite.GetGlobalBounds().Width, sprite.GetGlobalBounds().Height);
    }

    public void Update(float deltaTime)
    {
        //if (Collision.CircleRectangle(sprite.Origin, Powerup.Radius,))
        foreach (Powerup powerup in powerups)
        {
            powerup.sprite.Position += new Vector2f(0,100f) * deltaTime;
        }
    }

    public void Draw(RenderTarget target)
    {
        foreach (Powerup powerup in powerups)
        {
            target.Draw(powerup.sprite);
        }
    }
    public void SpawnPowerUp(Vector2f spawnPosition)
    {
        if (seed.Next(0,9) == 0)
        {
            powerups.Add(new Powerup());
            powerups[powerups.Count-1].sprite.Position = spawnPosition;
        }
    }
}