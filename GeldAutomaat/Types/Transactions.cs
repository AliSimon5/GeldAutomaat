using GeldAutomaat.Data;
using M.NetStandard.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeldAutomaat.Types
{
    public class Transactions : BoxItem
    {
        public string DepositWithdraw { get; set; }

        public string Time { get; set; }
        public string Amount { get; set; }
        public string Employee { get; set; }

        public string GetBoxItemTitle()
        {
            string User = MainWindow.strAccountNumber;
            string DepositOrWithdraw = string.Empty;
            if (DepositWithdraw == "0")
                DepositOrWithdraw = "Opgenomen";
            if (DepositWithdraw == "1")
                DepositOrWithdraw = "Gestort";
            if (Employee != null)
                User = DataManager.GetUsernameOfEmployeeID(Employee);

            return $"Gedaan door: {User}\n€{Amount} {DepositOrWithdraw}\n{Time}";
        }
    }
}
