using System;
using System.Windows.Forms;
using System.Reflection;
using QuestionDatabase;
using LoggerUtils;
using ResultCodes;
using System.Text;
using System.Collections;

namespace QuestionsFormsTest
{
    public partial class SettingsForm : Form
    {
        private DatabaseController DatabaseController;
        private Control ConnectionValuesContainer;
        private ConnectionString ConnectionString;
        public SettingsForm(DatabaseController pDatabaseController)
        {
            try
            {
                InitializeComponent();
                DatabaseController = pDatabaseController;
                ConnectionString = new ConnectionString(DatabaseController.ConnectionString);
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                ConnectionValuesContainer = Controls["connectionContainer"];
                UpdateSettingFields();
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Updates the settings field with the data from the connection string object
        /// </summary>
        private void UpdateSettingFields()
        {
            try
            {
                Type tConnectionStringType = typeof(ConnectionString);
                PropertyInfo[] tConnectionStringProporties = tConnectionStringType.GetProperties();

                // Dynamically loop through the proporties of the connection string class and assigns the values of the input fields with the values in the connection string object
                foreach (PropertyInfo tConnectionStringProporty in tConnectionStringProporties)
                {
                    string tConnectionStringProportyName = tConnectionStringProporty.Name;

                    Control tCurrentConnectionStringField = ConnectionValuesContainer.Controls["input_" + tConnectionStringProportyName];
                    string tCurrentConnectionStringValue = tConnectionStringProporty.GetValue(ConnectionString, null).ToString();
                    tCurrentConnectionStringField.Text = tCurrentConnectionStringValue;

                    if (tConnectionStringProportyName.Equals("IntegratedSecurity"))
                    {
                        CheckIntegratedSecurityValue();
                    }
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Toggles the username and password buttons on and off based on the value of the Security type
        /// </summary>
        private void CheckIntegratedSecurityValue()
        {
            try
            {
                if (ConnectionValuesContainer.Controls["input_IntegratedSecurity"].Text.Equals("SSPI"))
                {
                    ToggleUsernamePasswordFields(false);
                }
                else
                {
                    ToggleUsernamePasswordFields(true);
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Helper function that toggles the username/password buttons based on the value given
        /// </summary>
        /// <param name="pValue">whether to show the username/password buttons</param>
        private void ToggleUsernamePasswordFields(bool pValue)
        {
            try
            {
                ConnectionValuesContainer.Controls["input_Username"].Enabled = pValue;
                ConnectionValuesContainer.Controls["input_Password"].Enabled = pValue;
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
            
        }

        /// <summary>
        /// Helper function that loops thro the props of the connection string class and gets the corosponding input field for it
        /// then assigns the value of the object prop to the value of the input field
        /// </summary>
        private void FillConnectionStringFields()
        {
            try
            {
                Type tConnectionStringType = typeof(ConnectionString);
                PropertyInfo[] tConnectionStringProporties = tConnectionStringType.GetProperties();
                
                // Dynamically loop through the proporties of the connection string class and assigns the values of the connection string object to the values
                // of the input fields
                foreach (PropertyInfo tConnectionStringProporty in tConnectionStringProporties)
                {
                    string tConnectionStringProportyName = tConnectionStringProporty.Name;

                    Control tCurrentConnectionStringField = ConnectionValuesContainer.Controls["input_" + tConnectionStringProportyName];
                    tConnectionStringProporty.SetValue(ConnectionString, tCurrentConnectionStringField.Text.ToString(), null);
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// On click listener for the test button to test out the currently inputted connection string data
        /// </summary>
        /// <param name="sender">The control that fired the event</param>
        /// <param name="e">Extra data about the event</param>
        private void testBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckFormFields())
                {
                    return;
                }

                FillConnectionStringFields();
                int tResponseCode = DatabaseController.TestDatabaseConnection(ConnectionString);
                string tResultMessage = "";
                MessageBoxIcon tIcon = MessageBoxIcon.Error;


                switch ((ResultCodesEnum) tResponseCode)
                {
                    case ResultCodesEnum.SUCCESS:
                        tResultMessage = "Connection success";
                        tIcon = MessageBoxIcon.Information;
                        break;
                    case ResultCodesEnum.DATABASE_AUTHENTICATION_FAILUER:
                        tResultMessage = "User not authenticated";
                        break;
                    case ResultCodesEnum.DATABASE_CONNECTION_DENIED:
                        tResultMessage = "Connection to database denied";
                        break;
                    case ResultCodesEnum.DATABASE_CONNECTION_FAILURE:
                        tResultMessage = "Connection to database failed";
                        break;
                    case ResultCodesEnum.SERVER_CONNECTION_FAILURE:
                        tResultMessage = "Connection to server failed";
                        break;
                    case ResultCodesEnum.SERVER_PAUSED:
                        tResultMessage = "Connection to server failed, server is paused";
                        break;
                    case ResultCodesEnum.SERVER_NOT_FOUND_OR_DOWN:
                        tResultMessage = "Connection to server failed, server was not found or down";
                        break;
                    default:
                        tResultMessage = "Connection failure";
                        break;
                }

                string tMessageCaption = "Connection test result";
                MessageBoxButtons tMessageButtons = MessageBoxButtons.OK;

                MessageBox.Show(tResultMessage, tMessageCaption, tMessageButtons, tIcon);
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// On click listener for the save button which saves data to the app.config file then changes the connection string in the database object
        /// </summary>
        /// <param name="sender">The control that fired the event</param>
        /// <param name="e">Extra data about the event</param>
        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckFormFields())
                {
                    return;
                }

                FillConnectionStringFields();
                int tResponseCode = DatabaseController.ChangeConnectionString(ConnectionString);

                string tMessage;
                string tMessageCaption;
                MessageBoxButtons tMessageButtons = MessageBoxButtons.OK;
                MessageBoxIcon tIcon;

                if (ResultCodesEnum.SUCCESS == (ResultCodesEnum) tResponseCode)
                {
                    tMessage = "Connection string saved successfuly";
                    tMessageCaption = "Success";
                    tIcon = MessageBoxIcon.Information;
                    ConnectionString.ApplyChanges();
                }
                else
                {
                    tMessage = "Connection string was not saved successfuly";
                    tMessageCaption = "Failure";
                    tIcon = MessageBoxIcon.Error;
                }

                MessageBox.Show(tMessage, tMessageCaption, tMessageButtons, tIcon);

                LandingForm tLandingForm = (LandingForm)Owner;
                tLandingForm.LoadUpdateForm();
                Close();
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        private bool CheckFormFields()
        {
            ArrayList tControlNames = new ArrayList();

            try
            {
                foreach (Control tSettingsInputField in Controls["connectionContainer"].Controls)
                {
                    if (tSettingsInputField.Enabled && string.IsNullOrEmpty(tSettingsInputField.Text))
                        tControlNames.Add(tSettingsInputField.Tag);
                }

                if (tControlNames.Count != 0)
                {
                    StringBuilder tMessageString = new StringBuilder();
                    string tMessageCaption = "Error";
                    MessageBoxButtons tMessageButtons = MessageBoxButtons.OK;
                    MessageBoxIcon tIcon = MessageBoxIcon.Error;

                    tMessageString.AppendLine("There are empty values.. please fill them.\n");
                    tMessageString.AppendLine("Empty values:-\n");

                    foreach (string tControlName in tControlNames)
                    {
                        tMessageString.AppendLine("- " + tControlName);
                    }

                    MessageBox.Show(tMessageString.ToString(), tMessageCaption, tMessageButtons, tIcon);
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }

            return tControlNames.Count == 0;
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string tMessage = "Are you sure you want to exit?";
                string tMessageCaption = "Exit";
                MessageBoxButtons tMessageButtons = MessageBoxButtons.YesNo;
                MessageBoxIcon tIcon = MessageBoxIcon.Warning;
                DialogResult tResult;

                tResult = MessageBox.Show(tMessage, tMessageCaption, tMessageButtons, tIcon);

                if (tResult == DialogResult.Yes)
                {
                    Close();
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        private void input_IntegratedSecurity_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                CheckIntegratedSecurityValue();
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }
    }
}
