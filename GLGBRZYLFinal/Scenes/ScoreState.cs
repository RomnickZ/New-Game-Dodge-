using GLGBRZYLFinal.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GLGBRZYLFinal.Scenes
{
	public class ScoreState : State
	{
		private SpriteBatch _spriteBatch;
		//Font
		private SpriteFont _font;
		private string _text;

		//Button
		private Button _returnBtn;

		public ScoreState(Game1 game, GraphicsDevice device, ContentManager contentManager,float score) : base(game, device, contentManager)
		{
			_spriteBatch = new SpriteBatch(device);

			_returnBtn = new Button(_content.Load<Texture2D>("Controls/Button"), _content.Load<SpriteFont>("lives"))
			{
				Position = new Vector2(300, 180),
				Text = "Return"
			};
			_returnBtn.Click += ReturnButton_Click;

			_text = $"You Died! \n" +
				$"Score: {(score * 300).ToString("N2")}";
		}

		private void ReturnButton_Click(object sender, EventArgs e)
		{
			_game.ChangeScene(new MenuState(this._game, _device, _content));
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			_returnBtn.Draw(gameTime, spriteBatch);
			spriteBatch.DrawString(_content.Load<SpriteFont>("File"), _text, new Vector2(320, 300), Color.Black);

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
