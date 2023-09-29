using GeldAutomaat.Types;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeldAutomaat.Data
{
    public partial class DataManager
    {
        public static string connstr = "Server=localhost;Uid=root;database=geldautomaat";
        public static bool CheckIfAccountExistsWithPin(string argAccount, string argPincode)
        {
            List<string> listOutcome = new List<string>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM `rekeningen` WHERE `RekeningNummer` = '{argAccount}' AND `Pincode` = '{argPincode}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOutcome.Add(reader.GetString(0));
                        }
                    }
                }
            }
            if (listOutcome.Count == 0)
                return false;
            else return true;
        }
        public static bool CheckIfAccountExists(string argAccount)
        {
            List<string> listOutcome = new List<string>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM `rekeningen` WHERE `RekeningNummer` = '{argAccount}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOutcome.Add(reader.GetString(0));
                        }
                    }
                }
            }
            if (listOutcome.Count == 0)
                return false;
            else return true;
        }
        public static bool CheckIfEmployeeExists(string argUsername, string argPassword)
        {
            List<string> listOutcome = new List<string>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM `medewerkers` WHERE `Gebruikersnaam` = '{argUsername}' AND `Wachtwoord` = '{argPassword}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOutcome.Add(reader.GetString(0));
                        }
                    }
                }
            }
            if (listOutcome.Count == 0)
                return false;
            else return true;
        }
        public static bool AddNewAccount(string argAccount, string argPincode, string argSaldo)
        {
            if (CheckIfAccountExists(argAccount))
                return false;

            List<string> listOutcome = new List<string>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO `rekeningen` (`RekeningNummer`, `Pincode`, `Saldo`) VALUES ('{argAccount}', '{argPincode}', '{argSaldo}')";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOutcome.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return true;
        }
        public static bool AddTransaction(string argAccount, int argWithdrawOrDeposit, int argSaldo, string Medewerkers_idMedewerkers = null)
        {
            List<string> listOutcome = new List<string>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    if (Medewerkers_idMedewerkers == null)
                        cmd.CommandText = $"INSERT INTO `transacties` (`Rekeningen_RekeningNummer`, `Opnemen/Storten`, `Hoeveel`) VALUES ('{argAccount}', '{argWithdrawOrDeposit}', '{argSaldo}')";
                    else
                        cmd.CommandText = $"INSERT INTO `transacties` (`Rekeningen_RekeningNummer`, `Opnemen/Storten`, `Hoeveel`, `Medewerkers_idMedewerkers`) VALUES ('{argAccount}', '{argWithdrawOrDeposit}', '{argSaldo}', '{Medewerkers_idMedewerkers}')";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOutcome.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return true;
        }
        public static void EditExistingPincode(string argAccount, string argPincode)
        {
            List<string> listOutcome = new List<string>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE `rekeningen` SET `Pincode` = '{argPincode}' WHERE `rekeningen`.`RekeningNummer` = '{argAccount}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOutcome.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }
        public static string GetSaldoFromAccount(string argAccount)
        {
            List<string> listOutcome = new List<string>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT `Saldo` FROM `rekeningen` WHERE `RekeningNummer` = '{argAccount}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOutcome.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return listOutcome.First();
        }
        public static string GetIDOfEmployee(string argName)
        {
            List<string> listOutcome = new List<string>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT `idMedewerkers` FROM `medewerkers` WHERE `Gebruikersnaam` = '{argName}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOutcome.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return listOutcome.First();
        }
        public static string GetUsernameOfEmployeeID(string argEmployeeID)
        {
            List<string> listOutcome = new List<string>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT `Gebruikersnaam` FROM `medewerkers` WHERE `idMedewerkers` = '{argEmployeeID}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOutcome.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return listOutcome.First();
        }
        public static void EditExistingSaldo(string argAccount, int argBalance)
        {
            List<string> listOutcome = new List<string>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE `rekeningen` SET `Saldo` = '{argBalance}' WHERE `rekeningen`.`RekeningNummer` = '{argAccount}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOutcome.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }
        public static void DeleteExistingAccount(string argAccount)
        {
            List<string> listOutcome = new List<string>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM `rekeningen` WHERE `RekeningNummer` = '{argAccount}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOutcome.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }
        public static int GetTransactionsOfToday(string argAccount)
        {
            List<string> listOutcome = new List<string>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM `transacties` WHERE `Rekeningen_RekeningNummer` = '{argAccount}' AND DAY(`Tijd`) = {DateTime.Now.Day} AND  `Opnemen/Storten` = 1";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOutcome.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return listOutcome.Count;
        }
        public static List<Transactions> GetAllTransactions(string argAccount)
        {
            List<Transactions> listOutcome = new List<Transactions>();
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM `transacties` WHERE `Rekeningen_RekeningNummer` = '{argAccount}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transactions TransactionInfo = new Transactions();
                            TransactionInfo.DepositWithdraw = reader.GetString(2);
                            TransactionInfo.Time = reader.GetString(3);
                            TransactionInfo.Amount = reader.GetString(4);
                            TransactionInfo.Employee = reader.IsDBNull(5) ? null : reader.GetString(5);
                            listOutcome.Add(TransactionInfo);
                        }
                    }
                }
            }
            return listOutcome;
        }

    }
}
