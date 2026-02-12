using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame {
	internal class Hero {
		public string Name { get; set; }

		public int CurrentHitPoints { get; set; }
		public int MaxHitPoints { get; set; }

		public int Strength { get; set; }
		public int StrengthModifier {
			get {
				int mod = (int)Math.Floor((Strength - 10.0) / 2);
				return mod;
			}
		}

		public int Dexterity { get; set; }
		public int Intelligence { get; set; }

		public Hero(string name, int maxHitPoints,  int strength, int dexterity, int intelligence) {
			Name = name;
			MaxHitPoints = CurrentHitPoints = maxHitPoints;
			Strength = strength;
			Dexterity = dexterity;
			Intelligence = intelligence;
		}

	}
}
