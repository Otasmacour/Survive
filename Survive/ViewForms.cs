using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Survive
{
    public partial class ViewForms : Form
    {
        Controller controller;
        public ViewForms()
        {
            controller = new Controller(this);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler((sender, e) => KeyPresss(sender, (KeyPressEventArgs)e));
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler((sender, e) => ColorLabels(sender, (KeyPressEventArgs)e));
            //Proxy.Set();
            Proxy.outputType = OutputType.Forms;
            InitializeComponent();
            controller.Play();
        }
        private void KeyPresss(object sender, KeyPressEventArgs e)
        {
            Proxy.SetCurrentKey(e.KeyChar);
        }
        public void Display()
        {
            //Informations informations = new Informations();
            Random rnd = new Random();
            int mapWidth = 10;
            int mapHeight = 5;
            List<GameObject>[,] twoDArray = new List<GameObject>[mapHeight, mapWidth];
            for (int i = 0; i < 5 * 10; i++)
            {
                int height = (i) / mapWidth;
                int width = i - ((i / mapWidth) * mapWidth);
                GameObject element = new Wall();
                int n = rnd.Next(3);
                if (n == 0)
                {
                    element = new Wall();
                }
                if (n == 1)
                {
                    element = new Closet();
                }
                else if (n == 2)
                {
                    element = new Floor();
                }
                twoDArray[height, width] = new List<GameObject> { element };
            }
            ShowMap(twoDArray);
        }
        private void view_Load(object sender, EventArgs e)
        {

        }
    }
}