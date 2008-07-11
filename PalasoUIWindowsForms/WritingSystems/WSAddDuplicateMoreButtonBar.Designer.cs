namespace Palaso.UI.WindowsForms.WritingSystems
{
	partial class WSAddDuplicateMoreButtonBar
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this._addButton = new System.Windows.Forms.Button();
			this._duplicateButton = new System.Windows.Forms.Button();
			this._moreButton = new System.Windows.Forms.Button();
			this._contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._loadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._contextMenu.SuspendLayout();
			this.SuspendLayout();
			//
			// _addButton
			//
			this._addButton.Location = new System.Drawing.Point(20, 13);
			this._addButton.Name = "_addButton";
			this._addButton.Size = new System.Drawing.Size(75, 23);
			this._addButton.TabIndex = 0;
			this._addButton.Text = "Add";
			this._addButton.UseVisualStyleBackColor = true;
			this._addButton.Click += new System.EventHandler(this.AddButtonClick);
			//
			// _duplicateButton
			//
			this._duplicateButton.Location = new System.Drawing.Point(101, 13);
			this._duplicateButton.Name = "_duplicateButton";
			this._duplicateButton.Size = new System.Drawing.Size(75, 23);
			this._duplicateButton.TabIndex = 1;
			this._duplicateButton.Text = "Duplicate";
			this._duplicateButton.UseVisualStyleBackColor = true;
			this._duplicateButton.Click += new System.EventHandler(this.DuplicateButtonClick);
			//
			// _moreButton
			//
			this._moreButton.Cursor = System.Windows.Forms.Cursors.Arrow;
			this._moreButton.Location = new System.Drawing.Point(182, 13);
			this._moreButton.Name = "_moreButton";
			this._moreButton.Size = new System.Drawing.Size(75, 23);
			this._moreButton.TabIndex = 2;
			this._moreButton.Text = "More";
			this._moreButton.UseVisualStyleBackColor = true;
			this._moreButton.Click += new System.EventHandler(this.MoreButtonClick);
			//
			// _contextMenu
			//
			this._contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this._deleteMenuItem,
			this._loadMenuItem});
			this._contextMenu.Name = "_contextMenu";
			this._contextMenu.Size = new System.Drawing.Size(106, 48);
			//
			// _deleteMenuItem
			//
			this._deleteMenuItem.Name = "_deleteMenuItem";
			this._deleteMenuItem.Size = new System.Drawing.Size(105, 22);
			this._deleteMenuItem.Text = "&Delete";
			this._deleteMenuItem.Click += new System.EventHandler(this.DeleteMenuClick);
			//
			// _loadMenuItem
			//
			this._loadMenuItem.Name = "_loadMenuItem";
			this._loadMenuItem.Size = new System.Drawing.Size(105, 22);
			this._loadMenuItem.Text = "&Load";
			this._loadMenuItem.Click += new System.EventHandler(this.LoadMenuClick);
			//
			// WSAddDuplicateMoreButtonBar
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._moreButton);
			this.Controls.Add(this._duplicateButton);
			this.Controls.Add(this._addButton);
			this.Name = "WSAddDuplicateMoreButtonBar";
			this.Size = new System.Drawing.Size(273, 49);
			this._contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button _addButton;
		private System.Windows.Forms.Button _duplicateButton;
		private System.Windows.Forms.Button _moreButton;
		private System.Windows.Forms.ContextMenuStrip _contextMenu;
		private System.Windows.Forms.ToolStripMenuItem _deleteMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _loadMenuItem;
	}
}
