using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrbIt
{
    public partial class GameForm : Form
    {
        String parentnode;
        String lastItem;
        Game1 game;
        public GameForm(Game1 game)
        {
            this.game = game;
            InitializeComponent();
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Selected.Text = treeView1.SelectedNode.Text;
            if (treeView1.SelectedNode.Parent != null)
                parentnode = treeView1.SelectedNode.Parent.Text;
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            //String selected = treeView1.SelectedNode.Text;
            //String parentnode = treeView1.SelectedNode.Parent.Text;
            String textValue = valueTextBox.Text;
            if (parentnode.Equals("Orb"))
            {
                if (Selected.Text.Equals("VelocityMultiplier"))
                    game.orbVelMult = float.Parse(textValue);
                if (Selected.Text.Equals("Orb Radius"))
                    game.initialOrbRadius = float.Parse(textValue);
            }
            
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        private void collisionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (collisionCheckBox.Checked)
            {
                game.collisionOn = true;
            }
            else game.collisionOn = false;
        }

        private void MapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MapCheckBox.Checked)
            {
                game.mapOn = true;
            }
            else game.mapOn = false;
        }

        private void clrOrbsButton_Click(object sender, EventArgs e)
        {
            int count = game.orbs.Count;
            for (int i = 0; i < count; i++)
            {
                game.orbs.RemoveAt(0);
            }
        }

        private void fixCollisionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fixCollisionCheckBox.Checked)
            {
                game.fixCollisionOn = true;
            }
            else game.fixCollisionOn = false;
        }

        private void comboBoxColormode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxColormode.Text.Equals("normal")) game.level1.colorMode = "normal";
            else if (comboBoxColormode.Text.Equals("disco")) game.level1.colorMode = "disco";
            else if (comboBoxColormode.Text.Equals("wave") || comboBoxColormode.Text.Equals("wave2") ||comboBoxColormode.Text.Equals("wave3")) 
            { 
                game.level1.colorMode = comboBoxColormode.Text;
                for (int x = 0; x < game.level1.tileAmount.X; x++)
                    for (int y = 0; y < game.level1.tileAmount.Y; y++)
                        for (int z = 0; z < game.level1.tileAmount.Z; z++)
                        { game.level1.tile[x, y, z].color = new Microsoft.Xna.Framework.Color(((x + 1) * 15) % 255, ((y + 1) * 15) % 255, ((z + 1) * 15) % 255); }
            }
        }

        private void fullLightCB_CheckedChanged(object sender, EventArgs e) {
            if (fullLightCB.Checked)
            {
                game.fullLightOn = true;
            }
            else game.fullLightOn = false;
        }

        private void multLightCB_CheckedChanged(object sender, EventArgs e) {
            if (multLightCB.Checked)
            {
                game.smallLightsOn = true;
            }
            else game.smallLightsOn = false;
        }

        private void bulletsOnCB_CheckedChanged(object sender, EventArgs e)
        {
            if (bulletsOnCB.Checked)
            {
                game.bulletsOn = true;
            }
            else game.bulletsOn = false;
        }

        

    

       

        
    }
}
