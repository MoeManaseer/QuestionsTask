using System;
using System.Windows.Forms;
using System.Reflection;
using QuestionDatabase;
using LoggerUtils;
using ResultCodes;

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

        private void UpdateSettingFields()
        {
            try
            {
                Type tConnectionStringType = typeof(ConnectionString);
                PropertyInfo[] tConnectionStringProporties = tConnectionStringType.GetProperties();
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

        private void FillConnectionStringFields()
        {
            try
            {
                Type tConnectionStringType = typeof(ConnectionString);
                PropertyInfo[] tConnectionStringProporties = tConnectionStringType.GetProperties();
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

        private void testBtn_Click(object sender, EventArgs e)
        {
            try
            {
                FillConnectionStringFields();
                int tResponseCode = DatabaseController.TestDatabaseConnection(ConnectionString);
                string tResultMessage = "";

                switch ((ResultCodesEnum) tResponseCode)
                {
                    case ResultCodesEnum.SUCCESS:
                        tResultMessage = "Connection success";
                        break;
                    case ResultCodesEnum.DATABASE_AUTHENTICATION_FAILUER:
                        tResultMessage = "User not authenticated";
                        break;
                    case ResultCodesEnum.DATABASE_CONNECTION_DENIED:
                        tResultMessage = "Connection to database denied";
                        break;
                    case ResultCodesEnum.DATABASE_CONNECTION_FAILURE:
                        tResultMessage = "Connectin to database failed";
                        break;
                    case ResultCodesEnum.SERVER_CONNECTION_FAILURE:
                        tResultMessage = "Connection to server failed";
                        break;
                    default:
                        tResultMessage = "Connection failure";
                        break;
                }

                connectionTestOutput.Text = tResultMessage;
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
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
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
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

                if (tResult == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
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
