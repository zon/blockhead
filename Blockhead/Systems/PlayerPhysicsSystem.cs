using DefaultEcs;
using DefaultEcs.System;
using Basegame;

namespace Blockhead {

	public class PlayerPhysicsSystem : AComponentSystem<float, Player> {
		readonly Grid Grid;

		public PlayerPhysicsSystem(World world, Grid grid) : base(world) {
			Grid = grid;
		}

		protected override void Update(float dt, ref Player player) {
			if (Grid.IsSolid(
				player.X + player.DX * dt,
				player.Y,
				player.Width,
				player.Height
			)) {
				player.DX *= -Player.Bounce;
			}
			
			player.X += player.DX * dt;
			
			if (Grid.IsSolid(
				player.X,
				player.Y + player.DY * dt,
				player.Width,
				player.Height
			)) {
				player.DY *= -Player.Bounce;
			}
			
			player.Y += player.DY * dt;

			var friction = Player.Friction * dt;
			player.DX -= player.DX * friction;
			player.DY -= player.DY * friction;
		}

	}

}
