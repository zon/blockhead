using DefaultEcs.System;
using ldtk;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Basegame;

namespace Basegame.Client {

	public class LdtkDrawSystem : ISystem<float> {
		readonly LevelResources LevelResources;
		readonly CameraView Camera;
		readonly SpriteBatch Batch;

		LayerDefinition Collisions;
		TilesetDefinition Tileset;
		Texture2D Texture;

		public bool IsEnabled { get; set; }

		public LdtkDrawSystem(LevelResources levelResources, CameraView camera, SpriteBatch batch) {
			LevelResources = levelResources;
			Camera = camera;
			Batch = batch;
			IsEnabled = true;
		}

		public void Update(float state) {
			var world = LevelResources.World;
			var matrix = Camera.GetMatrix();

			if (Collisions == null) Collisions = world.GetLayerDefinition("Collisions");
			if (Tileset == null) Tileset = world.GetTileset(Collisions.AutoTilesetDefUid.Value);
			if (Texture == null) Texture = LevelResources.GetTilesetSprite(Tileset).Texture;

			var tileSize = new Point((int) Tileset.TileGridSize, (int) Tileset.TileGridSize);

			Batch.Begin(transformMatrix: matrix, samplerState: SamplerState.PointClamp);
			foreach (var level in world.Json.Levels) {
				var offset = new Point((int) level.WorldX, (int) level.WorldY);
				foreach (var layer in level.LayerInstances) {
					if (layer.LayerDefUid != Collisions.Uid) continue;
					foreach (var tile in layer.AutoLayerTiles) {
						Batch.Draw(
							texture: Texture,
							position: (View.ToPoint(tile.Px) + offset).ToVector2(),
							sourceRectangle: new Rectangle(View.ToPoint(tile.Src), tileSize),
							color: Color.White,
							rotation: 0,
							origin: Vector2.Zero,
							scale: Vector2.One,
							effects: SpriteEffects.None,
							layerDepth: 0
						);
					}
				}
			}
			Batch.End();
		}

		public void Dispose() {}

	}

}
