using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLGBRZYLFinal.PlayerComponent;
using GLGBRZYLFinal.Obstacles;

namespace GLGBRZYLFinal.Components
{
    public class CollisionManager:GameComponent
	{
		private Player player;
		private List<Bomb> bombs;
		private List<Blade> blades;
		private SoundEffect hitSound;

		public CollisionManager(Game game, Player player, List<Bomb> bombs, List<Blade> blades, SoundEffect hitSound) : base(game)
		{
			this.player = player;
			this.bombs = bombs;
			this.blades = blades;
			this.hitSound = hitSound;
		}

		public override void Update(GameTime gameTime)
		{
			Rectangle playerRect = player.GetBounds();

			foreach (Bomb bomb in bombs)
			{
				Rectangle bombRect = bomb.GetBounds();

				// Check for collision between player and bomb
				if (playerRect.Intersects(bombRect))
				{
					// Collision occurred, decrement player lives and reset bomb
					player.lives--;
					if (player.lives > 0)
					{
						hitSound.Play();
					}
					// Optionally, you can add more logic here, such as playing a sound or showing an animation

					bomb.Reset();
				}
			}

			foreach (Blade blade in blades)
			{
				Rectangle bombRect = blade.GetBounds();

				// Check for collision between player and bomb
				if (playerRect.Intersects(bombRect) && !blade.hasFallen)
				{
					// Collision occurred, decrement player lives and reset bomb
					player.lives--;
					if (player.lives > -1)
					{
						hitSound.Play();
					}

					blade.DeSpawn();
					Debug.WriteLine("Blade Hit");

				}
			}




			base.Update(gameTime);
		}
	}
}

