using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Note_App
{
    public partial class Form2 : Form
    {
        DataTable table;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            textBox1.Clear();
            textBox2.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            table.Rows.Add(textBox1.Text, textBox2.Text);

            textBox1.Clear();
            textBox2.Clear();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

            table = new DataTable();
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("message", typeof(string));

            dataGridView1.DataSource = table;

            dataGridView1.Columns["Message"].Visible = false;
            dataGridView1.Columns["Title"].Width = 190;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            if (index < -1)
            {
                textBox1.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            table.Rows[index].Delete();
        }
    }
}
