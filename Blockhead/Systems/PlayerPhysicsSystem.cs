using System;
using DefaultEcs;
using DefaultEcs.System;

namespace Blockhead {

	public class PlayerPhysicsSystem : AComponentSystem<float, Player> {

		public PlayerPhysicsSystem(World world) : base(world) {}

		protected override void Update(float dt, ref Player player) {
			player.X += player.DX * dt;
			player.Y += player.DY * dt;

			var friction = Player.Friction * dt;
			player.DX -= player.DX * friction;
			player.DY -= player.DY * friction;
		}

	}

}
