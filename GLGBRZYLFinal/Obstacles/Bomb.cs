using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLGBRZYLFinal.Obstacles
{
	public class Bomb : DrawableGameComponent
	{
		private Texture2D bombTexture;
		private Texture2D explosionTexture;
		private Vector2 position;
		private bool isExploded;
		private float explosionTimer;
		private float explosionDuration = 3.0f; // Duration of the explosion in seconds

		public Bomb(Game game, Texture2D bombTexture, Texture2D explosionTexture, Vector2 position) : base(game)
		{
			this.bombTexture = bombTexture;
			this.explosionTexture = explosionTexture;
			this.position = position;
			this.isExploded = false;
			this.explosionTimer = 0.0f;
		}

		public Vector2 GetPosition()
		{
			return position;
		}

		public void SetPosition(Vector2 newPosition)
		{
			position = newPosition;
		}

		public bool IsExploded
		{
			get { return isExploded; }
		}

		public void Reset()
		{
			isExploded = false;
			explosionTimer = 0.0f;
			// Set the bomb's position to be above the screen to prevent it from immediately falling
			SetPosition(new Vector2(-200, -200)); // Adjust these coordinates as needed
		}

		public void Update(GameTime gameTime, float fallSpeed)
		{
			if (!isExploded)
			{
				float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
				SetPosition(new Vector2(GetPosition().X, GetPosition().Y + fallSpeed * deltaTime));

				// Check if the bomb has reached the bottom
				if (GetPosition().Y > GraphicsDevice.Viewport.Height - bombTexture.Height)
				{
					// Bomb reached the bottom, trigger explosion
					isExploded = true;
				}
			}
			else
			{
				// Update explosion timer
				explosionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

				// Check if the explosion duration has been reached
				if (explosionTimer >= explosionDuration)
				{
					// Reset bomb state for a new bomb to be spawned
					Reset();
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!isExploded)
			{
				// Draw bomb if it hasn't exploded
				spriteBatch.Draw(bombTexture, position, Color.White);
			}
			else
			{
				// Draw explosion if the bomb has exploded
				spriteBatch.Draw(explosionTexture, new Vector2(position.X - bombTexture.Width - 10, position.Y - (explosionTexture.Height / 3)), Color.White);
			}
		}
		public Rectangle GetBounds()
		{
			// Adjust the bounding box size for the bomb
			int boundingBoxWidth = bombTexture.Width;
			int boundingBoxHeight = bombTexture.Height;

			// Calculate the bounding box position
			int boundingBoxX = (int)(position.X);
			int boundingBoxY = (int)(position.Y);

			return new Rectangle(boundingBoxX, boundingBoxY, boundingBoxWidth, boundingBoxHeight);
		}
	}
}
