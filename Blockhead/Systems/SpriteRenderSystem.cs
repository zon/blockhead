using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Basegame.Client;

namespace Blockhead {

	public class SpriteRenderSystem : AEntitySetSystem<float> {
		readonly CameraView Camera;

		public SpriteRenderSystem(World world, CameraView camera) : base(world
			.GetEntities()
			.With<Sprite>()
			.AsSet()
		) {
			Camera = camera;
		}

		protected override void Update(float dt, in Entity entity) {
			ref var player = ref entity.Get<Player>();
			ref var sprite = ref entity.Get<Sprite>();

			sprite.Update(dt);

			var frame = sprite.GetFrame();
			var size = Camera.WorldToTarget(player.Width, player.Height);
			var position = Camera.WorldToTarget(player.X, player.Y) + new Vector2(
				(size.X - frame.Width) / 2,
				size.Y - frame.Height
			);
			Camera.Batch.Draw(
				sprite.Document.Texture,
				position,
				frame,
				Color.White
			);
		}

	}

}
