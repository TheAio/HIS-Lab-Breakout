using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
namespace sfmlProject;

public class Tiles
{
    public Sprite sprite;
    string[] tiles = new string[3]{"assets/tileBlue.png", "assets/tileGreen.png", "assets/tilePink.png"};
    Random seed = new();
    List<Vector2f> positions;
    
    
    public const float Diameter = 20f;
    public const float Radius = Diameter * 0.5f;

    public void Draw(RenderTarget target)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            sprite.Position = positions[i];
            int roll = seed.Next(0,3);
            /*if (true)
            {
                switch (roll)
                {
                    case 0:
                        sprite.Color = new Color(Color.Blue);
                        break;
                    case 1:
                        sprite.Color = new Color(Color.Green);                        
                        break;
                    case 2:
                        sprite.Color = new Color(Color.Cyan);
                        break;
                }
            }*/
            target.Draw(sprite);
        }
    }

    public Tiles()
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("assets/tileBlue.png");
        positions = new List<Vector2f>();
        
        Vector2f tileTextureSize = (Vector2f)sprite.Texture.Size;
        sprite.Origin = 0.5f * tileTextureSize;
        sprite.Scale = new Vector2f(Diameter / tileTextureSize.Y, Diameter / tileTextureSize.Y);
        
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                Vector2f pos = new Vector2f(GraphicsSettings.ScreenW * 0.5f + i * 96.0f, GraphicsSettings.ScreenH * 0.3f + j * 48.0f);
                positions.Add(pos);
            }
        }
    }
}