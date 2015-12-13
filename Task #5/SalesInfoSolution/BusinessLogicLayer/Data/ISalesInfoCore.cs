using BusinessLogicLayer.Models.SaleModels;
using BusinessLogicLayer.Models.UserRoleModels;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Data
{
    interface ISalesInfoCore
    {
        ICollection<User> Users { get; }
        ICollection<Role> Roles { get; }
        void AddUser(string name, Role role);
        void ChangeUserRole(User user, Role newRole);
        void DeleteUser(User user);

        IEnumerable<Sale> GetSales(DateTime dateFrom, DateTime dateTo);
        void ChangeSale(Sale sale, Manager newManger, Client newClient, Product newProduct);
        void DeleteSale(Sale sale);
    }
}
