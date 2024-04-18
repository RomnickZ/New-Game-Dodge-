using GLGBRZYLFinal.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLGBRZYLFinal.Scenes
{
	public class AboutState : State
	{
		private SpriteBatch _spriteBatch;
		//Font
		private SpriteFont _font;
		private string _text;

		//Button
		private Button _returnBtn;

		public AboutState(Game1 game, GraphicsDevice device, ContentManager contentManager) : base(game, device, contentManager)
		{
			_spriteBatch = new SpriteBatch(device);

			_returnBtn = new Button(_content.Load<Texture2D>("Controls/Button"), _content.Load<SpriteFont>("lives"))
			{
				Position = new Vector2(300, 180),
				Text = "Return"
			};
			_returnBtn.Click += ReturnButton_Click;

			_text = $"Developers: \n\n" +
				$"Gaspar Lourenco - 8734715\n" +
				$"Gurlal Singh Brar - 8869786\n" +
				$"Romnick Zinampam - 8834187\n" +
				$"Youssef Lemfeddel - 8867203";


		}

		private void ReturnButton_Click(object sender, EventArgs e)
		{
			_game.ChangeScene(new MenuState(this._game, _device, _content));
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			_returnBtn.Draw(gameTime, spriteBatch);
			spriteBatch.DrawString(_content.Load<SpriteFont>("File"), _text, new Vector2(270, 300), Color.Black);

			spriteBatch.End();
		}

		public override void LoadContent()
		{
			
		}

		public override void PostUpdate(GameTime gameTime)
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			_returnBtn.Update(gameTime);
		}
	}
}
