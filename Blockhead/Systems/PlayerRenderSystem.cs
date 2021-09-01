using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Basegame;
using Basegame.Client;

namespace Blockhead {

	public class PlayerRenderSystem : AComponentSystem<float, Player> {
		readonly CameraView Camera;

		public PlayerRenderSystem(World world, CameraView camera) : base(world) {
			Camera = camera;
		}

		protected override void Update(float dt, ref Player player) {
			var texture = player.Document.Texture;
			var size = Camera.WorldToTarget(Player.Width, Player.Height);
			var position = Camera.WorldToTarget(player.X, player.Y) - new Vector2(
				texture.Width - size.X,
				texture.Height - size.Y
			) / 2;
			Camera.Batch.Draw(
				texture,
				position,
				null,
				Color.White
			);
		}

	}

}
