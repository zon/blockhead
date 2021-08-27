using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DefaultEcs;
using DefaultEcs.System;
using Basegame;
using Basegame.Client;

namespace Blockhead {

	public class Game : Microsoft.Xna.Framework.Game {
		GraphicsDeviceManager Graphics;
		LevelResources LevelResources;
		SpriteBatch Batch;
		CameraView Camera;
		ISystem<float> Rendering;
		SpriteBatch Result;

		public Game() {
			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize() {
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent() {
			var width = 1024;
			var height = 1024;

			Graphics.PreferredBackBufferWidth = width;
			Graphics.PreferredBackBufferHeight = height;
			Graphics.ApplyChanges();

			LevelResources = LevelResources.Load(Content, "test");
			Batch = new SpriteBatch(GraphicsDevice);
			Camera = new CameraView(Window, GraphicsDevice, width, height);
			Rendering = new LdtkDrawSystem(LevelResources, Camera, Batch);
			
			Result = new SpriteBatch(GraphicsDevice);
		}

		protected override void Update(GameTime gameTime) {
			// var dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			var dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

			GraphicsDevice.Clear(Color.Black);

			GraphicsDevice.SetRenderTarget(Camera.RenderTarget);
			GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
			GraphicsDevice.Clear(Color.Black);

			Rendering.Update(dt);
			
			GraphicsDevice.SetRenderTarget(null);

			Result.Begin(samplerState: SamplerState.PointClamp);
			Result.Draw(
				texture: Camera.RenderTarget,
				position: Vector2.Zero,
				sourceRectangle: null,
				color: Color.White,
				rotation: 0,
				origin: Vector2.Zero,
				scale: Vector2.One * View.SCALE,
				effects: SpriteEffects.None,
				layerDepth: 0
			);
			Result.End();

			base.Draw(gameTime);
		}
	}
}
