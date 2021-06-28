using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// class 
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Подключение базы данных 
        /// </summary>
        SoccerContext db;
        
       /// <summary>
       /// Конструктор
       /// </summary>
        public Form1()
        {
            InitializeComponent();

            db = new SoccerContext();
            db.Players.Load();
            dataGridView1.DataSource = db.Players.Local.ToBindingList();
        }

        /// <summary>
        /// Добавление
        /// Из команд в БД формируем список (id команды, название команды) 
        /// Затем происходит добавление игрока (возраст, имя, позиция, команда за которую играет)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            PlayerForm plForm = new PlayerForm();

            List<Team> teams = db.Teams.ToList();
            plForm.comboBox2.DataSource = teams;
            plForm.comboBox2.ValueMember = "Id";
            plForm.comboBox2.DisplayMember = "Name";

            DialogResult result = plForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Player player = new Player();
            player.Age = (int)plForm.numericUpDown1.Value;
            player.Name = plForm.textBox1.Text;
            player.Position = plForm.comboBox1.SelectedItem.ToString();
            player.Team = (Team)plForm.comboBox2.SelectedItem;

            db.Players.Add(player);
            db.SaveChanges();

            MessageBox.Show("Новый футболист добавлен");
        }

        /// <summary>
        /// Редактирование данных игрока и команды
        /// Редактирование осуществляется только если выделена вся строка*
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Player player = db.Players.Find(id);

                PlayerForm plForm = new PlayerForm();
                plForm.numericUpDown1.Value = player.Age;
                plForm.comboBox1.SelectedItem = player.Position;
                plForm.textBox1.Text = player.Name;
            
                List<Team> teams = db.Teams.ToList();
                plForm.comboBox2.DataSource = teams;
                plForm.comboBox2.ValueMember = "Id";
                plForm.comboBox2.DisplayMember = "Name";

                if (player.Team != null)
                    plForm.comboBox2.SelectedValue = player.Team.Id;

                DialogResult result = plForm.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;

                player.Age = (int)plForm.numericUpDown1.Value;
                player.Name = plForm.textBox1.Text;
                player.Position = plForm.comboBox1.SelectedItem.ToString();
                player.Team = (Team)plForm.comboBox2.SelectedItem;

                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();

                MessageBox.Show("Объект обновлен");
            }
        }

        /// <summary>
        /// Удаление
        /// Удаление осуществляется только всей строки (выделена вся строка)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Player player = db.Players.Find(id);
                db.Players.Remove(player);
                db.SaveChanges();

                MessageBox.Show("Объект удален");
            }
        }
  
        /// <summary>
        /// Открыть форму с командами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            AllTeams teams = new AllTeams();
            teams.Show();
        }
    }
}