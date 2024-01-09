using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;

namespace ConfigFile1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string InputBox(string prompt, string title, string defaultValue = "")
        {
            using (Form inputForm = new Form())
            {
                Label label = new Label() { Left = 20, Top = 20, Text = prompt };
                TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 200, Text = defaultValue };
                Button okButton = new Button() { Text = "OK", Left = 230, Top = 50, DialogResult = DialogResult.OK };
                Button cancelButton = new Button() { Text = "Cancel", Left = 230, Top = 75, DialogResult = DialogResult.Cancel };

                inputForm.Text = title;
                inputForm.AcceptButton = okButton;
                inputForm.CancelButton = cancelButton;

                inputForm.Controls.Add(label);
                inputForm.Controls.Add(textBox);
                inputForm.Controls.Add(okButton);
                inputForm.Controls.Add(cancelButton);

                return inputForm.ShowDialog() == DialogResult.OK ? textBox.Text : defaultValue;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            CountriesConfigurationSection countriesSection = (CountriesConfigurationSection)configuration.GetSection("countries");

            if (countriesSection != null)
            {
                string keyValuePairs = "";

                foreach (KeyValueConfigurationElement country in countriesSection.Countries)
                {
                    string name = country.CountryName;
                    string code = country.CountryCode;
                    keyValuePairs += $"Country: {name}, CountryCode: {code}\n";
                }
                MessageBox.Show(keyValuePairs, "Country Information");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            CountriesConfigurationSection countriesSection = (CountriesConfigurationSection)configuration.GetSection("countries");

            foreach (KeyValueConfigurationElement country in countriesSection.Countries)
            {
                string code = country.CountryCode;
                string newCode = InputBox($"Enter new countryCode:", "Input", code);

                country.CountryCode = newCode;
            }
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("countries");

            MessageBox.Show("Values Updated");
        }
    }
}
