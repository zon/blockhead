using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Basegame.Client;
using Microsoft.Xna.Framework.Graphics;
using System;

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
			ref var input = ref entity.Get<PlayerInput>();
			ref var player = ref entity.Get<Player>();
			ref var sprite = ref entity.Get<Sprite>();

			sprite.Update(dt);

			var tag = "stand";
			if (player.Grounded > 0) {
				tag = "jump";
			} else if (input.MoveX != 0) {
				tag = "walk";
				sprite.FrameRate = Math.Abs(player.DX) * 0.02f;
			}
			if (sprite.Tag.Name != tag) {
				sprite.Play(tag);
			}
			
			var effects = SpriteEffects.None;
			if (player.Facing < 0) {
				effects = SpriteEffects.FlipHorizontally;
			}

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
			Camera.Batch.Draw(
				texture: sprite.Document.Texture,
				position: position,
				sourceRectangle: frame,
				color: Color.White,
				rotation: 0,
				origin: Vector2.Zero,
				scale: 1,
				effects: effects,
				layerDepth: 0
			);
		}

	}

}
