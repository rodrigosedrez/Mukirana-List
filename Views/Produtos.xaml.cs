using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using LiteDB;
using Muquirana.Models;
using Muquirana.Services;
using Muquirana.Views.Popups;

namespace Muquirana.Views;

public partial class Produtos : ContentPage
{
    private ListModels _listModels;

    private IServiceRepository _repository;
    private string _title;
    private string _titleId;

    public Produtos(IServiceRepository iservice)
    {

        InitializeComponent();
        _repository = iservice;
        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
        {
            Reload();
        });
    }

    protected override void OnAppearing()
    {

        base.OnAppearing();
        Reload();
    }

    public void NomeLista(ListModels listModels)
    {
        try
        {
            _listModels = listModels;

            NomeVindo.Text = _listModels.ListName;
            _title = _listModels.ListName.ToString();
            _titleId = _listModels.Id.ToString();
        }
        catch
        {
            return;
        }

    }


    public void Reload()
    {    // code executed aways on the page

        try
        {
            var itens = _repository.GetProduct().Where(x => x.PListName == _title).OrderByDescending(x => x.ProductId).ToList();
            var hidem = _repository.GetProduct().Where(x => x.PListName == _title).Where(x => x.IsVisibleList == true).OrderByDescending(x => x.ProductId).ToList();
            if (glyphEye.Glyph == "visibility_off")
            {
                collectionViewListas.ItemsSource = hidem;
            }
            else
            {
                collectionViewListas.ItemsSource = itens;
            }
            SomaExpandRest();
        }
        catch
        {
            return;
        }

    }
    public void ValueFromList()
    {
        try
        {
            var botao = _repository.GetProduct().Where(x => x.PListName == _title).ToList();
        }
        catch
        {
            return;
        }
    }

    private void ButtonAdd_Clicked(object sender, EventArgs e)
    {
        try
        {
            this.ShowPopup(new ProdutosAdd(_repository));
            string[] teste = { _title, "Novo", "", _titleId };
            WeakReferenceMessenger.Default.Send<string[]>(teste);
        }
        catch
        {
            return;
        }



    }


    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            var checkbox = sender as CheckBox;
            var dataObj = checkbox.BindingContext as Products;

            var produto = _repository.GetProduct().Where(x => x.PListName == _title).Where(x => x.ProductId == dataObj.ProductId).First();


            if (glyphEye.Glyph == "visibility_off" && e.Value == true)
            {
                produto.IsVisibleList = false;
                _repository.UpdateProduct(produto);
                Reload();
            }
            produto.Ccheck = e.Value;
            _repository.UpdateProduct(produto);
        }
        catch
        {
            return;
        }

    }

    private void SwipeItem_Invoked_Delete(object sender, EventArgs e)
    {
        try
        {
            var swiped = (SwipeItem)sender;
            Products products = (Products)swiped.CommandParameter;
            _repository.DeleteProduct(products);

            Reload();
        }
        catch
        {
            return;
        }

    }

    private void Button_Clicked_Value_Change(object sender, EventArgs e)
    {
        try
        {
            var change = (Button)sender;
            Products products = (Products)change.CommandParameter;

            this.ShowPopup(new ProdutosAdd(_repository));

            string[] teste = { _title, "NewValue", products.UserValue.ToString(), products.ProductId.ToString() };

            WeakReferenceMessenger.Default.Send<string[]>(teste);
        }
        catch
        {
            return;
        }


    }

    private void Button_Clicked_Quant_Update(object sender, EventArgs e)
    {
        try
        {
            var change = (Button)sender;
            Products products = (Products)change.CommandParameter;

            this.ShowPopup(new ProdutosAdd(_repository));

            string[] teste = { _title, "QtdUpdate", products.Quant.ToString(), products.ProductId.ToString() };

            WeakReferenceMessenger.Default.Send<string[]>(teste);
        }
        catch
        {
            return;
        }


    }

    private void HidenEye_Clicked_BlueEye(object sender, EventArgs e)
    {
        try
        {
            var busca = _repository.GetProduct().Where(x => x.PListName == _title).Where(x => x.Ccheck == true).ToList();
            switch (glyphEye.Glyph)
            {
                case "visibility":
                    foreach (var item in busca)
                    {
                        item.IsVisibleList = false; _repository.UpdateProduct(item);
                    };
                    glyphEye.Glyph = "visibility_off";
                    Reload();
                    break;

                case "visibility_off":
                    foreach (var item in busca)
                    {
                        item.IsVisibleList = true; _repository.UpdateProduct(item);
                    };
                    glyphEye.Glyph = "visibility";
                    Reload();
                    break;
            }
        }
        catch
        {
            return;
        }

    }

    private void Button_Clicked_MidleButto(object sender, EventArgs e)
    {
        try
        {
            var chenge = (Button)sender;
            var toexpend = _repository.GetListModels().Where(x => x.ListName == _title).Where(x => x.ToExpand >= 0);
            var toexpendRegist = _repository.GetListModels().Where(x => x.ListName == _title).Where(x => x.ToExpand >= 0).LastOrDefault();
            switch (chenge.Text)
            {
                case "Soma":

                    chenge.Text = "A Gastar";
                    ValorDaSoma.IsVisible = false;
                    chenge.BackgroundColor = Colors.LightGreen;
                    chenge.FontAttributes = FontAttributes.None;
                    EntryToExpand.Placeholder = string.Empty;

                    if (ValueToExpend.Text == "0")
                    {
                        ValueToExpendButton.IsVisible = true;
                        EntryToExpand.IsVisible = true;
                        ValueToExpend.IsVisible = false;
                        EntryToExpand.IsEnabled = true;
                        EntryToExpand.Placeholder = "$ para gastar";

                    }
                    else
                    {
                        EntryToExpand.IsVisible = false;
                        ValueToExpend.IsVisible = true;
                        glyphMoney.Glyph = "edit";
                        ValueToExpendButton.IsVisible = true;

                    }
                    break;

                case "A Gastar":
                    chenge.Text = "Restante";
                    chenge.BackgroundColor = Colors.Purple;
                    ValueToExpendButton.IsVisible = false;
                    EntryToExpand.IsVisible = false;
                    ValueToExpend.IsVisible = false;
                    RestValue.IsVisible = true;
                    chenge.TextColor = Colors.White;
                    break;

                case "Restante":
                    RestValue.IsVisible = false;
                    chenge.Text = "Soma";
                    chenge.FontAttributes = FontAttributes.Bold;
                    chenge.BackgroundColor = Colors.Yellow;
                    ValorDaSoma.IsVisible = true;
                    chenge.TextColor = Colors.Black;

                    break;
            }
        }
        catch
        {
            return;
        }

    }



    public void SomaExpandRest()
    {
        try
        {
            var usuarioValor = _repository.GetProduct().Where(x => x.PListName == _title).Where(x => x.UserQtdPrice > -9999).ToArray();

            var toexpend = _repository.GetListModels().Where(x => x.ListName == _title).Where(x => x.ToExpand >= 0).LastOrDefault();
            var containsExpand = _repository.GetListModels().Where(x => x.ListName == _title).Where(x => x.ToExpand >= 0);

            var soma = usuarioValor.Sum(x => x.UserQtdPrice);
            double aGastar;

            if (containsExpand.Count() == 0)
            {
                aGastar = 0;
            }
            else
            {
                aGastar =  toexpend.ToExpand;
            }


            var rest = aGastar - (usuarioValor.Sum(x => x.UserQtdPrice));



            ValueToExpend.Text = aGastar.ToString();


            if (rest.ToString().Length > 5)
            {
                RestValue.Text = rest.ToString("F2");
            }
            else
            {
                RestValue.Text = rest.ToString();
            }


            if (soma.ToString().Length > 5)
            {
                ValorDaSoma.Text = soma.ToString("F2");
            }
            else
            {
                ValorDaSoma.Text = soma.ToString();
            }
        }
        catch
        {
            return;
        }



    }

    private void ValueToExpendButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var toexpend = _repository.GetListModels()
                    .Where(x => x.ListName == _title)
                    .Where(x => x.ToExpand >= 0)
                    .LastOrDefault();

            EntryToExpand.Placeholder = string.Empty;
            switch (glyphMoney.Glyph)
            {
                case "attach_money":

                    if (string.IsNullOrEmpty(EntryToExpand.Text) || string.IsNullOrWhiteSpace(EntryToExpand.Text))
                    {
                        toexpend.ToExpand = 0;
                    }
                    else
                    {
                        toexpend.ToExpand = double.Parse(EntryToExpand.Text);
                    }

                    _repository.Update(toexpend);
                    Reload();
                    ValueToExpend.IsVisible = true;
                    EntryToExpand.IsVisible = false;
                    EntryToExpand.IsEnabled = false;
                    EntryToExpand.Text = string.Empty;
                    glyphMoney.Glyph = "edit";
                    break;

                case "edit":
                    ValueToExpend.IsVisible = false;
                    EntryToExpand.IsVisible = true;
                    EntryToExpand.IsEnabled = true;
                    glyphMoney.Glyph = "check";
                    break;

                case "check":

                    if (string.IsNullOrEmpty(EntryToExpand.Text) || string.IsNullOrWhiteSpace(EntryToExpand.Text))
                    {
                        toexpend.ToExpand = 0;
                    }
                    else
                    {
                        toexpend.ToExpand = double.Parse(EntryToExpand.Text);
                    }
                    _repository.Update(toexpend);
                    Reload();
                    ValueToExpend.IsVisible = true;
                    glyphMoney.Glyph = "edit";
                    ValueToExpendButton.IsVisible = true;
                    EntryToExpand.IsVisible = false;
                    EntryToExpand.IsEnabled = false;

                    break;
            }
        }
        catch
        {
            return;
        }
    }

    private void Button_Clicked_Help(object sender, EventArgs e)
    {
        try
        {
            this.ShowPopup(new ProductHelp());
        }
        catch
        {
            return;
        }
    }

}