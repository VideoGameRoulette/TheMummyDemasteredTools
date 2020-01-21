using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace MummySaveTool
{
    public partial class UI : Form
    {
        DirectoryInfo currentDir;
        readonly string appLocation = Application.StartupPath;
        readonly string backupFolder = "\\Backup\\";
        string file = "";
        string destination = "";

        public UI()
        {
            InitializeComponent();
            if (Properties.Settings.Default.SaveFolder == "")
            {
                string input = Interaction.InputBox($"Locate your steam user id at: C:\\Program Files (x86)\\Steam\\userdata\\", "Error Missing Steam ID", "USERIDgoesHERE", -1, -1);
                Properties.Settings.Default.SaveFolder = $"C:\\Program Files (x86)\\Steam\\userdata\\{input}\\630310\\remote\\";
                Properties.Settings.Default.Save();
            }
            if (!Directory.Exists($"{appLocation}{backupFolder}"))
            {
                Directory.CreateDirectory($"{appLocation}{backupFolder}");
                Directory.CreateDirectory($"{appLocation}{backupFolder}Any%");
                Directory.CreateDirectory($"{appLocation}{backupFolder}Low%");
                Directory.CreateDirectory($"{appLocation}{backupFolder}100%");
                Directory.CreateDirectory($"{appLocation}{backupFolder}RBO");
            }
            currentDir = new DirectoryInfo($"{appLocation}{backupFolder}");
            GetFiles();
        }

        private void exportSave1_Click(object sender, EventArgs e)
        {
            exportSave("slot1.save");
        }

        private void exportSave2_Click(object sender, EventArgs e)
        {
            exportSave("slot2.save");
        }

        private void exportSave3_Click(object sender, EventArgs e)
        {
            exportSave("slot3.save");
        }

        private void importSave1_Click(object sender, EventArgs e)
        {
            importSave("slot1.save");
        }

        private void importSave2_Click(object sender, EventArgs e)
        {
            importSave("slot2.save");
        }

        private void importSave3_Click(object sender, EventArgs e)
        {
            importSave("slot3.save");
        }

        void GetFiles()
        {
            fileList.Items.Clear();
            String[] files = Directory.GetFiles($"{currentDir.ToString()}{categoryList.Text}");
            for (var i = 0; i < files.Length; i++)
            {
                fileList.Items.Add(files[i]);
            }
        }

        void exportSave(string _saveName)
        {
            file = Properties.Settings.Default.SaveFolder + _saveName;
            if (String.IsNullOrWhiteSpace(saveName.Text))
            {
                MessageBox.Show("No filename has been input by user. Please give your save a name.", "Error");
                return;
            }
            destination = $"{appLocation}{backupFolder}{categoryList.Text}\\{categoryList.Text}_{saveName.Text}.save";
            File.Copy(file, destination);
            GetFiles();
        }

        void importSave(string _saveName)
        {
            File.Delete(Properties.Settings.Default.SaveFolder + _saveName);
            file = fileList.SelectedItems.ToString();
            destination = Properties.Settings.Default.SaveFolder + _saveName;
            File.Copy(file, destination);
            GetFiles();
        }

        private void resetSaveFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Locate your steam user id at: C:\\Program Files (x86)\\Steam\\userdata\\", "Error Missing Steam ID", "USERIDgoesHERE", -1, -1);
            Properties.Settings.Default.SaveFolder = $"C:\\Program Files (x86)\\Steam\\userdata\\{input}\\630310\\remote\\";
            Properties.Settings.Default.Save();
        }

        private void categoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFiles();
        }
    }
}
