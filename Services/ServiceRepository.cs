
using LiteDB;
using Muquirana.Models;

namespace Muquirana.Services
{
    // i decided use especific function to each class in ListModels.cs
    public class ServiceRepository : IServiceRepository
    {
        private readonly LiteDatabase _liteDatabase;
        private readonly string collectionName = "listModel";
        private readonly string secondCollecitonNme = "products";
        private readonly string CalcuName = "calcupadoras";
        private readonly string Calcuvalor = "calcuregisters";

        public ServiceRepository(LiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public void Add(ListModels listModels)
        {
            var col = _liteDatabase.GetCollection<ListModels>(collectionName);
            col.Insert(listModels);
            col.EnsureIndex(a => a.Id);
        }

        public void Update(ListModels listModels)
        {
            var col = _liteDatabase.GetCollection<ListModels>(collectionName);
            col.Update(listModels);
        }

        public void Delete(ListModels listModels)
        {
            var col = _liteDatabase.GetCollection<ListModels>(collectionName);
            col.Delete(listModels.Id);
        }

        public List<ListModels> GetListModels()
        {
            return _liteDatabase
                  .GetCollection<ListModels>(collectionName)
                  .Query()
                  .OrderBy(x => x.Id)
                  .ToList();
        }

        public List<Products> GetProduct()
        {
            return _liteDatabase
                  .GetCollection<Products>(secondCollecitonNme)
                  .Query()
                  .OrderBy(x => x.ProductId)
                  .ToList();
        }
        public void AddProduto(Products products)
        {
            var col = _liteDatabase.GetCollection<Products>(secondCollecitonNme);
            col.Insert(products);
            col.EnsureIndex(a => a.ProductId);
        }

        public void UpdateProduct(Products products)
        {
            var col = _liteDatabase.GetCollection<Products>(secondCollecitonNme);
            col.Update(products);
        }

        public void DeleteProduct(Products products)
        {
            var col = _liteDatabase.GetCollection<Products>(secondCollecitonNme);
            col.Delete(products.ProductId);
        }


    }

}