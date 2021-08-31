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
			var position = Camera.WorldToTarget(player.X, player.Y);
			Camera.Batch.Draw(
				texture,
				position,
				null,
				Color.White
			);
		}

	}

}
