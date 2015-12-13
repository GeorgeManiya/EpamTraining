using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Models.SaleModels;
using BusinessLogicLayer.Models.UserRoleModels;
using DataAccessLayer.Data;

namespace BusinessLogicLayer.Data
{
    public class SalesInfoCore : ISalesInfoCore
    {
        private SalesDataProvider _salesDataProvider;
        private UserRolesDataProvider _userRolesDataProvider;

        public SalesInfoCore()
        {
            _salesDataProvider = new SalesDataProvider();
            _userRolesDataProvider = new UserRolesDataProvider();

            Roles = _userRolesDataProvider.GetRoles().Select(r => RoleModelToObject(r)).ToList();
            Users = _userRolesDataProvider.GetUsers().Select(u => UserModelToObject(u)).ToList();
        }


        #region ISalesInfoCore implementation

        public ICollection<Role> Roles { get; private set; }

        public ICollection<User> Users { get; private set; }

        public void AddUser(string name, Role role)
        {
            var modelRole = RoleObjectToModel(role);
            var modelUser = _userRolesDataProvider.AddNewUser(name, modelRole);

            var user = new User(modelUser.Id, name, role);
            Users.Add(user);
        }

        public void ChangeUserRole(User user, Role newRole)
        {
            var modelUser = UserObjectToModel(user);
            var modelRole = RoleObjectToModel(newRole);

            _userRolesDataProvider.ChangeUserRole(modelUser, modelRole);
            user.SetRole(newRole);
        }

        public void DeleteUser(User user)
        {
            var modelUser = UserObjectToModel(user);
            _userRolesDataProvider.DeleteUser(modelUser);
        }


        public void ChangeSale(Sale sale, Manager newManger, Client newClient, Product newProduct)
        {
            var modelSale = SaleObjectToModel(sale);
            var newModelManager = ManagerObjectToModel(newManger);
            var newModelClient = ClientObjectToModel(newClient);
            var newModelProduct = ProductObjectToModel(newProduct);

            _salesDataProvider.ChangeSale(modelSale, newModelManager, newModelClient, newModelProduct);
            sale.SetManger(newManger);
            sale.SetClient(newClient);
            sale.SetProduct(newProduct);
        }

        public void DeleteSale(Sale sale)
        {
            var modelSale = SaleObjectToModel(sale);
            _salesDataProvider.DeleteSale(modelSale);
        }

        public IEnumerable<Sale> GetSales(DateTime dateFrom, DateTime dateTo)
        {
            return _salesDataProvider.GetSales(dateFrom, dateTo).Select(s => SaleModelToObject(s));
        }

        #endregion


        #region Models to object

        private Sale SaleModelToObject(DataAccessLayer.Models.SaleModels.Sale sale)
        {
            var objectManager = sale.Manager != null
                ? ManagerModelToObject(sale.Manager)
                : null;

            var objectClient = sale.Client != null
                ? ClientModelToObject(sale.Client)
                : null;

            var objectProduct = sale.Product != null
                ? ProductModelToObject(sale.Product)
                : null;

            return new Sale(sale.Id, objectManager, objectClient, objectProduct, sale.SaleDate);
        }

        private Manager ManagerModelToObject(DataAccessLayer.Models.SaleModels.Manager manager)
        {
            return new Manager(manager.Id, manager.Name);
        }

        private Client ClientModelToObject(DataAccessLayer.Models.SaleModels.Client client)
        {
            return new Client(client.Id, client.Name);
        }

        private Product ProductModelToObject(DataAccessLayer.Models.SaleModels.Product product)
        {
            return new Product(product.Id, product.ProductName, product.ProductCost);
        }


        private User UserModelToObject(DataAccessLayer.Models.UserRoleModels.User user)
        {
            var objectRole = user.Role != null
                ? RoleModelToObject(user.Role)
                : null;

            return new User(user.Id, user.Name, objectRole);
        }

        private Role RoleModelToObject(DataAccessLayer.Models.UserRoleModels.Role role)
        {
            return new Role(role.Id, role.RoleName);
        }

        #endregion


        #region Object to models

        private DataAccessLayer.Models.SaleModels.Sale SaleObjectToModel(Sale sale)
        {
            var modelManager = sale.Manager != null 
                ? ManagerObjectToModel(sale.Manager)
                : null;

            var modelClient = sale.Client != null
                ? ClientObjectToModel(sale.Client)
                : null;

            var modelProduct = sale.Product != null
                ? ProductObjectToModel(sale.Product)
                : null;

            return new DataAccessLayer.Models.SaleModels.Sale(sale.Id, modelManager, modelClient, modelProduct, sale.SaleDate);
        }

        private DataAccessLayer.Models.SaleModels.Manager ManagerObjectToModel(Manager manager)
        {
            return new DataAccessLayer.Models.SaleModels.Manager(manager.Id, manager.Name);
        }

        private DataAccessLayer.Models.SaleModels.Client ClientObjectToModel(Client client)
        {
            return new DataAccessLayer.Models.SaleModels.Client(client.Id, client.Name);
        }

        private DataAccessLayer.Models.SaleModels.Product ProductObjectToModel(Product product)
        {
            return new DataAccessLayer.Models.SaleModels.Product(product.Id, product.ProductName, product.ProductCost);
        }


        private DataAccessLayer.Models.UserRoleModels.User UserObjectToModel(User user)
        {
            var modelRole = user.Role != null
                ? RoleObjectToModel(user.Role)
                : null;

            return new DataAccessLayer.Models.UserRoleModels.User(user.Id, user.Name, modelRole);
        }

        private DataAccessLayer.Models.UserRoleModels.Role RoleObjectToModel(Role role)
        {
            return new DataAccessLayer.Models.UserRoleModels.Role(role.Id, role.RoleName);
        }

        #endregion
    }
}
