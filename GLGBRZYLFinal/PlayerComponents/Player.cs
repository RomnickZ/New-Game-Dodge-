using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GLGBRZYLFinal.PlayerComponent
{
	public class Player : DrawableGameComponent
	{
		private SpriteBatch spriteBatch;
		private Texture2D tex;
		private Vector2 position;
		private Vector2 velocity;
		private float jumpForce = 380f; // Adjust this value to control the jump height
		private Vector2 stage;
		private bool isGrounded;
		private bool canJump = true;
		private float gravity = 600f; // Adjust this value to control the fall speed
		private float speed = 200f;
		public int lives = 3;
		private SoundEffect jumpSound;

		public Vector2 Velocity { get => velocity; set => velocity = value; }
		public Vector2 Position { get => position; set => position = value; }

		public Player(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 velocity, Vector2 stage, bool isGrounded, SoundEffect jumpSound) : base(game)
		{
			this.spriteBatch = spriteBatch;
			this.tex = tex;
			this.position = position;
			this.velocity = velocity;
			this.stage = stage;
			this.isGrounded = isGrounded;
		}

		public void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(tex, position, Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}

		public void Update(GameTime gameTime, SoundEffect jumpSound)
		{
			KeyboardState ks = Keyboard.GetState();

			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			// Horizontal movement
			if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
			{
				velocity.X = -speed;
				if (position.X <= 0)
				{
					position.X = 0;
				}
			}
			else if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
			{
				velocity.X = speed;
				if (position.X + tex.Width >= stage.X)
				{
					position.X = stage.X - tex.Width;
				}
			}
			else
			{
				velocity.X = 0;
			}

			// Vertical movement (jumping and falling)
			if (ks.IsKeyDown(Keys.Space) && isGrounded && canJump)
			{
				velocity.Y = -jumpForce;
				isGrounded = false;
				jumpSound.Play();
			}

			velocity.Y += gravity * deltaTime;

			// Update position
			position += velocity * deltaTime;

			// Collision with the ground
			if (position.Y + tex.Height > stage.Y)
			{
				position.Y = stage.Y - tex.Height;
				isGrounded = true;
				canJump = true;
			}
			else
			{
				canJump = false;
			}

			base.Update(gameTime);
		}

		public Rectangle GetBounds()
		{
			return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
		}
	}
}