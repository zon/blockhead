﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite.Documents;
using DefaultEcs;
using DefaultEcs.System;
using Basegame;
using Basegame.Client;

namespace Blockhead {

	public class Game : Microsoft.Xna.Framework.Game {
		World World;
		GraphicsDeviceManager Graphics;
		LevelResources LevelResources;
		CameraView Camera;
		ISystem<float> BackgroundRendering;
		ISystem<float> ForegroundRendering;
		SpriteBatch Result;

		public Game() {
			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize() {
			World = new World();

			base.Initialize();
		}

		protected override void LoadContent() {
			var width = 1024;
			var height = 1024;

			Graphics.PreferredBackBufferWidth = width;
			Graphics.PreferredBackBufferHeight = height;
			Graphics.ApplyChanges();

			LevelResources = LevelResources.Load(Content, "test");
			Camera = new CameraView(Window, GraphicsDevice, width, height);
			BackgroundRendering = new LdtkDrawSystem(LevelResources, Camera);
			ForegroundRendering  = new SequentialSystem<float>(
				new PlayerRenderSystem(World, Camera)
			);
			
			Result = new SpriteBatch(GraphicsDevice);

			var entity = World.CreateEntity();
			entity.Set(new Player {
				Width = 0.75f,
				Height = 0.75f,
				X = 8,
				Y = 8,
				Document = Content.Load<AsepriteDocument>("ghost")
			});
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

			BackgroundRendering.Update(dt);

			Camera.Batch.Begin(
				transformMatrix: Camera.GetMatrix(),
				samplerState: SamplerState.PointClamp,
				sortMode: SpriteSortMode.FrontToBack
			);
			ForegroundRendering.Update(dt);
			Camera.Batch.End();
			
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