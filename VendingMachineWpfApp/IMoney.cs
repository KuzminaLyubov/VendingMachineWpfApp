namespace VendingMachineWpfApp
{
    interface IMoney
    {
        int Denomination
        {
            get;
            set;
        }

        int Count
        {
            get;
            set;
        }
    }
}

