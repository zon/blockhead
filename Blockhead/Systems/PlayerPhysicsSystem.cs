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

			// player.DY += Player.Gravity * dt;

			// if (player.Grounded == 0 && input.Jump < PlayerInput.Flex) {
			// 	player.DY += Player.Jump;
			// }

			var nx = player.X + player.DX * dt;
			if (Grid.IsSolid(
				nx,
				player.Y,
				player.Width,
				player.Height
			)) {
				if (player.DX > 0) {
					player.X = Calc.Floor(nx + player.Width) - player.Width;
				} else if (player.DX < 0) {
					player.X = Calc.Floor(nx) + 1;
				}
				player.DX = 0;
			}
			
			var ny = player.Y + player.DY * dt;
			if (Grid.IsSolid(
				player.X,
				ny,
				player.Width,
				player.Height
			)) {
				if (player.DY > 0) {
					player.Y = Calc.Floor(ny + player.Height) - player.Height;
				} else if (player.DY < 0) {
					player.Y = Calc.Floor(ny) + 1;
				}
				player.DY = 0;
			}
			
			player.X += player.DX * dt;
			player.Y += player.DY * dt;
		}

	}

}
