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
	public class HelpState : State
	{
		private SpriteBatch _spriteBatch;
		//Font
		private SpriteFont _font;
		private string _controls;
		private string _objective;

		//Button
		private Button _returnBtn;
		

		public HelpState(Game1 game, GraphicsDevice device, ContentManager contentManager) : base(game, device, contentManager)
		{
			_spriteBatch = new SpriteBatch(device);

			int windowWidht = device.Viewport.Width;
			int windowHeight = device.Viewport.Height;

			_controls = $"CONTROLS: \n" +
				$"Use arrows keys/WASD to move character \n" +
				$"Press Space bar to jump";
			_objective = $"OBJECTIVE: \n" +
				$"Dodge blades and bombs. Survive as long as you can!";
			_returnBtn = new Button(_content.Load<Texture2D>("Controls/Button"), _content.Load<SpriteFont>("lives"))
			{
				Position = new Vector2(300, 180),
				Text = "Return"
			};
			_returnBtn.Click += ReturnButton_Click;
			

		}

		private void ReturnButton_Click(object sender, EventArgs e)
		{
			_game.ChangeScene(new MenuState(this._game,_device,_content));
		}

		public override void LoadContent()
		{
			_font = _content.Load<SpriteFont>("lives");

		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			spriteBatch.DrawString(_content.Load<SpriteFont>("File"),_controls,new Vector2(30,300),Color.Black);
			spriteBatch.DrawString(_content.Load<SpriteFont>("File"), _objective, new Vector2(400, 300), Color.Black);
		
			_returnBtn.Draw(gameTime, spriteBatch);
			spriteBatch.End();
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
