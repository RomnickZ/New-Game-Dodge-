using GLGBRZYLFinal.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace GLGBRZYLFinal.Scenes
{
	internal class MenuState : State
	{
		private SpriteBatch _spriteBatch;
		private List<Component> components;
		private string gameTitle;

		public MenuState(Game1 game, GraphicsDevice device, ContentManager contentManager) : base(game, device, contentManager)
		{
			_spriteBatch = new SpriteBatch(device);

			int windowWidht = device.Viewport.Width;
			int windowHeight = device.Viewport.Height;

			Button playButton = new Button(contentManager.Load<Texture2D>("Controls/Button"), contentManager.Load<SpriteFont>("File"))
			{
				Position = new Vector2(windowWidht / 4, 170),
				Text = "Play"
			};
			playButton.Click += PlayButton_Click;

			Button helpButton = new Button(contentManager.Load<Texture2D>("Controls/Button"), contentManager.Load<SpriteFont>("File"))
			{
				Position = new Vector2(windowWidht / 2, 170),
				Text = "Help"
			};
			helpButton.Click += HelpButton_Click;

			Button aboutButton = new Button(contentManager.Load<Texture2D>("Controls/Button"), contentManager.Load<SpriteFont>("File"))
			{
				Position = new Vector2(windowWidht / 4, 300),
				Text = "About"
			};
			aboutButton.Click += AboutButton_Click;

			Button quitButton = new Button(contentManager.Load<Texture2D>("Controls/Button"), contentManager.Load<SpriteFont>("File"))
			{
				Position = new Vector2(windowWidht / 2, 300),
				Text = "Quit"
			};
			quitButton.Click += QuitButton_Click;

			components = new List<Component>()
			{
				quitButton, playButton, helpButton, aboutButton
			};

			gameTitle = "Dodge Guy";

		}

		private void QuitButton_Click(object sender, System.EventArgs e)
		{
			_game.Exit();
		}
		private void PlayButton_Click(object sender, System.EventArgs e)
		{
			_game.ChangeScene(new GameState(this._game,_device,_content));
		}


		private void HelpButton_Click(object sender, System.EventArgs e)
		{
			_game.ChangeScene(new HelpState(this._game,_device,_content));
		}

		private void AboutButton_Click(object sender, System.EventArgs e)
		{
			_game.ChangeScene(new AboutState(this._game,_device,_content));
		}


		public override void PostUpdate(GameTime gameTime)
		{
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var component in components)
			{
				component.Update(gameTime);
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			foreach (var component in components)
			{
				component.Draw(gameTime, spriteBatch);
			}
			SpriteFont font = _content.Load<SpriteFont>("File");
			

			spriteBatch.DrawString(_content.Load<SpriteFont>("Title"),gameTitle,new Vector2(310,50),Color.White);
			spriteBatch.End();
		}

		public override void LoadContent()
		{
			
		}
	}
}
