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
            table.Columns.Add("Message", typeof(string));

            dataGridView1.DataSource = table;

            // hide the message column and set title width if columns exist
            if (dataGridView1.Columns.Contains("Message"))
                dataGridView1.Columns["Message"].Visible = false;
            if (dataGridView1.Columns.Contains("Title"))
                dataGridView1.Columns["Title"].Width = 185;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
                return;

            int index = dataGridView1.CurrentCell.RowIndex;

            if (index >= 0 && index < dataGridView1.Rows.Count)
            {
                var titleVal = dataGridView1.Rows[index].Cells[0].Value;
                var messageVal = dataGridView1.Rows[index].Cells[1].Value;
                textBox1.Text = titleVal?.ToString() ?? string.Empty;
                textBox2.Text = messageVal?.ToString() ?? string.Empty;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
                return;

            int index = dataGridView1.CurrentCell.RowIndex;
            if (index >= 0 && index < table.Rows.Count)
                table.Rows[index].Delete();
        }
    }
}
