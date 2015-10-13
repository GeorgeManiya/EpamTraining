using SweetGift.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetGift.Data
{
    class GiftComponentComparerByWrapperType : IComparer<IGiftComponent>
    {
        public int Compare(IGiftComponent x, IGiftComponent y)
        {
            if (x != null && y != null)
            {
                if (x.Wrapper != null && y.Wrapper != null)
                {
                    if (x.Wrapper.WrapperType > y.Wrapper.WrapperType)
                    {
                        return 1;
                    }
                    else
                    {
                        return (x.Wrapper.WrapperType == y.Wrapper.WrapperType) ? 0 : -1;
                    }
                }
                else
                {
                    return (x.Wrapper == null && y.Wrapper == null) ? 0 : (x.Wrapper != null) ? 1 : -1;
                }
            }
            else
            {
                return (y == null && x == null) ? 0 : (x != null) ? 1 : -1;
            }
        }
    }
}
