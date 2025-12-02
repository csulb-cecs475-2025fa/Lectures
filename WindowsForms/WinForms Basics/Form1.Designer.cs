namespace WinForms_Basics {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.mPicture2 = new System.Windows.Forms.PictureBox();
			this.mPicture1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.mPowerText1 = new System.Windows.Forms.TextBox();
			this.mDefenseText1 = new System.Windows.Forms.TextBox();
			this.mAttackText1 = new System.Windows.Forms.TextBox();
			this.mLevelText1 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.mHPText1 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.mPowerText2 = new System.Windows.Forms.TextBox();
			this.mDefenseText2 = new System.Windows.Forms.TextBox();
			this.mAttackText2 = new System.Windows.Forms.TextBox();
			this.mLevelText2 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.mHPText2 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.mFightBtn = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.mPicture2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mPicture1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// mPicture2
			// 
			this.mPicture2.Image = ((System.Drawing.Image)(resources.GetObject("mPicture2.Image")));
			this.mPicture2.Location = new System.Drawing.Point(429, 15);
			this.mPicture2.Margin = new System.Windows.Forms.Padding(4);
			this.mPicture2.Name = "mPicture2";
			this.mPicture2.Size = new System.Drawing.Size(200, 225);
			this.mPicture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.mPicture2.TabIndex = 0;
			this.mPicture2.TabStop = false;
			this.mPicture2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mPicture2_MouseDoubleClick);
			// 
			// mPicture1
			// 
			this.mPicture1.Image = ((System.Drawing.Image)(resources.GetObject("mPicture1.Image")));
			this.mPicture1.Location = new System.Drawing.Point(16, 68);
			this.mPicture1.Margin = new System.Windows.Forms.Padding(4);
			this.mPicture1.Name = "mPicture1";
			this.mPicture1.Size = new System.Drawing.Size(250, 139);
			this.mPicture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.mPicture1.TabIndex = 1;
			this.mPicture1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(357, 133);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 41);
			this.label1.TabIndex = 2;
			this.label1.Text = "VS";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.mPowerText1);
			this.groupBox1.Controls.Add(this.mDefenseText1);
			this.groupBox1.Controls.Add(this.mAttackText1);
			this.groupBox1.Controls.Add(this.mLevelText1);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.mHPText1);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(16, 316);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox1.Size = new System.Drawing.Size(332, 198);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Stats";
			// 
			// mPowerText1
			// 
			this.mPowerText1.Location = new System.Drawing.Point(91, 153);
			this.mPowerText1.Margin = new System.Windows.Forms.Padding(4);
			this.mPowerText1.Name = "mPowerText1";
			this.mPowerText1.Size = new System.Drawing.Size(132, 22);
			this.mPowerText1.TabIndex = 4;
			this.mPowerText1.Text = "75";
			this.mPowerText1.TextChanged += new System.EventHandler(this.mPowerText1_TextChanged);
			// 
			// mDefenseText1
			// 
			this.mDefenseText1.Location = new System.Drawing.Point(91, 121);
			this.mDefenseText1.Margin = new System.Windows.Forms.Padding(4);
			this.mDefenseText1.Name = "mDefenseText1";
			this.mDefenseText1.Size = new System.Drawing.Size(132, 22);
			this.mDefenseText1.TabIndex = 3;
			this.mDefenseText1.Text = "115";
			this.mDefenseText1.TextChanged += new System.EventHandler(this.mDefenseText1_TextChanged);
			// 
			// mAttackText1
			// 
			this.mAttackText1.Location = new System.Drawing.Point(91, 89);
			this.mAttackText1.Margin = new System.Windows.Forms.Padding(4);
			this.mAttackText1.Name = "mAttackText1";
			this.mAttackText1.Size = new System.Drawing.Size(132, 22);
			this.mAttackText1.TabIndex = 2;
			this.mAttackText1.Text = "120";
			this.mAttackText1.TextChanged += new System.EventHandler(this.mAttackText1_TextChanged);
			// 
			// mLevelText1
			// 
			this.mLevelText1.Location = new System.Drawing.Point(91, 57);
			this.mLevelText1.Margin = new System.Windows.Forms.Padding(4);
			this.mLevelText1.Name = "mLevelText1";
			this.mLevelText1.Size = new System.Drawing.Size(132, 22);
			this.mLevelText1.TabIndex = 1;
			this.mLevelText1.Text = "100";
			this.mLevelText1.TextChanged += new System.EventHandler(this.mLevelText1_TextChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 153);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.TabIndex = 0;
			this.label6.Text = "Power:";
			// 
			// mHPText1
			// 
			this.mHPText1.Location = new System.Drawing.Point(91, 25);
			this.mHPText1.Margin = new System.Windows.Forms.Padding(4);
			this.mHPText1.Name = "mHPText1";
			this.mHPText1.Size = new System.Drawing.Size(132, 22);
			this.mHPText1.TabIndex = 0;
			this.mHPText1.TextChanged += new System.EventHandler(this.mHPText1_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 121);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(61, 16);
			this.label5.TabIndex = 0;
			this.label5.Text = "Defense:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 89);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Attack:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 57);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Level:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 25);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "HP:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.mPowerText2);
			this.groupBox2.Controls.Add(this.mDefenseText2);
			this.groupBox2.Controls.Add(this.mAttackText2);
			this.groupBox2.Controls.Add(this.mLevelText2);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.mHPText2);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Location = new System.Drawing.Point(429, 316);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox2.Size = new System.Drawing.Size(332, 198);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Stats";
			// 
			// mPowerText2
			// 
			this.mPowerText2.Location = new System.Drawing.Point(91, 153);
			this.mPowerText2.Margin = new System.Windows.Forms.Padding(4);
			this.mPowerText2.Name = "mPowerText2";
			this.mPowerText2.Size = new System.Drawing.Size(132, 22);
			this.mPowerText2.TabIndex = 4;
			this.mPowerText2.Text = "100";
			this.mPowerText2.TextChanged += new System.EventHandler(this.mPowerText2_TextChanged);
			// 
			// mDefenseText2
			// 
			this.mDefenseText2.Location = new System.Drawing.Point(91, 121);
			this.mDefenseText2.Margin = new System.Windows.Forms.Padding(4);
			this.mDefenseText2.Name = "mDefenseText2";
			this.mDefenseText2.Size = new System.Drawing.Size(132, 22);
			this.mDefenseText2.TabIndex = 3;
			this.mDefenseText2.Text = "50";
			this.mDefenseText2.TextChanged += new System.EventHandler(this.mDefenseText2_TextChanged);
			// 
			// mAttackText2
			// 
			this.mAttackText2.Location = new System.Drawing.Point(91, 89);
			this.mAttackText2.Margin = new System.Windows.Forms.Padding(4);
			this.mAttackText2.Name = "mAttackText2";
			this.mAttackText2.Size = new System.Drawing.Size(132, 22);
			this.mAttackText2.TabIndex = 2;
			this.mAttackText2.Text = "60";
			this.mAttackText2.TextChanged += new System.EventHandler(this.mAttackText2_TextChanged);
			// 
			// mLevelText2
			// 
			this.mLevelText2.Location = new System.Drawing.Point(91, 57);
			this.mLevelText2.Margin = new System.Windows.Forms.Padding(4);
			this.mLevelText2.Name = "mLevelText2";
			this.mLevelText2.Size = new System.Drawing.Size(132, 22);
			this.mLevelText2.TabIndex = 1;
			this.mLevelText2.Text = "100";
			this.mLevelText2.TextChanged += new System.EventHandler(this.mLevelText2_TextChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 153);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 16);
			this.label7.TabIndex = 0;
			this.label7.Text = "Power:";
			// 
			// mHPText2
			// 
			this.mHPText2.Location = new System.Drawing.Point(91, 25);
			this.mHPText2.Margin = new System.Windows.Forms.Padding(4);
			this.mHPText2.Name = "mHPText2";
			this.mHPText2.Size = new System.Drawing.Size(132, 22);
			this.mHPText2.TabIndex = 0;
			this.mHPText2.Text = "39";
			this.mHPText2.TextChanged += new System.EventHandler(this.mHPText2_TextChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(8, 121);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(61, 16);
			this.label8.TabIndex = 0;
			this.label8.Text = "Defense:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(8, 89);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(47, 16);
			this.label9.TabIndex = 0;
			this.label9.Text = "Attack:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(8, 57);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(43, 16);
			this.label10.TabIndex = 0;
			this.label10.Text = "Level:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(8, 25);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(29, 16);
			this.label11.TabIndex = 0;
			this.label11.Text = "HP:";
			// 
			// mFightBtn
			// 
			this.mFightBtn.AutoSize = true;
			this.mFightBtn.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mFightBtn.Location = new System.Drawing.Point(327, 546);
			this.mFightBtn.Margin = new System.Windows.Forms.Padding(4);
			this.mFightBtn.Name = "mFightBtn";
			this.mFightBtn.Size = new System.Drawing.Size(139, 50);
			this.mFightBtn.TabIndex = 14;
			this.mFightBtn.Text = "FIGHT";
			this.mFightBtn.UseVisualStyleBackColor = true;
			this.mFightBtn.Click += new System.EventHandler(this.mFightBtn_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(779, 609);
			this.Controls.Add(this.mFightBtn);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.mPicture1);
			this.Controls.Add(this.mPicture2);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.Text = "Plz Don\'t Sue Me Nintendo";
			((System.ComponentModel.ISupportInitialize)(this.mPicture2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mPicture1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox mPicture2;
		private System.Windows.Forms.PictureBox mPicture1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox mPowerText1;
		private System.Windows.Forms.TextBox mDefenseText1;
		private System.Windows.Forms.TextBox mAttackText1;
		private System.Windows.Forms.TextBox mLevelText1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox mHPText1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox mPowerText2;
		private System.Windows.Forms.TextBox mDefenseText2;
		private System.Windows.Forms.TextBox mAttackText2;
		private System.Windows.Forms.TextBox mLevelText2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox mHPText2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button mFightBtn;
	}
}

