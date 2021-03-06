using System;
using System.Collections.Immutable;
using DefaultEcs;

namespace Basegame {

	public class Grid {
		public readonly ImmutableDictionary<Coord, Node> Nodes;
		public readonly Spawn[] Spawns;

		public Grid(ImmutableDictionary<Coord, Node> nodes, Spawn[] spawns = null) {
			Nodes = nodes;
			Spawns = (spawns != null) ? spawns : new Spawn[0];
		}

		public Grid(LdtkWorld world) : this(world.GetNodes(), world.GetSpawns()) {}

		public Node Get(long x, long y) {
			return Get(new Coord(x, y));
		}

		public Node Get(Coord coord) {
			Node res;
			Nodes.TryGetValue(coord, out res);
			return res;
		}

		public bool IsSolid(Coord coord) {
			var node = Get(coord);
			if (node == null) {
				return true;
			} else {
				if (node.Solid) {
					return true;
				} else {
					return false;
				}
			}
		}

		public bool IsSolid(long x, long y) {
			return IsSolid(new Coord(x, y));
		}

		public bool IsSolid(float x, float y, float width, float height) {
			var ay = Calc.Floor(y);
			var by = Calc.Floor(y + height);
			if ((y + height) % 1 == 0) by -= 1;
			var ax = Calc.Floor(x);
			var bx = Calc.Floor(x + width);
			if ((x + width) % 1 == 0) bx -= 1;
			for (var yy = ay; yy <= by; yy++) {
				for (var xx = ax; xx <= bx; xx++) {
					if (IsSolid(xx, yy)) {
						return true;
					}
				}
			}
			return false;
		}

		// https://stackoverflow.com/a/3706260
		public Node GetOpenNearby(EntityMap<Position> positions, long x, long y) {
			long vx = 1;
			long vy = 0;
			long len = 1;
			long ox = 0;
			long oy = 0;
			long p = 0;
			for (var _ = 0; _ < 64; _++) {

				var node = Get(x + ox, y + oy);
				if (node != null && !node.Solid) {
					if (!positions.ContainsKey(node.Position)) return node;
				}

				ox += vx;
				oy += vy;
				p += 1;
				if (p >= len) {
					p = 0;
					var f = vx;
					vx = -vy;
					vy = f;
					if (vy == 0) {
						len += 1;
					}
				}
			}
			return null;
		}

		public Node GetOpenNearby(EntityMap<Position> positions, Coord coord) {
			return GetOpenNearby(positions, coord.X, coord.Y);
		}

	}

}
