using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FantasySmash
{

	public class Game1 : Microsoft.Xna.Framework.Game
	{
		Texture2D skeleton;
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		float timer = 0f;
		float interval = 100f;
		int currentFrame = 1;
		public int maxFrame;
		public int minFrame;
		public bool moving;
		public int lastPos;
		int spriteWidth = 24;
		int spriteHeight = 34;
		Rectangle sourceRect;
		Vector2 origin;
		Vector2 charPos = new Vector2(100,200);

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			
			// TODO: Add your initialization logic here
			this.graphics.PreferredBackBufferWidth = 1200;
			this.graphics.PreferredBackBufferHeight = 900;
			this.graphics.ApplyChanges();
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			Content.RootDirectory = "Content";
			skeleton = Content.Load<Texture2D>("characters");
            IsMouseVisible = true;
			
			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		private void checkPlayerDeath()
		{
			/*foreach (Enemy enemy in EnemyManager.Enemies)
			{
				if (enemy.EnemyBase.IsCircleColliding(
					Player.BaseSprite.WorldCenter,
					Player.BaseSprite.CollisionRadius))
				{
					gameState = GameStates.GameOver;
				}
			}*/
		}

		protected override void Update(GameTime gameTime)
		{

			//Increase the timer by the number of milliseconds since update was last called
			timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

			//Check the timer is more than the chosen interval
			if (timer > interval)
			{
				//Show the next frame
				//
				//Reset the timer
				timer = 0f;
			}
			//If we are on the last frame, reset back to the one before the first frame (because currentframe++ is called next so the next frame will be 1!)
			
			if (currentFrame == maxFrame)
			{
				currentFrame = minFrame;
			}

			sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
			origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

			handleKeyboardMovement(Keyboard.GetState());
			base.Update(gameTime);
		}



		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			spriteBatch.Begin();

			//Draw the sprite in the centre of an 800x600 screen
			spriteBatch.Draw(skeleton, new Vector2(charPos.X, charPos.Y), sourceRect, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);

			spriteBatch.End();

			base.Draw(gameTime);
		}

		protected virtual void handleKeyboardMovement(KeyboardState keyState)
		{
			Keys[] currentPressedKeys = keyState.GetPressedKeys();

			/*if (keyState.IsKeyDown(Keys.Down))
			{

				charPos.Y++;
				minFrame = 1;
				maxFrame = 3;
				lastPos = 1;
				moving = true;
			}
			
			else if (keyState.IsKeyDown(Keys.Up))
			{
				charPos.Y--;
				minFrame = 12;
				maxFrame = 15;
				lastPos = 12;
				moving = true;

			}

			else if (keyState.IsKeyDown(Keys.Left))
			{
				maxFrame = 9;
				minFrame = 6;
				lastPos = 6;
				moving = true;
				charPos.X--;
			}
			else if (keyState.IsKeyDown(Keys.Right))
			{
				maxFrame = 21;
				minFrame = 18;
				lastPos = 18;
				moving = true;
				charPos.X++;
			}
			else
			{
				moving = false;
			}*/
				
			foreach (Keys key in currentPressedKeys)
			{
				if (key == Keys.W)
				{
					charPos.Y--;
					minFrame = 12;
					maxFrame = 15;
					lastPos = 12;
					moving = true;
				}
				else if (key == Keys.S)
				{
					charPos.Y++;
					minFrame = 1;
					maxFrame = 3;
					lastPos = 1;
					moving = true;
				}
				else if (key == Keys.A)
				{
					maxFrame = 9;
					minFrame = 6;
					lastPos = 6;
					moving = true;
					charPos.X--;
				}
				else if (key == Keys.D)
				{
					maxFrame = 21;
					minFrame = 18;
					lastPos = 18;
					moving = true;
					charPos.X++;
				}
				else if (key == Keys.D && key == Keys.W)
				{
					maxFrame = 21;
					minFrame = 18;
					lastPos = 18;
					moving = true;
					charPos.X++;
				}
				else
				{
					moving = false;
				}
			}

			if (moving)
			{
				if (currentFrame >= 24)
					currentFrame = 1;

				currentFrame++;
			}
			else
				currentFrame = lastPos;
			//if (keyState.IsKeyDown(Keys.Space))
			//WeaponManager.FireWeapon(TurretSprite.WorldLocation, BaseSprite.Velocity * WeaponManager.WeaponSpeed);
		}

	}
}
