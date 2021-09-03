using DefaultEcs;
using DefaultEcs.System;
using Basegame;

namespace Blockhead {

	public class PlayerPhysicsSystem : AEntitySetSystem<float> {
		readonly Grid Grid;

		public PlayerPhysicsSystem(World world, Grid grid) : base(world
			.GetEntities()
			.With<Player>()
			.AsSet()
		) {
			Grid = grid;
		}

		protected override void Update(float dt, in Entity entity) {
			ref var input = ref entity.Get<PlayerInput>();
			ref var player = ref entity.Get<Player>();

			var ax = input.MoveX * Player.Accel;
			player.DX += ax * dt;
			player.DX += -player.DX * Player.Friction * dt;

			var ay = input.MoveY * Player.Accel;
			player.DY += ay * dt;
			player.DY += -player.DY * Player.Friction * dt; 

			// player.DX = input.MoveX * 5;
			// player.DY = input.MoveY * 5;
			// player.DY += Player.Gravity / 4;

			var nx = player.X + player.DX * dt;
			if (Grid.IsSolid(
				nx,
				player.Y,
				player.Width,
				player.Height
			)) {
				player.DX *= -Player.Bounce;
			}
			
			if (Grid.IsSolid(
				player.X,
				player.Y + player.DY * dt,
				player.Width,
				player.Height
			)) {
				player.DY *= -Player.Bounce;
			}
			
			player.X += player.DX * dt;
			player.Y += player.DY * dt;
		}

	}

}
