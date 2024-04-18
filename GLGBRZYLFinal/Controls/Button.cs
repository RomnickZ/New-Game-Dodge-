using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLGBRZYLFinal.Components
{
    internal class Button : Component
	{
		private MouseState _currentMS;
		private MouseState _previousMS;
		private SpriteFont _font;
		private bool _isHovering;
		private Texture2D _buttonTex;

		public event EventHandler Click;
		public bool Clicked { get; private set; }	
		public Color Color { get; private set; }

		public Vector2 Position { get; set; }

		public Rectangle Rectangle //Gets the bounds of the utton
		{
			get
			{
				return new Rectangle((int)Position.X,(int)Position.Y,_buttonTex.Width, _buttonTex.Height);
			}
		}

		public string Text { get; set; }


		public Button(Texture2D texture, SpriteFont font)
		{
			_font = font;
			_buttonTex = texture;
			Color = Color.Black;
		}


		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			var color = Color.White;

			if (_isHovering)
			{
				color = Color.Gray;
			}

			spriteBatch.Draw(_buttonTex,Rectangle,color);

			if (!string.IsNullOrEmpty(Text))
			{
				var x = (Rectangle.X + (Rectangle.Width/2) - (_font.MeasureString(Text).X/2));
				var y = (Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(Text).Y / 2));

				spriteBatch.DrawString(_font, Text, new Vector2(x, y), Color);
			}

		}

		public override void Update(GameTime gameTime)
		{
			_previousMS = _currentMS;
			_currentMS = Mouse.GetState();

			var mouseRect = new Rectangle(_currentMS.X, _currentMS.Y, 1, 1);

			_isHovering = false;


			//Hovering effect and click event
			if (mouseRect.Intersects(Rectangle)) 
			{
				_isHovering = true;

				if(_currentMS.LeftButton == ButtonState.Pressed && _previousMS.LeftButton == ButtonState.Released)
				{
					Click?.Invoke(this, new EventArgs());
				}
			}
		}
	}
}
