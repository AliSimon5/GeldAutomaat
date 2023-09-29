using M.NetStandard.Encryption;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;
using GeldAutomaat.Data;
using GeldAutomaat.Logic;
using M.Core.Application.WPF.MessageBox;
using Google.Protobuf.Collections;
using M.Core.Application.ControlHelpers;
using GeldAutomaat.Types;

namespace GeldAutomaat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string strEncryptedAccountNumber = string.Empty;
        public static string strAccountNumber = string.Empty;
        public string strEncryptedPinCode = string.Empty;
        public string strEmployeeUserName = string.Empty;
        public string strEncryptedEmployeePassword = string.Empty;

        static ListBoxControl<Transactions> _lbAllTransactions;

        public MainWindow()
        {
            InitializeComponent();
            _lbAllTransactions = new ListBoxControl<Transactions>(lbAllTransactions);
            StartUp();
        }

        public void StartUp()
        {
            tbPinCode.IsReadOnly = true;
            gMainWindow.Visibility = Visibility.Visible;
            gPinKeyboard.Visibility = Visibility.Visible;
            gEmployeeLoginWindow.Visibility = Visibility.Hidden;
            gLoggedInWindow.Visibility = Visibility.Hidden;
            gCreateAccount.Visibility = Visibility.Hidden;
            gEmployeeWindow.Visibility = Visibility.Hidden;
            gEditPincode.Visibility = Visibility.Hidden;
            gEditBalance.Visibility = Visibility.Hidden;
            gShowBalance.Visibility = Visibility.Hidden;
            gWithdrawMoney.Visibility = Visibility.Hidden;
            gDepositMoney.Visibility = Visibility.Hidden;
            gShowTransactions.Visibility = Visibility.Hidden;
            tbCreatePincode.MaxLength = 4;
            tbCreateAccountNumber.MaxLength = 18;
        }

        #region Numpad
        public bool CheckIfMaxChar()
        {
            if (tbPinCode.Text.Length == 4)
                return true;
            else return false;
        }
        private void btnNumOne_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfMaxChar())
                return;
            string strOldText = tbPinCode.Text;
            string strNewText = strOldText + "1";
            tbPinCode.Text = strNewText;
        }
        private void btnNumTwo_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfMaxChar())
                return;
            string strOldText = tbPinCode.Text;
            string strNewText = strOldText + "2";
            tbPinCode.Text = strNewText;

        }
        private void btnNumThree_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfMaxChar())
                return;
            string strOldText = tbPinCode.Text;
            string strNewText = strOldText + "3";
            tbPinCode.Text = strNewText;
        }
        private void btnNumFour_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfMaxChar())
                return;
            string strOldText = tbPinCode.Text;
            string strNewText = strOldText + "4";
            tbPinCode.Text = strNewText;
        }
        private void btnNumFive_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfMaxChar())
                return;
            string strOldText = tbPinCode.Text;
            string strNewText = strOldText + "5";
            tbPinCode.Text = strNewText;
        }
        private void btnNumSix_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfMaxChar())
                return;
            string strOldText = tbPinCode.Text;
            string strNewText = strOldText + "6";
            tbPinCode.Text = strNewText;
        }
        private void btnNumSeven_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfMaxChar())
                return;
            string strOldText = tbPinCode.Text;
            string strNewText = strOldText + "7";
            tbPinCode.Text = strNewText;
        }
        private void btnNumEight_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfMaxChar())
                return;
            string strOldText = tbPinCode.Text;
            string strNewText = strOldText + "8";
            tbPinCode.Text = strNewText;
        }
        private void btnNumNine_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfMaxChar())
                return;
            string strOldText = tbPinCode.Text;
            string strNewText = strOldText + "9";
            tbPinCode.Text = strNewText;
        }
        private void btnNumZero_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfMaxChar())
                return;
            string strOldText = tbPinCode.Text;
            string strNewText = strOldText + "0";
            tbPinCode.Text = strNewText;
        }
        private void btnNumCorr_Click(object sender, RoutedEventArgs e)
        {
            if (tbPinCode.Text.Length == 0) return;
            string strOldText = tbPinCode.Text;
            string strNewText = strOldText.Remove(strOldText.Length - 1, 1);
            tbPinCode.Text = strNewText;
        }
        private void btnNumOk_Click(object sender, RoutedEventArgs e)
        {
            if (gMainWindow.Visibility == Visibility.Visible)
            {
                if (tbPinCode.Text.Length != 4)
                {
                    MBox.ShowWarning("Pincode is 4 cijfers");
                    return;
                }
                if (tbAccountNumber.Text.Length == 0)
                {
                    MBox.ShowWarning("Rekening nummer is leeg");
                    return;
                }

                strEncryptedAccountNumber = LogicManager.EncryptString(tbAccountNumber.Text.Replace(" ", ""));
                strEncryptedPinCode = LogicManager.EncryptString(tbPinCode.Text);

                if (!DataManager.CheckIfAccountExistsWithPin(strEncryptedAccountNumber, strEncryptedPinCode))
                {
                    MBox.ShowWarning("Gegeven Gegevens kloppen niet");
                    tbAccountNumber.Text = string.Empty;
                    tbPinCode.Text = string.Empty;
                    return;
                }
                else
                {
                    strAccountNumber = tbAccountNumber.Text;
                    gMainWindow.Visibility = Visibility.Hidden;
                    gPinKeyboard.Visibility = Visibility.Hidden;
                    gLoggedInWindow.Visibility = Visibility.Visible;
                    tbPinCode.Text = string.Empty;
                    tbAccountNumber.Text = string.Empty;
                }
            }
            else if (gEditPincode.Visibility == Visibility.Visible)
            {
                if (tbPinCode.Text.Length != 4)
                {
                    MBox.ShowWarning("Pincode is 4 cijfers");
                    return;
                }
                if (MBox.ShowQuestion($"Weet u zeker dat u de pincode wilt wijzigen naar {tbPinCode.Text}?"))
                {
                    strEncryptedPinCode = LogicManager.EncryptString(tbPinCode.Text);
                    DataManager.EditExistingPincode(strEncryptedAccountNumber, strEncryptedPinCode);
                    MBox.ShowInformation("Pincode is aangepast.");
                    gEditPincode.Visibility = Visibility.Hidden;
                    gEmployeeWindow.Visibility = Visibility.Visible;
                    gPinKeyboard.Visibility = Visibility.Hidden;
                    tbPinCode.Text = string.Empty;
                }
                else
                {
                    tbPinCode.Text = string.Empty;
                    return;
                }
            }
        }
        #endregion

        #region BackButtons
        private void btnCreateAccountBack_Click(object sender, RoutedEventArgs e)
        {
            gCreateAccount.Visibility = Visibility.Hidden;
            gEmployeeWindow.Visibility = Visibility.Visible;
            tbCreateAccountNumber.Text = string.Empty;
            tbCreatePincode.Text = string.Empty;
            tbCreateSaldo.Text = string.Empty;
        }
        private void btnEditBalanceBack_Click(object sender, RoutedEventArgs e)
        {
            gEditBalance.Visibility = Visibility.Hidden;
            gEmployeeWindow.Visibility = Visibility.Visible;
            tbEditSaldo.Text = string.Empty;
        }
        private void btnLoginEmployeeBack_Click(object sender, RoutedEventArgs e)
        {
            gEmployeeLoginWindow.Visibility = Visibility.Hidden;
            gMainWindow.Visibility = Visibility.Visible;
            gPinKeyboard.Visibility = Visibility.Visible;
            tbEmployeeUserName.Text = string.Empty;
            tbEmployeePassword.Password = string.Empty;
        }
        private void btnEditPincodeBack_Click(object sender, RoutedEventArgs e)
        {
            gEditPincode.Visibility = Visibility.Hidden;
            gPinKeyboard.Visibility = Visibility.Hidden;
            gEmployeeWindow.Visibility = Visibility.Visible;
            tbPinCode.Text = string.Empty;
        }

        private void btnShowBalanceBack_Click(object sender, RoutedEventArgs e)
        {
            gShowBalance.Visibility = Visibility.Hidden;
            gLoggedInWindow.Visibility = Visibility.Visible;
        }
        private void btnWithdrawMoneyBack_Click(object sender, RoutedEventArgs e)
        {
            gWithdrawMoney.Visibility = Visibility.Hidden;
            gLoggedInWindow.Visibility = Visibility.Visible;
        }
        private void btnDepositMoneyBack_Click(object sender, RoutedEventArgs e)
        {
            gDepositMoney.Visibility = Visibility.Hidden;
            gLoggedInWindow.Visibility = Visibility.Visible;
        }
        private void btnShowTransactionsBack_Click(object sender, RoutedEventArgs e)
        {
            gShowTransactions.Visibility = Visibility.Hidden;
            gLoggedInWindow.Visibility = Visibility.Visible;
        }
        #endregion

        #region Employee
        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            gMainWindow.Visibility = Visibility.Hidden;
            gPinKeyboard.Visibility = Visibility.Hidden;
            gEmployeeLoginWindow.Visibility = Visibility.Visible;
            tbAccountNumber.Text = string.Empty;
            tbPinCode.Text = string.Empty;
        }
        private void btnEmployeeLogin_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmployeeUserName.Text.Length == 0)
            {
                MBox.ShowWarning("Gebruikersnaam is leeg");
                return;
            }
            if (tbEmployeePassword.Password.Length == 0)
            {
                MBox.ShowWarning("Wachtwoord is leeg");
                return;
            }

            strEmployeeUserName = tbEmployeeUserName.Text;
            strEncryptedEmployeePassword = LogicManager.EncryptString(tbEmployeePassword.Password);


            if (!DataManager.CheckIfEmployeeExists(strEmployeeUserName, strEncryptedEmployeePassword))
            {
                MBox.ShowWarning("Gegeven Gegevens kloppen niet");
                tbEmployeeUserName.Text = string.Empty;
                tbEmployeePassword.Password = string.Empty;
                return;
            }
            else
            {
                gEmployeeLoginWindow.Visibility = Visibility.Hidden;
                gEmployeeWindow.Visibility = Visibility.Visible;
            }
        }
        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            gEmployeeWindow.Visibility = Visibility.Hidden;
            gCreateAccount.Visibility = Visibility.Visible;

        }
        private void btnSubmitCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (tbCreateAccountNumber.Text.Length == 0)
            {
                MBox.ShowWarning("Rekening nummer is leeg");
                return;
            }
            if (tbCreateAccountNumber.Text.Length < 18)
            {
                MBox.ShowWarning("Rekening nummer is niet lang genoeg");
                return;
            }
            if (tbCreatePincode.Text.Length == 0)
            {
                MBox.ShowWarning("Pincode is leeg");
                return;
            }
            if (tbCreatePincode.Text.Length < 4)
            {
                MBox.ShowWarning("Pincode is niet lang genoeg");
                return;
            }

            string strNewAccountNumber = LogicManager.EncryptString(tbCreateAccountNumber.Text.Replace(" ", ""));
            string strNewPinCode = LogicManager.EncryptString(tbCreatePincode.Text);
            string strNewSaldo = "0";

            if (tbCreateSaldo.Text != string.Empty)
                strNewSaldo = tbCreateSaldo.Text;

            if (!DataManager.AddNewAccount(strNewAccountNumber, strNewPinCode, strNewSaldo))
            {
                MBox.ShowWarning("Ingevoerde rekening bestaat al.");
                tbCreateAccountNumber.Text = string.Empty;
                return;
            }
            else
            {
                MBox.ShowInformation("Nieuwe rekening toevoegen is gelukt.");
                gCreateAccount.Visibility = Visibility.Hidden;
                gEmployeeWindow.Visibility = Visibility.Visible;
                return;
            }
        }
        public bool CheckIfAccountExists()
        {
            strEncryptedAccountNumber = LogicManager.EncryptString(tbEmployeeAccountNumber.Text.Replace(" ", ""));
            if (!DataManager.CheckIfAccountExists(strEncryptedAccountNumber))
            {
                MBox.ShowWarning("Rekening nummer bestaat niet.");
                strEncryptedAccountNumber = string.Empty;
                return false;
            }
            else return true;
        }
        private void btnEditPincode_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIfAccountExists())
                return;
            gEmployeeWindow.Visibility = Visibility.Hidden;
            gEditPincode.Visibility = Visibility.Visible;
            gPinKeyboard.Visibility = Visibility.Visible;
        }
        private void btnEditBalance_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIfAccountExists())
                return;
            gEmployeeWindow.Visibility = Visibility.Hidden;
            gEditBalance.Visibility = Visibility.Visible;
            string strSaldo = DataManager.GetSaldoFromAccount(strEncryptedAccountNumber);
            tblBalance.Text = $"Huidige Saldo: €{strSaldo}";
        }
        private void btnConfirmEditBalance_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbEditSaldo.Text, out int InsertedAmount))
            {

                if (MBox.ShowQuestion($"Weet u zeker dat u de saldo wilt wijzigen naar €{tbEditSaldo.Text}?"))
                {
                    DataManager.EditExistingSaldo(strEncryptedAccountNumber, int.Parse(tbEditSaldo.Text));
                    string IdOfEmployee = DataManager.GetIDOfEmployee(strEmployeeUserName);
                    DataManager.AddTransaction(strEncryptedAccountNumber, 0, InsertedAmount, IdOfEmployee);
                    MBox.ShowInformation("Saldo is aangepast.");
                    gEditBalance.Visibility = Visibility.Hidden;
                    gEmployeeWindow.Visibility = Visibility.Visible;
                    tbEditSaldo.Text = string.Empty;
                }
                else
                {
                    tbEditSaldo.Text = string.Empty;
                    return;
                }
            }
            else
            {
                MBox.ShowWarning("Kan geen text of komma getallen editen.");
            }
        }
        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIfAccountExists())
                return;
            if (MBox.ShowQuestion($"Weet u zeker dat u de rekening permanent wilt verwijderen?"))
            {
                if (MBox.ShowQuestion($"Als u op YES drukt dan wordt de rekening COMPLEET verwijderd uit het systeem!\nWeet u zeker dat u wilt doorgaan?"))
                {
                    DataManager.DeleteExistingAccount(strEncryptedAccountNumber);
                    MBox.ShowInformation("Rekening is verwijderd.");
                    tbEmployeeAccountNumber.Text = string.Empty;
                }
            }
        }
        #endregion

        private void btnShowBalance_Click(object sender, RoutedEventArgs e)
        {
            gLoggedInWindow.Visibility = Visibility.Hidden;
            gShowBalance.Visibility = Visibility.Visible;
            string strSaldo = DataManager.GetSaldoFromAccount(strEncryptedAccountNumber);
            lblCurrentBalance.Content = $"Huidige Saldo: €{strSaldo}";
        }
        private void btnWithdrawMoney_Click(object sender, RoutedEventArgs e)
        {
            gLoggedInWindow.Visibility = Visibility.Hidden;
            gWithdrawMoney.Visibility = Visibility.Visible;
            int CountOfWithdrawalsToday = DataManager.GetTransactionsOfToday(strEncryptedAccountNumber);
            tblCountOfWithdrawals.Text = $"Vandaag heeft u {CountOfWithdrawalsToday}/3 keer geld opgenomen.";
            if (CountOfWithdrawalsToday == 3)
            {
                tbWithdrawMoney.IsEnabled = false;
                btnWitdrawInsertedAmount.IsEnabled = false;
            }
        }
        private void btnWitdrawInsertedAmount_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbWithdrawMoney.Text, out int GivenAmount))
            {
                if (GivenAmount > 500)
                {
                    MBox.ShowWarning("Bedrag mag niet hoger zijn dan €500.");
                    tbWithdrawMoney.Text = string.Empty;
                    return;
                }
                if (GivenAmount < 10)
                {
                    MBox.ShowWarning("Bedrag mag niet kleiner zijn dan €10.");
                    tbWithdrawMoney.Text = string.Empty;
                    return;
                }

                string CurrentAmountString = DataManager.GetSaldoFromAccount(strEncryptedAccountNumber);
                int CurrentAmount = int.Parse(CurrentAmountString);
                if (CurrentAmount < GivenAmount)
                {
                    MBox.ShowWarning($"U kunt maximaal {CurrentAmount} opnemen.");
                    tbWithdrawMoney.Text = string.Empty;
                    return;
                }

                int EditedAmount = CurrentAmount - GivenAmount;
                DataManager.EditExistingSaldo(strEncryptedAccountNumber, EditedAmount);
                DataManager.AddTransaction(strEncryptedAccountNumber, 1, GivenAmount);
                MBox.ShowInformation($"€{GivenAmount} is opgenomen.");
                tbWithdrawMoney.Text = string.Empty;
                btnWithdrawMoney_Click(null, null);
            }
            else
            {
                MBox.ShowWarning("Kan niet text of komma getallen opnemen.");
                tbWithdrawMoney.Text = string.Empty;
                return;
            }
        }
        private void btnDepositMoney_Click(object sender, RoutedEventArgs e)
        {
            gLoggedInWindow.Visibility = Visibility.Hidden;
            gDepositMoney.Visibility = Visibility.Visible;
        }
        private void btnDepositInsertedAmount_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbDepositMoney.Text, out int GivenAmount))
            {
                if (GivenAmount > 2000)
                {
                    MBox.ShowWarning("Bedrag mag niet hoger zijn dan €2000.");
                    tbDepositMoney.Text = string.Empty;
                    return;
                }
                if (GivenAmount < 10)
                {
                    MBox.ShowWarning("Bedrag mag niet kleiner zijn dan €10.");
                    tbDepositMoney.Text = string.Empty;
                    return;
                }

                string CurrentAmountString = DataManager.GetSaldoFromAccount(strEncryptedAccountNumber);
                int CurrentAmount = int.Parse(CurrentAmountString);


                int EditedAmount = CurrentAmount + (GivenAmount - 2);
                DataManager.EditExistingSaldo(strEncryptedAccountNumber, EditedAmount);
                DataManager.AddTransaction(strEncryptedAccountNumber, 0, GivenAmount);
                MBox.ShowInformation($"€{GivenAmount} is gestort.");
                tbDepositMoney.Text = string.Empty;
            }
            else
            {
                MBox.ShowWarning("Kan niet text of komma getallen opnemen.");
                tbDepositMoney.Text = string.Empty;
                return;
            }
        }
        private void btnShowTransactions_Click(object sender, RoutedEventArgs e)
        {
            gLoggedInWindow.Visibility = Visibility.Hidden;
            gShowTransactions.Visibility = Visibility.Visible;
            // pakt alleen de id van alle transacties
            // zet de string om in class
            List<Transactions> listOfTransactions = DataManager.GetAllTransactions(strEncryptedAccountNumber);
            List<Transactions> listOfLatestTransactions = Enumerable.Reverse(listOfTransactions).Take(3).ToList();
            _lbAllTransactions.SetItemsSource(listOfLatestTransactions);
        }
        private void btnShowAllTransactions_Click(object sender, RoutedEventArgs e)
        {
            List<Transactions> listOfTransactions = DataManager.GetAllTransactions(strEncryptedAccountNumber);
            _lbAllTransactions.SetItemsSource(Enumerable.Reverse(listOfTransactions).ToList());
        }

    }
}
