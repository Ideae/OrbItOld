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
using OrbIt.GameObjects;

namespace OrbIt
{
    public partial class GameForm : Form
    {
        String parentnode;
        String lastItem;
        Game1 game;

        Random seed;
        Random rand;

        public GameForm(Game1 game)
        {
            this.game = game;
            InitializeComponent();
            seed = new Random(1000);
            rand = new Random(seed.Next());
            
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
                game.room1.PropertiesDict["collisionOn"] = true;
            }
            else game.room1.PropertiesDict["collisionOn"] = false;
        }

        private void MapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MapCheckBox.Checked)
            {
                game.room1.PropertiesDict["mapOn"] = true;
            }
            else game.room1.PropertiesDict["mapOn"] = false;
        }

        

        private void fixCollisionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fixCollisionCheckBox.Checked)
            {
                game.room1.PropertiesDict["fixCollisionOn"] = true;
            }
            else game.room1.PropertiesDict["fixCollisionOn"] = false;
        }

        private void comboBoxColormode_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBoxColormode.Text.Equals("normal")) game.room1.level.colorMode = "normal";
            else if (comboBoxColormode.Text.Equals("disco")) game.room1.level.colorMode = "disco";
            else
            {
                
                game.room1.level.colorMode = comboBoxColormode.Text;
                for (int x = 0; x < game.room1.level.tileAmount.X; x++)
                    for (int y = 0; y < game.room1.level.tileAmount.Y; y++)
                        for (int z = 0; z < game.room1.level.tileAmount.Z; z++)
                        {
                            int nextRand = rand.Next(3);
                            if (nextRand == 0)
                            {
                                nextRand = rand.Next(50);
                                game.room1.level.tile[x, y, z].color = new Microsoft.Xna.Framework.Color(((x + 1) * nextRand) % 255, ((y + 1) * nextRand) % 255, ((z + 1) * nextRand) % 255);
                            }
                            else
                            {
                                int levelwidth = (int)game.room1.level.tileAmount.X;
                                int levelheight = (int)game.room1.level.tileAmount.Y;
                                game.room1.level.tile[x, y, z].color = new Microsoft.Xna.Framework.Color((int)(x / levelwidth) * 255, (int)(y / levelheight) * 255, 125);
                            
                            }
                        }
            }
        }

        private void fullLightCB_CheckedChanged(object sender, EventArgs e) {
            if (fullLightCB.Checked)
            {
                game.room1.PropertiesDict["fullLightOn"] = true;
            }
            else game.room1.PropertiesDict["fullLightOn"] = false;
        }

        private void multLightCB_CheckedChanged(object sender, EventArgs e) {
            if (multLightCB.Checked)
            {
                game.room1.PropertiesDict["smallLightsOn"] = true;
            }
            else game.room1.PropertiesDict["smallLightsOn"] = false;
        }

        private void bulletsOnCB_CheckedChanged(object sender, EventArgs e)
        {
            if (bulletsOnCB.Checked)
            {
                game.room1.PropertiesDict["bulletsOn"] = true;
            }
            else game.room1.PropertiesDict["bulletsOn"] = false;
        }

        private void clrOrbsButton_Click(object sender, EventArgs e)
        {
            int count = game.room1.GameObjectDict["orbs"].Count;
            for (int i = 0; i < count; i++)
            {
                game.room1.GameObjectDict["orbs"].RemoveAt(0);
            }
        }

        private void clearNodesB_Click(object sender, EventArgs e)
        {
            int count = game.room1.GameObjectDict["gnodes"].Count;
            for (int i = 0; i < count; i++)
            {
                game.room1.GameObjectDict["gnodes"].RemoveAt(0);
            }
            count = game.room1.GameObjectDict["rnodes"].Count;
            for (int i = 0; i < count; i++)
            {
                game.room1.GameObjectDict["rnodes"].RemoveAt(0);
            }
            count = game.room1.GameObjectDict["snodes"].Count;
            for (int i = 0; i < count; i++)
            {
                game.room1.GameObjectDict["snodes"].RemoveAt(0);
            }
            count = game.room1.GameObjectDict["tnodes"].Count;
            for (int i = 0; i < count; i++)
            {
                game.room1.GameObjectDict["tnodes"].RemoveAt(0);
            }
        }

        private void slowdownB_Click(object sender, EventArgs e)
        {
            int count = game.room1.GameObjectDict["orbs"].Count;
            for (int i = 0; i < count; i++)
            {
                if (game.room1.GameObjectDict["orbs"][i] is MoveableObject)
                {
                    MoveableObject mo = (MoveableObject)game.room1.GameObjectDict["orbs"][i];
                    mo.velocity.X *= 0.5f;
                    mo.velocity.Y *= 0.5f;
                }
            }
            count = game.room1.GameObjectDict["gnodes"].Count;
            for (int i = 0; i < count; i++)
            {
                if (game.room1.GameObjectDict["gnodes"][i] is MoveableObject)
                {
                    MoveableObject mo = (MoveableObject)game.room1.GameObjectDict["gnodes"][i];
                    mo.velocity.X *= 0.5f;
                    mo.velocity.Y *= 0.5f;
                }
            }
            count = game.room1.GameObjectDict["rnodes"].Count;
            for (int i = 0; i < count; i++)
            {
                if (game.room1.GameObjectDict["rnodes"][i] is MoveableObject)
                {
                    MoveableObject mo = (MoveableObject)game.room1.GameObjectDict["rnodes"][i];
                    mo.velocity.X *= 0.5f;
                    mo.velocity.Y *= 0.5f;
                }
            }
            count = game.room1.GameObjectDict["snodes"].Count;
            for (int i = 0; i < count; i++)
            {
                if (game.room1.GameObjectDict["snodes"][i] is MoveableObject)
                {
                    MoveableObject mo = (MoveableObject)game.room1.GameObjectDict["snodes"][i];
                    mo.velocity.X *= 0.5f;
                    mo.velocity.Y *= 0.5f;
                }
            }
            count = game.room1.GameObjectDict["tnodes"].Count;
            for (int i = 0; i < count; i++)
            {
                if (game.room1.GameObjectDict["tnodes"][i] is MoveableObject)
                {
                    MoveableObject mo = (MoveableObject)game.room1.GameObjectDict["tnodes"][i];
                    mo.velocity.X *= 0.5f;
                    mo.velocity.Y *= 0.5f;
                }
            }
        }

        private void frictionCB_CheckedChanged(object sender, EventArgs e)
        {
            if (frictionCB.Checked)
            {
                game.room1.PropertiesDict["friction"] = true;
            }
            else game.room1.PropertiesDict["friction"] = false;
        }

        

    

       

        
    }
}
