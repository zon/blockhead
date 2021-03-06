using DefaultEcs;
using DefaultEcs.System;
using Basegame;
using System;

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

			if (input.MoveX != 0) {
				player.Facing = input.MoveX;
			}

			// var ay = input.MoveY * Player.Accel;
			// player.DY += ay * dt;
			// player.DY += -player.DY * Player.Friction * dt;

			var g = 1f;
			if (input.Jump > 0 || player.DY > 0) {
				g = 2;
			}
			player.DY += Player.Gravity * g * dt;

			if (input.Jump < PlayerInput.Flex && player.Grounded == 0) {
				player.DY += Player.Jump;
			}

			player.Grounded += dt;

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
			
			var dy = player.DY * dt;
			var ny = player.Y + player.DY * dt;
			if (Grid.IsSolid(
				player.X,
				Math.Min(player.Y, player.Y + dy),
				player.Width,
				Math.Max(player.Height, player.Height +dy)
			)) {
				if (player.DY > 0) {
					player.Y = Calc.Floor(ny + player.Height) - player.Height;
					player.Grounded = 0;
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
