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


    public Tiles()
    {
        sprite.Texture = new Texture(tiles[seed.Next(0, tiles.Length)]);
        positions = new List<Vector2f>();
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