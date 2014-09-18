namespace AutoControlLib
{
    partial class AutoControl
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
            this._mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // _mainLayout
            // 
            this._mainLayout.ColumnCount = 2;
            this._mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainLayout.Location = new System.Drawing.Point(0, 0);
            this._mainLayout.Name = "_mainLayout";
            this._mainLayout.RowCount = 1;
            this._mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._mainLayout.Size = new System.Drawing.Size(150, 150);
            this._mainLayout.TabIndex = 0;
            // 
            // AutoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._mainLayout);
            this.Name = "AutoControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _mainLayout;
    }
}
