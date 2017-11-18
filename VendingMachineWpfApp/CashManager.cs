using System;
using System.IO;
using System.Collections.Generic;

namespace VendingMachineWpfApp
{
    [Serializable]
    public class CashManager
    {
        private const string _fileName = "CashManager.bin";

        // Store counters for all coins and bills here
        private List<Coin> _coinCollection = new List<Coin>();
        private List<Bill> _billCollection = new List<Bill>();

		public void BillAccepted(int billValue)
        {
            // Increase counter for the corresponding bill value
            _billCollection.Add(new Bill(billValue, 1));
        }

        public void CoinAccepted(int coinValue)
		{
            // Increase counter for the corresponding coin value
            _coinCollection.Add(new Coin(coinValue, 1));
		}

		public int[] GiveChange(int credit)
        {
            // Simple or advanced algorithm for giving the change

            List<int> coins = new List<int>();
            foreach (var denomination in new [] {10,5,2,1} )
            {
                for (int i = 0; i < credit; i++)
                {
                    if (credit - denomination < 0) continue;
                    coins.Add(denomination);
                    credit = credit - denomination;
                }
            }

            return coins.ToArray();
        }

		// + Save-restore methods
		public void SaveState()
		{
			VendingMachineStateHandler.Save<CashManager>(_fileName, this);
		}

		public static CashManager RestoreState()
		{
            if (File.Exists(_fileName))
                return VendingMachineStateHandler.Restore<CashManager>(_fileName);

            CashManager cashManager = new CashManager();
            return cashManager;
		}
    }
}
