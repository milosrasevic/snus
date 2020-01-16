namespace Trending
{
    partial class TrendingForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrendingForm));
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.listViewTag = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.outVal = new System.Windows.Forms.NumericUpDown();
            this.outName = new System.Windows.Forms.TextBox();
            this.sendOutBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.simVal = new System.Windows.Forms.NumericUpDown();
            this.simName = new System.Windows.Forms.TextBox();
            this.sendSimBtn = new System.Windows.Forms.Button();
            this.valueSim = new System.Windows.Forms.Label();
            this.tagNameSim = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outVal)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simVal)).BeginInit();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(13, 12);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(1048, 611);
            this.zedGraphControl1.TabIndex = 3;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // listViewTag
            // 
            this.listViewTag.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16});
            this.listViewTag.Location = new System.Drawing.Point(1067, 12);
            this.listViewTag.Name = "listViewTag";
            this.listViewTag.Size = new System.Drawing.Size(433, 306);
            this.listViewTag.TabIndex = 4;
            this.listViewTag.UseCompatibleStateImageBehavior = false;
            this.listViewTag.View = System.Windows.Forms.View.Details;
            this.listViewTag.SelectedIndexChanged += new System.EventHandler(this.listViewTag_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Tag name";
            this.columnHeader4.Width = 84;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Description";
            this.columnHeader5.Width = 78;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Driver";
            this.columnHeader6.Width = 67;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "I/O Address";
            this.columnHeader7.Width = 87;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Scan time";
            this.columnHeader8.Width = 61;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Alarm floor";
            this.columnHeader9.Width = 67;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Alarm ceiling";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Scan";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Auto";
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Initial value";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Limit low";
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Limit high";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Units";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(1067, 324);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(433, 299);
            this.splitContainer1.SplitterDistance = 337;
            this.splitContainer1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel2.Controls.Add(this.outVal);
            this.panel2.Controls.Add(this.outName);
            this.panel2.Controls.Add(this.sendOutBtn);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(92, 187);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 109);
            this.panel2.TabIndex = 10;
            // 
            // outVal
            // 
            this.outVal.Location = new System.Drawing.Point(90, 33);
            this.outVal.Name = "outVal";
            this.outVal.Size = new System.Drawing.Size(133, 20);
            this.outVal.TabIndex = 9;
            // 
            // outName
            // 
            this.outName.Location = new System.Drawing.Point(90, 7);
            this.outName.Name = "outName";
            this.outName.Size = new System.Drawing.Size(133, 20);
            this.outName.TabIndex = 10;
            this.outName.TextChanged += new System.EventHandler(this.outName_TextChanged);
            // 
            // sendOutBtn
            // 
            this.sendOutBtn.BackColor = System.Drawing.Color.White;
            this.sendOutBtn.Location = new System.Drawing.Point(30, 71);
            this.sendOutBtn.Name = "sendOutBtn";
            this.sendOutBtn.Size = new System.Drawing.Size(184, 23);
            this.sendOutBtn.TabIndex = 7;
            this.sendOutBtn.Text = "Send Output Value";
            this.sendOutBtn.UseVisualStyleBackColor = false;
            this.sendOutBtn.Click += new System.EventHandler(this.sendOutBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tag name";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel1.Controls.Add(this.simVal);
            this.panel1.Controls.Add(this.simName);
            this.panel1.Controls.Add(this.sendSimBtn);
            this.panel1.Controls.Add(this.valueSim);
            this.panel1.Controls.Add(this.tagNameSim);
            this.panel1.Location = new System.Drawing.Point(92, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 109);
            this.panel1.TabIndex = 9;
            // 
            // simVal
            // 
            this.simVal.Location = new System.Drawing.Point(90, 33);
            this.simVal.Name = "simVal";
            this.simVal.Size = new System.Drawing.Size(133, 20);
            this.simVal.TabIndex = 8;
            // 
            // simName
            // 
            this.simName.Location = new System.Drawing.Point(90, 7);
            this.simName.Name = "simName";
            this.simName.Size = new System.Drawing.Size(133, 20);
            this.simName.TabIndex = 8;
            this.simName.TextChanged += new System.EventHandler(this.simName_TextChanged);
            // 
            // sendSimBtn
            // 
            this.sendSimBtn.BackColor = System.Drawing.Color.White;
            this.sendSimBtn.Location = new System.Drawing.Point(30, 68);
            this.sendSimBtn.Name = "sendSimBtn";
            this.sendSimBtn.Size = new System.Drawing.Size(184, 23);
            this.sendSimBtn.TabIndex = 7;
            this.sendSimBtn.Text = "Send Simulation Value";
            this.sendSimBtn.UseVisualStyleBackColor = false;
            this.sendSimBtn.Click += new System.EventHandler(this.sendSimBtn_Click);
            // 
            // valueSim
            // 
            this.valueSim.AutoSize = true;
            this.valueSim.Location = new System.Drawing.Point(11, 40);
            this.valueSim.Name = "valueSim";
            this.valueSim.Size = new System.Drawing.Size(34, 13);
            this.valueSim.TabIndex = 2;
            this.valueSim.Text = "Value";
            // 
            // tagNameSim
            // 
            this.tagNameSim.AutoSize = true;
            this.tagNameSim.Location = new System.Drawing.Point(11, 14);
            this.tagNameSim.Name = "tagNameSim";
            this.tagNameSim.Size = new System.Drawing.Size(55, 13);
            this.tagNameSim.TabIndex = 1;
            this.tagNameSim.Text = "Tag name";
            // 
            // TrendingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(1512, 635);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.listViewTag);
            this.Controls.Add(this.zedGraphControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TrendingForm";
            this.Text = "Trending";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outVal)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simVal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.ListView listViewTag;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown outVal;
        private System.Windows.Forms.TextBox outName;
        private System.Windows.Forms.Button sendOutBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown simVal;
        private System.Windows.Forms.TextBox simName;
        private System.Windows.Forms.Button sendSimBtn;
        private System.Windows.Forms.Label valueSim;
        private System.Windows.Forms.Label tagNameSim;
    }
}

