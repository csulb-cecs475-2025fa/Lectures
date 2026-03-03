using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms_Basics;

namespace Basics {
	public partial class EditorDemo : Form {
		public Pokemon MyPokemon { get; }
		public EditorDemo(Pokemon pokemon) {
			InitializeComponent();
			MyPokemon = pokemon;
		}

		private void button1_Click(object sender, EventArgs e) {
			MyPokemon.Attack = Convert.ToInt32(textBox1.Text);
			this.Close();
		}
	}
}
