using LiteDB;

namespace Muquirana.Models
{
    //Class to name lists
    public class ListModels
    {
        [BsonId]
        public int Id { get; set; }

        [BsonField("ListName")]
        public string ListName { get; set; }

        [BsonField("ToExpand")]
        public double ToExpand { get; set; }

        [BsonField("DateTime")]
        public DateTime DateTime { get; set; }

        [BsonField("Ccheck")]
        public bool Ccheck { get; set; }

        [BsonField("Visible")]
        public bool Visible { get; set; }

    }


    // Class to list products 

    public class Products
    {
        [BsonId]
        public int ProductId { get; set; }

        [BsonField("IdList")]
        public int IdList { get; set; }

        [BsonField("PListName")]
        public string PListName { get; set; }

        [BsonField("ProductName")]
        public string ProductName { get; set; }

        [BsonField("UserValue")]
        public double UserValue { get; set; }

        [BsonField("CalcuValue")]
        public double CalcuValue { get; set; }

        [BsonField("Quant")]
        public double Quant { get; set; }

        [BsonField("UserQtdPrice")]
        public double UserQtdPrice { get; set; }

        [BsonField("Ccheck")]
        public bool Ccheck { get; set; }

        [BsonField("IsVisibleList")]
        public bool IsVisibleList { get; set; }


    }


    // icon calculator class to register this part use (Calculadora and CalcuRegist)



}
