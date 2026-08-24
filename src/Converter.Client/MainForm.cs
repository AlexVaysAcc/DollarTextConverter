using Serilog;
using System.Diagnostics;

namespace Converter.Client
{
    public partial class
        MainForm : Form
    {
        private const int MAX_DOLLARS = 999999999;
        private const int MAX_CENTS = 99;

        private readonly ConvertorApiClient mApiClient = new ConvertorApiClient();
        private bool mIsRequestInProcess = false;

        public MainForm()
        {
            InitializeComponent();
            SetupGUI();
            Log.Information("MainForm initialized successfully.");
        }

        private void SetupGUI()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            btnConvert.Enabled = false;

            comboBoxLanguage.Text = "Select Language";
            comboBoxLanguage.Items.Add("English");
            comboBoxLanguage.Items.Add("Deutsch");

            labelDisplay.BorderStyle = BorderStyle.Fixed3D;
            labelDisplay.AutoSize = false;
            labelDisplay.Size = new Size(1000, 200);
            labelDisplay.BackColor = Color.AliceBlue;
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            Log.Information("User clicked 'Convert' button. Raw input sequence: '{RawInput}'", textBoxAmount.Text);

            string cleanInput = textBoxAmount.Text.Trim().Replace(" ", "");

            if (!ValidateInput(cleanInput, out long dollars, out int cents, out string lang))
            {
                return;
            }

            btnConvert.Enabled = false;
            mIsRequestInProcess = true;
            labelDisplay.ForeColor = Color.Green;
            labelDisplay.Text = "Sending request...";

            string result = await mApiClient.ConvertDollarAsync(dollars, cents, lang);

            if (result.StartsWith("Server") || result.StartsWith("HTTP") || result.StartsWith("Unexpected"))
            {
                Log.Warning("Conversion failed. Displaying error result to the user: {Result}", result);
                labelDisplay.ForeColor = Color.Red;
            } 
            else 
            {
                Log.Information("Conversion completed successfully.");
            }

            labelDisplay.Text = result;
            btnConvert.Enabled = true;
            mIsRequestInProcess = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult iExit;

            iExit = MessageBox.Show("Do you want to exit", "Confirmation Dialog Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (iExit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (mIsRequestInProcess)
            {
                Log.Warning("Reset is blocked while the user attempted to reset the form while an active HTTP web request was processing.");
                MessageBox.Show("The reset cannot be performed.\nThe request is in process.", "Warning Dialog Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult iReset;

            iReset = MessageBox.Show("Do you want to reset", "Confirmation Dialog Box", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (iReset == DialogResult.Yes)
            {
                Log.Information("Form fields were reset to default values by user.");
                comboBoxLanguage.SelectedIndex = -1;
                comboBoxLanguage.Text = "Select Language";
                textBoxAmount.Text = "";
                textBoxAmount.PlaceholderText = "0";
                labelDisplay.Text = "";
            }
        }

        private bool ValidateInput(string rawInput, out long dollars, out int cents, out string lang)
        {
            string[] numbers = rawInput.Split(',');

            // Initializes out variables to safe defaults
            dollars = 0;
            cents = 0;
            lang = string.Empty;

            // Validates Dollars
            if (!long.TryParse(numbers[0], out dollars) || dollars < 0 || dollars > MAX_DOLLARS)
            {
                Log.Warning("Validation failed: Invalid dollars value parsing string '{Input}'", numbers[0]);
                ShowValidationError($"The {rawInput} is wrong Input!\nPlease enter dollars amount from 0 to {MAX_DOLLARS}.");
                return false;
            }

            // Validates Cents
            if (numbers.Length > 1)
            {
                string centsStr = numbers[1];

                if (centsStr.Length > 2 || !int.TryParse(centsStr, out cents) || cents < 0 || cents > MAX_CENTS)
                {
                    Log.Warning("Validation failed: Invalid cents value parsing string '{Input}'", numbers[1]);
                    ShowValidationError($"The {centsStr} is wrong input for cents!\nPlease enter correct amount of cents.");
                    return false;
                }

                if (centsStr.Length == 1)
                {
                    cents *= 10;
                }
            }

            // Validates Language
            if (comboBoxLanguage.SelectedItem is not string selectedLanguage)
            {
                Log.Warning("Validation failed: No language is selected in ComboBox.");
                ShowValidationError("Please select a language.");
                return false;
            }

            // Determines Language String
            lang = selectedLanguage switch
            {
                "English" => "en",
                "Deutsch" => "de",
                _ => string.Empty
            };

            Log.Debug("Input parsed successfully. Details -> Dollars: {Dollars}, Cents: {Cents}, MappedLang: {Lang}", dollars, cents, lang);
            return true;
        }
        private void ShowValidationError(string message)
        {
            labelDisplay.ForeColor = Color.Red;
            labelDisplay.Text = message;
        }

        private void textBoxAmount_TextChanged(object sender, EventArgs e)
        {
            btnConvert.Enabled = !string.IsNullOrEmpty(textBoxAmount.Text);
        }
    }
}
