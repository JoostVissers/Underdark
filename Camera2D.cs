
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Underdark;

public class Camera2D : ICamera2D
{
    private Vector2 _position;
    protected float _viewportHeight;
    protected float _viewportWidth;
    Viewport viewport;

    public Camera2D(Viewport viewport)
    {
        this.viewport = viewport;
    }

    #region Properties

    public Vector2 Position
    {
        get { return _position; }
        set { _position = value; }
    }
    public float Rotation { get; set; }
    public Vector2 Origin { get; set; }
    public float Scale { get; set; }
    public Vector2 ScreenCenter { get; protected set; }
    public Matrix Transform { get; set; }
    public IFocusable Focus { get; set; }
    public float MoveSpeed { get; set; }

    #endregion

    /// <summary>
    /// Called when the GameComponent needs to be initialized. 
    /// </summary>
    public void Initialize()
    {
        _viewportWidth = viewport.Width;
        _viewportHeight = viewport.Height;

        ScreenCenter = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
        Scale = 3;
        MoveSpeed = 10.0f;
    }

    public void Update(GameTime gameTime)
    {
        // Create the Transform used by any
        // spritebatch process
        Transform = Matrix.Identity *
                    Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                    Matrix.CreateScale(new Vector3(Scale, Scale, Scale));

        Origin = ScreenCenter / Scale;

        // Move the Camera to the position that it needs to go
        var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _position.X += (Focus.Position.X - Position.X) * MoveSpeed * delta;
        _position.Y += (Focus.Position.Y - Position.Y) * MoveSpeed * delta;

    }

    /// <summary>
    /// Determines whether the target is in view given the specified position.
    /// This can be used to increase performance by not drawing objects
    /// directly in the viewport
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="texture">The texture.</param>
    /// <returns>
    ///     <c>true</c> if [is in view] [the specified position]; otherwise, <c>false</c>.
    /// </returns>
    public bool IsInView(Vector2 position, Texture2D texture)
    {
        // If the object is not within the horizontal bounds of the screen

        if ((position.X + texture.Width) < (Position.X - Origin.X) || (position.X) > (Position.X + Origin.X))
        {
            return false;
        }
        // If the object is not within the vertical bounds of the screen
        if ((position.Y + texture.Height) < (Position.Y - Origin.Y) || (position.Y) > (Position.Y + Origin.Y))
        {
            return false;
        }
           

        // In View
        return true;
    }
}