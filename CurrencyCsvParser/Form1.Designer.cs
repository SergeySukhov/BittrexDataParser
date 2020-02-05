namespace CurrencyCsvParser
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonStart = new System.Windows.Forms.Button();
			this.textBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonStart
			// 
			this.buttonStart.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonStart.ForeColor = System.Drawing.Color.LightGray;
			this.buttonStart.Location = new System.Drawing.Point(13, 386);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(137, 52);
			this.buttonStart.TabIndex = 0;
			this.buttonStart.Text = "Start parse";
			this.buttonStart.UseVisualStyleBackColor = false;
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// textBox
			// 
			this.textBox.BackColor = System.Drawing.SystemColors.WindowText;
			this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBox.ForeColor = System.Drawing.Color.LightGray;
			this.textBox.Location = new System.Drawing.Point(13, 13);
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox.Size = new System.Drawing.Size(775, 367);
			this.textBox.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.buttonStart);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.TextBox textBox;
	}
}

