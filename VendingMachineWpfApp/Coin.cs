using System;

namespace VendingMachineWpfApp
{
    [Serializable]
    public class Coin : IMoney
    {
        private int _denomination;
        private int _count;

        public Coin(int denomination, int count)
        {
            _denomination = denomination;
            _count = count;
        }

        public int Denomination
        {
            get { return _denomination; }
            set { _denomination = value; }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
    }
}

