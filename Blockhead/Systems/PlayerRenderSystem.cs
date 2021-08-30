using System;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Basegame.Client;

namespace Blockhead {

	public class PlayerRenderSystem : AComponentSystem<float, Player> {
		readonly CameraView Camera;

		public PlayerRenderSystem(World world, CameraView camera) : base(world) {
			Camera = camera;
		}

		protected override void Update(float dt, ref Player player) {
			Camera.Batch.Draw(
				texture: player.Document.Texture,
				position: new Vector2(player.X, player.Y),
				color: Color.White
			);
		}

	}

}
