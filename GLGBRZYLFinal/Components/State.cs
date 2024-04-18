using GLGBRZYLFinal.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLGBRZYLFinal.Components
{
	public abstract class State
	{
		//State Class to manage all states of the game.
		protected ContentManager _content;

		protected GraphicsDevice _device;

		protected Game1 _game;


		public abstract void Draw(GameTime gameTime,SpriteBatch spriteBatch);

		public abstract void PostUpdate(GameTime gameTime);

		public State(Game1 game, GraphicsDevice device, ContentManager contentManager)
		{
			_game = game;
			_device = device;
			_content = contentManager;
		}

		public abstract void Update(GameTime gameTime);

		public abstract void LoadContent();

	}
}
