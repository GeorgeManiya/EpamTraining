using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesDataAccess.Models
{
    public class Manager : IHasIdentifications
    {
        public Manager(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; private set; }

        public string Name { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
