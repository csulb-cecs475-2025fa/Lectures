using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Basics {
	public partial class Form1 : Form {

		// We create private fields for objects from the Model that we need to represent the state of the form.
		private Pokemon mTogekiss = new Pokemon() {
			HP = 85,
			Level = 100,
			Attack = 120,
			Defense = 115
		};
		private int mTogekissAttackPower = 75;

		private Pokemon mCharmander = new Pokemon() {
			HP = 39,
			Level = 100,
			Attack = 60,
			Defense = 50
		};
		private int mCharmanderAttackPower = 100;

		// We set the default state of our UI controls in the constructor.
		public Form1() {
			// Never remove this method call.
			InitializeComponent();

			mHPText1.Text = mTogekiss.HP.ToString();
			mLevelText1.Text = mTogekiss.Level.ToString();
			mAttackText1.Text = mTogekiss.Attack.ToString();
			mDefenseText1.Text = mTogekiss.Defense.ToString();
			mPowerText1.Text = mTogekissAttackPower.ToString();

			mHPText2.Text = mCharmander.HP.ToString();
			mLevelText2.Text = mCharmander.Level.ToString();
			mAttackText2.Text = mCharmander.Attack.ToString();
			mDefenseText2.Text = mCharmander.Defense.ToString();
			mPowerText2.Text = mCharmanderAttackPower.ToString();
		}

		// We then hook event handlers into appropriate events from our controls, and program
		// logic in response to those events.
		// Notice that these methods are not mentioned anywhere in this file... how does that magic work???

		// This event fires when the Togekiss HP text box changes text.
		private void mHPText1_TextChanged(object sender, EventArgs e) {
			mTogekiss.HP = Convert.ToInt32(mHPText1.Text);
		}

		private void mLevelText1_TextChanged(object sender, EventArgs e) {
			mTogekiss.Level = Convert.ToInt32(mLevelText1.Text);
		}

		private void mAttackText1_TextChanged(object sender, EventArgs e) {
			//mTogekiss.Attack = Convert.ToInt32(mAttackText1.Text);
		}

		private void mDefenseText1_TextChanged(object sender, EventArgs e) {
			mTogekiss.Defense = Convert.ToInt32(mDefenseText1.Text);
		}

		private void mPowerText1_TextChanged(object sender, EventArgs e) {
			mTogekissAttackPower = Convert.ToInt32(mPowerText1.Text);
		}

		private void mHPText2_TextChanged(object sender, EventArgs e) {
			mCharmander.HP = Convert.ToInt32(mHPText2.Text);
		}

		private void mLevelText2_TextChanged(object sender, EventArgs e) {
			mCharmander.Level = Convert.ToInt32(mLevelText2.Text);
		}

		private void mAttackText2_TextChanged(object sender, EventArgs e) {
			mCharmander.Attack = Convert.ToInt32(mAttackText2.Text);
		}

		private void mDefenseText2_TextChanged(object sender, EventArgs e) {
			mCharmander.Defense = Convert.ToInt32(mDefenseText2.Text);

		}

		private void mPowerText2_TextChanged(object sender, EventArgs e) {
			mCharmanderAttackPower = Convert.ToInt32(mPowerText2.Text);
		}


		// This event triggers when the FIGHT button is clicked.
		private void mFightBtn_Click(object sender, EventArgs e) {
			mTogekiss.AttackTarget(mCharmander, Convert.ToInt32(mPowerText1.Text));
			mCharmander.AttackTarget(mTogekiss, Convert.ToInt32(mPowerText2.Text));

			// Setting the .Text field triggers mHPText2_TextChanged.
			mHPText1.Text = mTogekiss.HP.ToString();
			mHPText2.Text = mCharmander.HP.ToString();
		}

		// This event fires when Charmander's picture is double clicked.
		private void mPicture2_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				System.IO.Stream file = System.Reflection.Assembly.GetExecutingAssembly()
					.GetManifestResourceStream("Basics.Resources.charizard.png");
				mPicture2.Image = Image.FromStream(file);

				mCharmander.HP = 100;
				mCharmander.Attack = 600;
				mCharmander.Defense = 100;
				mHPText2.Text = mCharmander.HP.ToString();
				mAttackText2.Text = mCharmander.Attack.ToString();
				mDefenseText2.Text = mCharmander.Defense.ToString();
			}
		}

		
	}
}
