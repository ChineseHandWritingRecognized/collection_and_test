namespace testsx
{
	partial class Form1
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.label3 = new System.Windows.Forms.Label();
			this.save = new System.Windows.Forms.Label();
			this.caiji = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.ClearButt = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("黑体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.ForeColor = System.Drawing.Color.Orange;
			this.label3.Location = new System.Drawing.Point(616, 119);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(145, 90);
			this.label3.TabIndex = 354;
			this.label3.Tag = "Q";
			this.label3.Text = "识别";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label3.Click += new System.EventHandler(this.label3_Click);
			// 
			// save
			// 
			this.save.BackColor = System.Drawing.Color.Transparent;
			this.save.Font = new System.Drawing.Font("黑体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.save.ForeColor = System.Drawing.Color.Orange;
			this.save.Location = new System.Drawing.Point(616, 29);
			this.save.Name = "save";
			this.save.Size = new System.Drawing.Size(145, 90);
			this.save.TabIndex = 353;
			this.save.Tag = "Q";
			this.save.Text = "保存";
			this.save.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.save.Click += new System.EventHandler(this.save_Click);
			// 
			// caiji
			// 
			this.caiji.BackColor = System.Drawing.Color.Transparent;
			this.caiji.Font = new System.Drawing.Font("黑体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.caiji.ForeColor = System.Drawing.Color.Orange;
			this.caiji.Location = new System.Drawing.Point(465, 119);
			this.caiji.Name = "caiji";
			this.caiji.Size = new System.Drawing.Size(145, 90);
			this.caiji.TabIndex = 352;
			this.caiji.Tag = "Q";
			this.caiji.Text = "采集";
			this.caiji.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.caiji.Click += new System.EventHandler(this.caiji_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.button1.Location = new System.Drawing.Point(475, 294);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(151, 74);
			this.button1.TabIndex = 351;
			this.button1.Text = "清空";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// ClearButt
			// 
			this.ClearButt.BackColor = System.Drawing.Color.Transparent;
			this.ClearButt.Font = new System.Drawing.Font("黑体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ClearButt.ForeColor = System.Drawing.Color.Orange;
			this.ClearButt.Location = new System.Drawing.Point(465, 29);
			this.ClearButt.Name = "ClearButt";
			this.ClearButt.Size = new System.Drawing.Size(145, 90);
			this.ClearButt.TabIndex = 350;
			this.ClearButt.Tag = "Q";
			this.ClearButt.Text = "重写";
			this.ClearButt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ClearButt.Click += new System.EventHandler(this.ClearButt_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.White;
			this.pictureBox1.Location = new System.Drawing.Point(23, 29);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(436, 334);
			this.pictureBox1.TabIndex = 349;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 396);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.save);
			this.Controls.Add(this.caiji);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.ClearButt);
			this.Controls.Add(this.pictureBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label save;
		private System.Windows.Forms.Label caiji;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label ClearButt;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}

