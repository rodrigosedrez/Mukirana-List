using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Muquirana.Models;
using Muquirana.Services;
using System.Globalization;

namespace Muquirana.Views.Popups;

public partial class ProdutosAdd : Popup
{
    private IServiceRepository _repository;
    string func;
    string _titleList;
    int _listId;
    string value;
    double _Quant;
    int idProdu;
    string decimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
    public ProdutosAdd(IServiceRepository repository)
    {
        InitializeComponent();
        this._repository = repository;

        WeakReferenceMessenger.Default.Register<string[]>(this, (e, msg) =>
        {
            _titleList = msg[0].ToString();
            func = msg[1].ToString();
            value = msg[2].ToString();
            _listId = int.Parse(msg[3].ToString());

            if (msg[1] == "QtdUpdate")
            {
                NewProduct.IsVisible = false;
                NewQtd.IsVisible = true;
                ProductEntry.IsVisible = false;
                ValueEntry.IsVisible = false;
                BorderSize.WidthRequest = 200;
                QtdEntry.Placeholder = value;
                buttonsAdd.HorizontalOptions = LayoutOptions.Center;
                idProdu = _listId;
            }
            if (msg[1] == "NewValue")
            {
                NewProduct.IsVisible = false;
                NewValue.IsVisible = true;
                ProductEntry.IsVisible = false;
                ValueEntry.IsVisible = true;
                QtdEntry.IsVisible = false;
                BorderSize.WidthRequest = 200;
                ValueEntry.Placeholder = value;
                buttonsAdd.HorizontalOptions = LayoutOptions.Center;
                idProdu = _listId;
            }
        });

    }


    public void AddNewProduct()
    {
        if (string.IsNullOrEmpty(ProductEntry.Text) || string.IsNullOrWhiteSpace(ProductEntry.Text))
        {
            return;
        }
        else
        {

            if (string.IsNullOrEmpty(ValueEntry.Text) || string.IsNullOrWhiteSpace(ValueEntry.Text))
            {
                ValueEntry.Text = "0";
            }
            if (string.IsNullOrEmpty(QtdEntry.Text) || string.IsNullOrWhiteSpace(QtdEntry.Text))
            {
                QtdEntry.Text = "1";
            }
            if (decimalSeparator == ",")
            {
                ValueEntry.Text = ValueEntry.Text.Replace('.', ',');
            }
            if (decimalSeparator == ",")
            {
                QtdEntry.Text = QtdEntry.Text.Replace('.', ',');
            }
            Products products = new Products
            {
                IdList = _listId,
                PListName =  _titleList,
                Quant = double.Parse(QtdEntry.Text),
                ProductName = ProductEntry.Text,
                UserValue = double.Parse(ValueEntry.Text),
                IsVisibleList = true,
                UserQtdPrice = double.Parse(QtdEntry.Text)*double.Parse(ValueEntry.Text),
            };

            _repository.AddProduto(products);
            WeakReferenceMessenger.Default.Send<string>(string.Empty);

        }
    }

    public void QtdUpdate()
    {
        string decimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;

        if (string.IsNullOrEmpty(QtdEntry.Text) || string.IsNullOrWhiteSpace(QtdEntry.Text))
        {
            return;
        }
        else
        {
            if (decimalSeparator == ",")
            {
                QtdEntry.Text = QtdEntry.Text.Replace('.', ',');
            }
            var qtdUp = _repository.GetProduct().Where(x => x.PListName == _titleList).Where(x => x.ProductId == idProdu).LastOrDefault();

            _Quant = double.Parse(QtdEntry.Text);
            qtdUp.Quant = _Quant;
            qtdUp.UserQtdPrice = _Quant * qtdUp.UserValue;

            _repository.UpdateProduct(qtdUp);

        }
    }

    public void ValueUpdate()
    {

        if (string.IsNullOrEmpty(ValueEntry.Text) || string.IsNullOrWhiteSpace(ValueEntry.Text))
        {
            return;
        }
        else
        {
            if (decimalSeparator == ",")
            {
                ValueEntry.Text = ValueEntry.Text.Replace('.', ',');
            }
            var qtdUp = _repository.GetProduct().Where(x => x.PListName == _titleList).Where(x => x.ProductId == idProdu).LastOrDefault();
            //_quant is the value  pegadinha do malandro
            _Quant = double.Parse(ValueEntry.Text);
            qtdUp.UserValue = _Quant;
            qtdUp.UserQtdPrice = qtdUp.Quant* _Quant;

            _repository.UpdateProduct(qtdUp);
        }
    }

    private void Button_Clicked_Cancel(object sender, EventArgs e)
    {
        this.Close(true);
    }

    private void Button_Clicked_Ok(object sender, EventArgs e)
    {

        switch (func)
        {
            case "QtdUpdate": QtdUpdate(); break;

            case "Novo": AddNewProduct(); break;

            case "NewValue": ValueUpdate(); break;
        }
        WeakReferenceMessenger.Default.Send<string>(string.Empty);

        this.Close(true);
    }
}