using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace LansSimpleHealthbar
{
	class Config : ModConfig
	{
		// You MUST specify a ConfigScope.
		public override ConfigScope Mode => ConfigScope.ClientSide;


		[Label("Show Mana")]
		[DefaultValue(true)]
		public bool ShowMana;

		[Label("Show Potion Sickness")]
		[DefaultValue(true)]
		public bool ShowPotionSickness;
		
	}
}
