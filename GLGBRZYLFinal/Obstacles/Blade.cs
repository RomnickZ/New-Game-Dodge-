using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLGBRZYLFinal.Obstacles
{
	public class Blade : DrawableGameComponent
	{
		//Blade Obstacle

		private Texture2D tex;
		private Vector2 pos;
		private Vector2 speed;
		private Vector2 stage;
		public bool hasFallen;


		public Blade(Game game, Texture2D texture, Vector2 position, Vector2 speed, Vector2 stage) : base(game)
		{
			this.tex = texture;
			this.pos = position;
			this.speed = speed;
			this.stage = stage;
		}


		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(tex, pos, Color.White);

		}

		public void Update(GameTime gameTime)
		{
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			pos += speed;

			
			if (pos.Y + tex.Height / 2 > stage.Y)
			{
				//This variable decides if the blade is ready to be destroyed.
				hasFallen = true;
			}


			base.Update(gameTime);
		}

		public void DeSpawn()
		{   
			//This variable decides if the blade is ready to be destroyed.
			hasFallen = true;
		}



		public Rectangle GetBounds()
		{
			int bladeBoundWidth = tex.Width;
			int bladeBoundHeight = tex.Height;

			int boundingBoxX = (int)(pos.X);
			int boundingBoxY = (int)(pos.Y);

			return new Rectangle(boundingBoxX, boundingBoxY, bladeBoundWidth, bladeBoundHeight);

		}

	}
}
