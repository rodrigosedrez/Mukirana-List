using Muquirana.Models;

namespace Muquirana.Services
{
    public interface IServiceRepository
    {
        void Add(ListModels listModels);

        void AddProduto(Products products);
        void Delete(ListModels listModels);

        void DeleteProduct(Products products);

        List<ListModels> GetListModels();
        List<Products> GetProduct();
        void Update(ListModels listModels);

        void UpdateProduct(Products products);
    }
}