using GLGBRZYLFinal.Components;
using GLGBRZYLFinal.Obstacles;
using GLGBRZYLFinal.PlayerComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GLGBRZYLFinal.Scenes
{
	internal class GameState : State
	{
		//Misc
		private SpriteBatch _spriteBatch;
		private Texture2D backgroundImg;
		private Random random;
		private SpriteFont timerText;
		private float timer;
		private SpriteFont timerLvl2Text;
		private float timerLvl2;
		private bool isContentLoaded;
		private SoundEffect bgm1;

		//Player
		private Player player;
		private CollisionManager collisionManager;
		public SpriteFont lives;
		private SoundEffect jumpSound;
		private Texture2D playerTexture;
		private Vector2 playerPos;
		private Vector2 playerVelocity;
		private SoundEffect hitSound;
		private SoundEffect gameOverEffect;

		//Stage
		private Vector2 stage;

		//Bomb
		private float bombSpawnTimer;
		private float bombSpawnInterval = 5f; // Bomb spawns every {bombSpawnInterval} seconds
		private float bombFallSpeed = 200f;
		private List<Bomb> bombs;
		private Texture2D bombTexture;
		private Texture2D explosionTexture;

		//Blade
		private Texture2D bladeTex;
		private Vector2 bladePos;
		private Vector2 bladeSpeed;
		private List<Blade> blades;
		private float bladeSpawnTimer;
		private float bladeSpawnInterval = 1;

		public GameState(Game1 game, GraphicsDevice device, ContentManager contentManager) : base(game, device, contentManager)
		{
			random = new Random();

			bombs = new List<Bomb>();
			random = new Random();
			blades = new List<Blade>();

			
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			_device.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();

			// Draw background
			_spriteBatch.Draw(backgroundImg, Vector2.Zero, Color.White);

			// Draw Lives
			string livesText = (player.lives < 0) ? $"Lives: 0" : $"Lives: {player.lives}";

			_spriteBatch.DrawString(lives, livesText, new Vector2(50, 30), Color.Black);

			_spriteBatch.DrawString(timerText, timer.ToString("N2"), new Vector2(400, 30), Color.Black);


			// Draw bombs and explosions
			foreach (Bomb bomb in bombs)
			{
				bomb.Draw(_spriteBatch);
			}

			foreach (Blade blade in blades)
			{
				blade.Draw(_spriteBatch);
			}
			_spriteBatch.End();

			if (player.lives > 0)
			{
				player.Draw(gameTime);

			}

		}

		public override void LoadContent()
		{
			isContentLoaded = true;
			_spriteBatch = new SpriteBatch(_device);

			//Load Blade
			bladeTex = _content.Load<Texture2D>("Blade");

			//Load Sounds
			jumpSound = _content.Load<SoundEffect>("jumpSound");
			hitSound = _content.Load<SoundEffect>("HitSound");
			gameOverEffect = _content.Load<SoundEffect>("gameOver");
			bgm1 = _content.Load<SoundEffect>("BMG");



			//Load lives spriteFont
			lives = _content.Load<SpriteFont>("lives");
			timerText = _content.Load<SpriteFont>("lives");
			timerLvl2Text = _content.Load<SpriteFont>("lives");

			// Load images
			backgroundImg = _content.Load<Texture2D>("backgroundImg");
			bombTexture = _content.Load<Texture2D>("Bomb");
			explosionTexture = _content.Load<Texture2D>("Explosion");

			// Load player texture
			playerTexture = _content.Load<Texture2D>("Player");

			// Set up player
			playerPos = new Vector2(_game.GraphicsDevice.Viewport.Width / 2 - playerTexture.Width / 2,
										   _game.GraphicsDevice.Viewport.Height - playerTexture.Height / 2);
			playerVelocity = new Vector2();
			stage = new Vector2(_game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height);
			player = new Player(_game, _spriteBatch, playerTexture, playerPos, playerVelocity, stage, true, jumpSound);
			_game.Components.Add(player);

			// Set up collision manager
			collisionManager = new CollisionManager(_game, player, bombs, blades, hitSound);
			_game.Components.Add(collisionManager);
		}

		public override void PostUpdate(GameTime gameTime)
		{
		}

		public override void Update(GameTime gameTime)
		{
			if(!isContentLoaded) 
			{ 
				LoadContent();
				bgm1.Play(0.05f,0,0);
				
			}
			
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				_game.ChangeScene(new MenuState(this._game, _device, _content));
				bgm1.Dispose();
			}

			if (timer > 30)
			{
				UpdateBombs(gameTime);
			}

			UpdateBlade(gameTime);
			player.Update(gameTime, jumpSound);

			if(player.lives<=0)
			{
				gameOverEffect.Play();
				_game.ChangeScene(new ScoreState(_game, _device, _content,timer));
				bgm1.Dispose();

			}
			if (timer > 180)
			{
				bgm1.Dispose();
				bgm1.Play();
			}

			timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
			timerLvl2 += (float)gameTime.ElapsedGameTime.TotalSeconds;

			
		}

		private void UpdateBombs(GameTime gameTime)
		{
			bombSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (bombSpawnTimer > bombSpawnInterval)
			{
				bombSpawnTimer = 0;
				
				SpawnBomb();
				if (timer < 60)
				{
					bombSpawnInterval = random.Next(2, 5);

				}
				else
				{
					bombSpawnInterval = random.Next(1, 3);
				}
			}

			for (int i = bombs.Count - 1; i >= 0; i--)
			{
				Bomb bomb = bombs[i];
				bomb.Update(gameTime, bombFallSpeed);

				if (bomb.GetPosition().Y > _device.Viewport.Height && bomb.IsExploded)
				{
					// Bomb reached the bottom and exploded, reset its state and position, and spawn a new bomb
					bomb.Reset();
					SpawnBomb();
				}
			}


		}

		private void SpawnBomb()
		{
			
			float spawnX = random.Next(_device.Viewport.Width - bombTexture.Width);
			bombs.Add(new Bomb(_game, bombTexture, explosionTexture, new Vector2(spawnX, random.Next(-200, -20))));

		}

		private void UpdateBlade(GameTime gameTime)
		{


			bladeSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (bladeSpawnTimer > bladeSpawnInterval)
			{
				bladeSpawnTimer = 0;
				SpawnBlade();
				SpawnBlade();
			}

			foreach (Blade blade in blades)
			{
				blade.Update(gameTime);

			}
			for (int i = 0; i < blades.Count - 1; i++)
			{
				Blade blade = blades[i];
				if (blade.hasFallen)
				{
					blades.Remove(blade);
				}
			}

		}

		private void SpawnBlade()
		{
			bladeSpawnInterval = 1;

			float spawnX = random.Next(_device.Viewport.Width - bombTexture.Width);
			bladePos.X = spawnX;
			bladePos.Y = random.Next(-200, -20);
			bladeSpeed = new Vector2(0, 3);

			blades.Add(new Blade(_game, bladeTex, bladePos, bladeSpeed, stage));

		}
	}
}
