using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace sodoku
{
    public partial class Form1 : Form
    {
        public Dictionary<int, Grupo> grupos= new Dictionary<int,Grupo>();
        
        public Form1()
        {
            InitializeComponent();
            Metodos.CreaGrupos(ref grupos);
            //MessageBox.Show("Se crearon los grupos."); 
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Metodos.AgregaTodos(ref grupos);

            foreach (KeyValuePair<int, Grupo> kvp in grupos)
            {
                Grupo gpo = kvp.Value;
                if(gpo.Valor==0)
                {
                    //MessageBox.Show("CERO");
                }
            }

            LlenaControles();  
        }

        private void LlenaControles() {
            int pos=0;
            foreach (Control ctrl in this.Controls) {
                if (ctrl is TextBox) { 
                    pos++;
                    ctrl.Text=grupos[pos].Valor.ToString();
                }
            
            }
        }
    }
}
