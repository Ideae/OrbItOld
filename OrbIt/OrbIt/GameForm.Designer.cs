namespace OrbIt
{
    partial class GameForm
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
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Game");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("VelocityMultiplier");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Mass");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Orb Radius");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Orb", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("GravityMultiplier");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("GravityNode", new System.Windows.Forms.TreeNode[] {
            treeNode13});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.modifyButton = new System.Windows.Forms.Button();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.Selected = new System.Windows.Forms.Label();
            this.collisionCheckBox = new System.Windows.Forms.CheckBox();
            this.MapCheckBox = new System.Windows.Forms.CheckBox();
            this.clrOrbsButton = new System.Windows.Forms.Button();
            this.fixCollisionCheckBox = new System.Windows.Forms.CheckBox();
            this.labelColorMode = new System.Windows.Forms.Label();
            this.comboBoxColormode = new System.Windows.Forms.ComboBox();
            this.ControlsLabel = new System.Windows.Forms.Label();
            this.fullLightCB = new System.Windows.Forms.CheckBox();
            this.multLightCB = new System.Windows.Forms.CheckBox();
            this.bulletsOnCB = new System.Windows.Forms.CheckBox();
            this.clearNodesB = new System.Windows.Forms.Button();
            this.slowdownB = new System.Windows.Forms.Button();
            this.frictionCB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 36);
            this.treeView1.Name = "treeView1";
            treeNode8.Name = "Game";
            treeNode8.Text = "Game";
            treeNode9.Name = "VelMult";
            treeNode9.Text = "VelocityMultiplier";
            treeNode10.Name = "Mass";
            treeNode10.Text = "Mass";
            treeNode11.Name = "OrbRadius";
            treeNode11.Text = "Orb Radius";
            treeNode12.Name = "Orb";
            treeNode12.Text = "Orb";
            treeNode13.Name = "GravMult";
            treeNode13.Text = "GravityMultiplier";
            treeNode14.Name = "GravityNode";
            treeNode14.Text = "GravityNode";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode12,
            treeNode14});
            this.treeView1.Size = new System.Drawing.Size(171, 136);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // modifyButton
            // 
            this.modifyButton.Location = new System.Drawing.Point(189, 62);
            this.modifyButton.Name = "modifyButton";
            this.modifyButton.Size = new System.Drawing.Size(101, 23);
            this.modifyButton.TabIndex = 1;
            this.modifyButton.Text = "Modify";
            this.modifyButton.UseVisualStyleBackColor = true;
            this.modifyButton.Click += new System.EventHandler(this.modifyButton_Click);
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point(189, 36);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(101, 20);
            this.valueTextBox.TabIndex = 2;
            // 
            // Selected
            // 
            this.Selected.AutoSize = true;
            this.Selected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Selected.Location = new System.Drawing.Point(189, 13);
            this.Selected.Name = "Selected";
            this.Selected.Size = new System.Drawing.Size(2, 15);
            this.Selected.TabIndex = 3;
            // 
            // collisionCheckBox
            // 
            this.collisionCheckBox.AutoSize = true;
            this.collisionCheckBox.Checked = true;
            this.collisionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.collisionCheckBox.Location = new System.Drawing.Point(12, 190);
            this.collisionCheckBox.Name = "collisionCheckBox";
            this.collisionCheckBox.Size = new System.Drawing.Size(64, 17);
            this.collisionCheckBox.TabIndex = 4;
            this.collisionCheckBox.Text = "Collision";
            this.collisionCheckBox.UseVisualStyleBackColor = true;
            this.collisionCheckBox.CheckedChanged += new System.EventHandler(this.collisionCheckBox_CheckedChanged);
            // 
            // MapCheckBox
            // 
            this.MapCheckBox.AutoSize = true;
            this.MapCheckBox.Checked = true;
            this.MapCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MapCheckBox.Location = new System.Drawing.Point(12, 284);
            this.MapCheckBox.Name = "MapCheckBox";
            this.MapCheckBox.Size = new System.Drawing.Size(47, 17);
            this.MapCheckBox.TabIndex = 5;
            this.MapCheckBox.Text = "Map";
            this.MapCheckBox.UseVisualStyleBackColor = true;
            this.MapCheckBox.CheckedChanged += new System.EventHandler(this.MapCheckBox_CheckedChanged);
            // 
            // clrOrbsButton
            // 
            this.clrOrbsButton.Location = new System.Drawing.Point(229, 231);
            this.clrOrbsButton.Name = "clrOrbsButton";
            this.clrOrbsButton.Size = new System.Drawing.Size(75, 23);
            this.clrOrbsButton.TabIndex = 6;
            this.clrOrbsButton.Text = "Clear Orbs";
            this.clrOrbsButton.UseVisualStyleBackColor = true;
            this.clrOrbsButton.Click += new System.EventHandler(this.clrOrbsButton_Click);
            // 
            // fixCollisionCheckBox
            // 
            this.fixCollisionCheckBox.AutoSize = true;
            this.fixCollisionCheckBox.Checked = true;
            this.fixCollisionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fixCollisionCheckBox.Location = new System.Drawing.Point(11, 213);
            this.fixCollisionCheckBox.Name = "fixCollisionCheckBox";
            this.fixCollisionCheckBox.Size = new System.Drawing.Size(80, 17);
            this.fixCollisionCheckBox.TabIndex = 8;
            this.fixCollisionCheckBox.Text = "Fix Collision";
            this.fixCollisionCheckBox.UseVisualStyleBackColor = true;
            this.fixCollisionCheckBox.CheckedChanged += new System.EventHandler(this.fixCollisionCheckBox_CheckedChanged);
            // 
            // labelColorMode
            // 
            this.labelColorMode.AutoSize = true;
            this.labelColorMode.Location = new System.Drawing.Point(213, 170);
            this.labelColorMode.Name = "labelColorMode";
            this.labelColorMode.Size = new System.Drawing.Size(61, 13);
            this.labelColorMode.TabIndex = 10;
            this.labelColorMode.Text = "Color Mode";
            // 
            // comboBoxColormode
            // 
            this.comboBoxColormode.FormattingEnabled = true;
            this.comboBoxColormode.Items.AddRange(new object[] {
            "normal",
            "disco",
            "wave",
            "wave2",
            "wave3",
            "wave4",
            "wave5"});
            this.comboBoxColormode.Location = new System.Drawing.Point(183, 186);
            this.comboBoxColormode.Name = "comboBoxColormode";
            this.comboBoxColormode.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColormode.TabIndex = 11;
            this.comboBoxColormode.SelectedIndexChanged += new System.EventHandler(this.comboBoxColormode_SelectedIndexChanged);
            // 
            // ControlsLabel
            // 
            this.ControlsLabel.AutoSize = true;
            this.ControlsLabel.Location = new System.Drawing.Point(12, 370);
            this.ControlsLabel.Name = "ControlsLabel";
            this.ControlsLabel.Size = new System.Drawing.Size(156, 130);
            this.ControlsLabel.TabIndex = 12;
            this.ControlsLabel.Text = resources.GetString("ControlsLabel.Text");
            // 
            // fullLightCB
            // 
            this.fullLightCB.AutoSize = true;
            this.fullLightCB.Location = new System.Drawing.Point(11, 237);
            this.fullLightCB.Name = "fullLightCB";
            this.fullLightCB.Size = new System.Drawing.Size(100, 17);
            this.fullLightCB.TabIndex = 13;
            this.fullLightCB.Text = "Full Lightsource";
            this.fullLightCB.UseVisualStyleBackColor = true;
            this.fullLightCB.CheckedChanged += new System.EventHandler(this.fullLightCB_CheckedChanged);
            // 
            // multLightCB
            // 
            this.multLightCB.AutoSize = true;
            this.multLightCB.Location = new System.Drawing.Point(11, 261);
            this.multLightCB.Name = "multLightCB";
            this.multLightCB.Size = new System.Drawing.Size(125, 17);
            this.multLightCB.TabIndex = 14;
            this.multLightCB.Text = "Multiple Lightsources";
            this.multLightCB.UseVisualStyleBackColor = true;
            this.multLightCB.CheckedChanged += new System.EventHandler(this.multLightCB_CheckedChanged);
            // 
            // bulletsOnCB
            // 
            this.bulletsOnCB.AutoSize = true;
            this.bulletsOnCB.Location = new System.Drawing.Point(11, 308);
            this.bulletsOnCB.Name = "bulletsOnCB";
            this.bulletsOnCB.Size = new System.Drawing.Size(57, 17);
            this.bulletsOnCB.TabIndex = 15;
            this.bulletsOnCB.Text = "Bullets";
            this.bulletsOnCB.UseVisualStyleBackColor = true;
            this.bulletsOnCB.CheckedChanged += new System.EventHandler(this.bulletsOnCB_CheckedChanged);
            // 
            // clearNodesB
            // 
            this.clearNodesB.Location = new System.Drawing.Point(229, 261);
            this.clearNodesB.Name = "clearNodesB";
            this.clearNodesB.Size = new System.Drawing.Size(75, 23);
            this.clearNodesB.TabIndex = 16;
            this.clearNodesB.Text = "Clear Nodes";
            this.clearNodesB.UseVisualStyleBackColor = true;
            this.clearNodesB.Click += new System.EventHandler(this.clearNodesB_Click);
            // 
            // slowdownB
            // 
            this.slowdownB.Location = new System.Drawing.Point(229, 291);
            this.slowdownB.Name = "slowdownB";
            this.slowdownB.Size = new System.Drawing.Size(75, 23);
            this.slowdownB.TabIndex = 17;
            this.slowdownB.Text = "Slowdown";
            this.slowdownB.UseVisualStyleBackColor = true;
            this.slowdownB.Click += new System.EventHandler(this.slowdownB_Click);
            // 
            // frictionCB
            // 
            this.frictionCB.AutoSize = true;
            this.frictionCB.Location = new System.Drawing.Point(11, 332);
            this.frictionCB.Name = "frictionCB";
            this.frictionCB.Size = new System.Drawing.Size(60, 17);
            this.frictionCB.TabIndex = 18;
            this.frictionCB.Text = "Friction";
            this.frictionCB.UseVisualStyleBackColor = true;
            this.frictionCB.CheckedChanged += new System.EventHandler(this.frictionCB_CheckedChanged);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 509);
            this.Controls.Add(this.frictionCB);
            this.Controls.Add(this.slowdownB);
            this.Controls.Add(this.clearNodesB);
            this.Controls.Add(this.bulletsOnCB);
            this.Controls.Add(this.multLightCB);
            this.Controls.Add(this.fullLightCB);
            this.Controls.Add(this.ControlsLabel);
            this.Controls.Add(this.comboBoxColormode);
            this.Controls.Add(this.labelColorMode);
            this.Controls.Add(this.fixCollisionCheckBox);
            this.Controls.Add(this.clrOrbsButton);
            this.Controls.Add(this.MapCheckBox);
            this.Controls.Add(this.collisionCheckBox);
            this.Controls.Add(this.Selected);
            this.Controls.Add(this.valueTextBox);
            this.Controls.Add(this.modifyButton);
            this.Controls.Add(this.treeView1);
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button modifyButton;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.Label Selected;
        private System.Windows.Forms.CheckBox collisionCheckBox;
        private System.Windows.Forms.CheckBox MapCheckBox;
        private System.Windows.Forms.Button clrOrbsButton;
        private System.Windows.Forms.CheckBox fixCollisionCheckBox;
        private System.Windows.Forms.Label labelColorMode;
        private System.Windows.Forms.ComboBox comboBoxColormode;
        private System.Windows.Forms.Label ControlsLabel;
        private System.Windows.Forms.CheckBox fullLightCB;
        private System.Windows.Forms.CheckBox multLightCB;
        private System.Windows.Forms.CheckBox bulletsOnCB;
        private System.Windows.Forms.Button clearNodesB;
        private System.Windows.Forms.Button slowdownB;
        private System.Windows.Forms.CheckBox frictionCB;

    }
}