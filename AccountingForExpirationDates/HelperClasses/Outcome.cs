namespace AccountingForExpirationDates.HelperClasses
{
    public class Outcome<T1, T2>
    {
        public T1 status;
        public T2 data;

        public Outcome(T1 _status, T2 _data)
        {
            status = _status;
            data = _data;
        }
    }
}
