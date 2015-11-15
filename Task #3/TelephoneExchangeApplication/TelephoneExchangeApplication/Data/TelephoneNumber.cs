using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneExchangeApplication.Data
{
    public struct TelephoneNumber : IEquatable<TelephoneNumber>
    {
        public TelephoneNumber(ushort code, uint number)
        {
            if (code > 99)
                throw new ArgumentException("Number cann not contains more then 2 numerics");
            if (number > 999999)
                throw new ArgumentException("Number cann not contains more then 6 numerics");

            Code = code;
            Number = number;
        }

        public ushort Code { get; private set; }
        public uint Number { get; private set; }

        public override string ToString()
        {
            return string.Format("{0:00}-{1:000000}", Code, Number);
        }

        public override bool Equals(object obj)
        {
            if (obj is TelephoneNumber)
            {
                return Code == ((TelephoneNumber)obj).Code && Number == ((TelephoneNumber)obj).Number;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode() | Number.GetHashCode();
        }

        public bool Equals(TelephoneNumber other)
        {
            return Code == other.Code && Number == other.Number;
        }

        public static bool operator ==(TelephoneNumber p1, TelephoneNumber p2)
        {
            return (p1 as IEquatable<TelephoneNumber>).Equals(p2);
        }

        public static bool operator !=(TelephoneNumber p1, TelephoneNumber p2)
        {
            return !(p1 == p2);
        }
    }
}
