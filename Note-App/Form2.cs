using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Collections.ObjectModel;

namespace Note_App
{
    public partial class Form2 : Form
    {

        DataTable table;
        readonly string dataFilePath;

        private class Note
        {
            public string Title { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
        }
        public Form2()
        {
            InitializeComponent();
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dir = Path.Combine(appData, "Note-App");
            Directory.CreateDirectory(dir);
            dataFilePath = Path.Combine(dir, "notes.json");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            textBox1.Clear();
            textBox2.Clear();

        }

        private void LoadNotes()
        {
            try
            {
                if (!File.Exists(dataFilePath))
                    return;

                var json = File.ReadAllText(dataFilePath);
                if (string.IsNullOrWhiteSpace(json))
                    return;

                var notes = JsonSerializer.Deserialize<ObservableCollection<Note>>(json);
                if (notes == null)
                    return;

                foreach (var n in notes)
                    table.Rows.Add(n.Title ?? string.Empty, n.Message ?? string.Empty);
            }
            catch
            {
                // ignore load errors
            }
        }

        private void SaveNotes()
        {
            try
            {
                var notes = new List<Note>();
                foreach (DataRow row in table.Rows)
                {
                    var title = row.Field<string>("Title") ?? string.Empty;
                    var message = row.Field<string>("Message") ?? string.Empty;
                    notes.Add(new Note { Title = title, Message = message });
                }

                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(notes, options);
                File.WriteAllText(dataFilePath, json);
            }
            catch
            {
                // ignore save errors
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveNotes();
            base.OnFormClosing(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            table.Rows.Add(textBox1.Text, textBox2.Text);

            textBox1.Clear();
            textBox2.Clear();

            SaveNotes();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            table = new DataTable();
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("Message", typeof(string));

            dataGridView1.DataSource = table;

            // load persisted notes
            LoadNotes();
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

            textBox1.Clear();
            textBox2.Clear();

            SaveNotes();
        }
    }
}
