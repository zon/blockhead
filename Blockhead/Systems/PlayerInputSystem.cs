using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework.Input;

namespace Blockhead {

	public class PlayerInputSystem : AComponentSystem<float, Player> {

		public PlayerInputSystem(World world) : base(world) {}

		protected override void Update(float dt, ref Player player) {
			var state = Keyboard.GetState();
			if (state.IsKeyDown(Keys.A)) player.DX -= Player.Accel * dt;
			if (state.IsKeyDown(Keys.D)) player.DX += Player.Accel * dt;
			if (state.IsKeyDown(Keys.W)) player.DY -= Player.Accel * dt;
			if (state.IsKeyDown(Keys.S)) player.DY += Player.Accel * dt;
		}

	}

}
