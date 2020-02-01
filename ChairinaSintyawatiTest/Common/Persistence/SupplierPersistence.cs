using Common.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.ViewModels;
using Data.Context;
using System.Data.Entity;

namespace Common.Persistence
{
    public class SupplierPersistence : ISupplierPersistence
    {
        MyContext myContext = new MyContext();
        bool status = false;
        public bool Delete(int id)
        {
            var get = myContext.Suppliers.Find(id);
            get.Delete();
            myContext.Entry(get).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
                status = true;
            return status;
        }

        public List<Supplier> Get()
        {
            var get = myContext.Suppliers.Where(x => x.IsDelete == false).ToList();
            return get;
        }

        public Supplier Get(int id)
        {
            var get = myContext.Suppliers.Find(id);
            return get;
        }

        public bool Insert(SupplierVM supplierVM)
        {
            var push = new Supplier(supplierVM);
            myContext.Suppliers.Add(push);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return status = true;
            }
            else
            {
                return status = false;
            }
        }

        public bool Update(int id, SupplierVM supplierVM)
        {
            var get = myContext.Suppliers.Find(id);
            get.Update(supplierVM);
            myContext.Entry(get).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
                status = true;
            return status;
        }
    }
}
