    public struct TelephoneNumber
    {
        public int Code { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Code, Number);
        }
    }
        public TelephoneNumber Number { get; set; }

