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
    List<String> textures;
    List<Boolean> isLiving;
    //LinkedList<String> textures;

    private Vector2f size;
    
    
    public const float Diameter = 20f;
    public const float Radius = Diameter * 0.5f;

    public void Update(Tiles tile, Ball ball, float deltaTime, Pause pause, Paddle paddle, Powerup powerup)
    {
        if (ball.score > 2499)
        {
            pause.isPaused = true;
            paddle.sprite.Position = new Vector2f((GraphicsSettings.ScreenW / 2f), 650f);
        }
        
        for (int i = 0; i < isLiving.Count; i++)
        {
            var pos = positions[i];
            if (isLiving[i] && Collision.CircleRectangle(ball.sprite.Position, Ball.Radius, pos, size, out Vector2f hit))
            {
                ball.sprite.Position += hit;
                ball.Reflect(hit.Normalized());
                powerup.SpawnPowerUp(positions[i]);
                isLiving[i] = false;
                //positions.RemoveAt(i);
                //i = 0; i är ju deklarerat i for loopen varje frame ändå? är inte i reduntant här? loopen körs ju ändå varje frame vilket innebär att 
                ball.score += 100;
            }
        }
    }
    
    public void Draw(RenderTarget target)
    {
        for (int i = 0; i < isLiving.Count; i++)
        {
            if (isLiving[i])
            {
                sprite.Position = positions[i];
                sprite.Texture = new Texture(textures[i]);
                target.Draw(sprite);
            }
            /*int roll = seed.Next(0, 2);
            if (roll == 0)
            {
                sprite.Color = Color.Red;
                
            }
            else if (roll == 1)
            {
                sprite.Color = Color.Yellow;
            }*/
        }
    }

    public void CreateTiles()
    {
        Random seed = new();
        positions.Clear();
        textures.Clear();
        isLiving.Clear();
        /*for (int i = 0; i <= 25; i++)
        {
        }*/
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                Vector2f pos = new Vector2f(GraphicsSettings.ScreenW * 0.5f + i * 96.0f, GraphicsSettings.ScreenH * 0.3f + j * 48.0f);
                positions.Add(pos);
                int roll = seed.Next(0,3);
                textures.Add(tiles[roll]);
                isLiving.Add(true);
            }
        }
    }



    public Tiles()
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("assets/tileBlue.png");
        positions = new List<Vector2f>();
        textures = new List<string>();
        isLiving = new List<bool>();
        
        Vector2f tileTextureSize = (Vector2f)sprite.Texture.Size;
        sprite.Origin = 0.5f * tileTextureSize;
        sprite.Scale = new Vector2f(Diameter / tileTextureSize.Y, Diameter / tileTextureSize.Y);
        size = new Vector2f(sprite.GetGlobalBounds().Width, sprite.GetGlobalBounds().Height);
        
        CreateTiles();
    }
}