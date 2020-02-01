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
    public class ItemPersistence : IItemPersistence
    {
        MyContext myContext = new MyContext();
        bool status = false;
        public bool Delete(int id)
        {
            var get = myContext.Items.Find(id);
            get.Delete();
            myContext.Entry(get).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
                status = true;
            return status;
        }

        public List<Item> Get()
        {
            var get = myContext.Items.Where(x => x.IsDelete == false).ToList();
            return get;
        }

        public Item Get(int id)
        {
            var get = myContext.Items.Find(id);
            return get;
        }

        public bool Insert(ItemVM itemVM)
        {
            var getSupplier = myContext.Suppliers.Find(itemVM.Suppliers_Id);
            var push = new Item(itemVM);
            push.Suppliers = getSupplier;
            myContext.Items.Add(push);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return status = true;
            }
           return status;

        }

        public bool Update(int id, ItemVM itemVM)
        {
            var getSupplier = myContext.Suppliers.Find(itemVM.Suppliers_Id);
            var get = myContext.Items.Find(id);

            get.Suppliers = getSupplier;
            get.Update(itemVM);
            myContext.Entry(get).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
                status = true;
            return status;
        }
    }
}
