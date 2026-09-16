using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
namespace sfmlProject;

public class Ball
{
    public Sprite sprite;
    //private Tiles tiles = new();
    public const float Diameter = 20f;
    public const float Radius = Diameter * 0.5f;
    public int health = 3;
    public int score = 0;
    public Text gui;
    public bool isBallIdle = true;
    public float speed;
    Random seed = new();
    //public float roll;
    
    public Vector2f direction = new Vector2f(1,1) / MathF.Sqrt(2.0f);
    
    public void Update(float deltaTime, Paddle paddle, RenderWindow window, Tiles tiles)
    {
        CheckHealth(tiles);
        Vector2f newPos = sprite.Position;
        if (isBallIdle)
        {
            speed = deltaTime * 0f;
            newPos = paddle.sprite.Position - new  Vector2f(0, 22);
            window.KeyPressed += (o, e) =>
            {
                if (e.Code == Keyboard.Key.Space && isBallIdle) //TODO: Fråga cissi om varför vi behöver felhantera med en extra bool
                {
                    if (seed.Next(0, 2) == 0)
                    {
                        direction.X = -1;
                    }
                    else
                    {
                        direction.X = 1;
                    }
                    /*roll = seed.Next(-1, 2);
                    if (roll > -0.1f || roll < 0.1f)
                    {
                        roll += 0.2f;
                    }    
                    //TODO : Fråga cissi om detta, vi kunde inte lösa med hjälp av classmates
                    direction.X = roll;
                    direction.Y = MathF.Sqrt(direction.X*2 - (roll * roll));
                    direction = direction / MathF.Sqrt(2.0f);*/
                    isBallIdle = false;
                }
            };
            
        }
        else
        {
            speed = deltaTime * 500.0f;
        }
        newPos += direction * speed;
        CheckAndReflect(ref newPos, paddle);
        sprite.Position = newPos;
    }

    public void CheckAndReflect(ref Vector2f newPos, Paddle paddle)
    {
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
            //If we touch the bottom, reduce health and reset ball.
            health--;
            newPos.Y = GraphicsSettings.ScreenH - Radius;
            //newPos = new Vector2f(250, 300);
            newPos = paddle.sprite.Position - new  Vector2f(0, 5);
            //float speed = deltaTime * 100.0f;
            isBallIdle = true;
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
        gui.DisplayedString = $"Health: {health}";
        gui.Position = new Vector2f(12, 8);
        target.Draw(gui);
        gui.DisplayedString = $"Score: {score}";
        gui.Position = new Vector2f(GraphicsSettings.ScreenW - gui.GetGlobalBounds().Width - 12, 8);
        target.Draw(gui);
    }

    public void Reflect(Vector2f normal)
    {
        direction -= normal * ( 2 * (direction.X * normal.X + direction.Y * normal.Y) );
        /*
         Calculates the mathematically correct reflection vector based on the normal vector,
         in a box this is overkill, as there are only 2 predictable reflections but this is mathematically correct.
         */
    }

    public void CheckHealth(Tiles tiles)
    {
        if (health <= 0)
        {
            health = 3;
            score = 0;
            
            tiles.CreateTiles();
        }
    }
    
    public Ball()
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("assets/ball.png");
        sprite.Position = new Vector2f(250, 300);
        Vector2f ballTextureSize = (Vector2f) sprite.Texture.Size;
        sprite.Origin = 0.5f * ballTextureSize;
        sprite.Scale = new Vector2f(Diameter / ballTextureSize.X, Diameter / ballTextureSize.Y);
        gui = new Text();
        gui.CharacterSize = 24;
        gui.Font = new Font("assets/future.ttf");
    }
}