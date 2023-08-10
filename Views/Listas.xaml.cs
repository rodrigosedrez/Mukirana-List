using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Muquirana.Models;
using Muquirana.Services;
using Muquirana.Views.Popups;

namespace Muquirana.Views;

public partial class Listas : ContentPage
{
    private IServiceRepository _repository;

    public Listas(IServiceRepository serviceRepository)
    {
        InitializeComponent();
        this._repository = serviceRepository;
        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
        {
            Reload(); // the legacy message from Views/Popups/ListasAdd.xaml.cs  requare reload page after add new list
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            var itens = _repository.GetListModels().OrderByDescending(x => x.Id);
            collectionViewListas.ItemsSource = itens; //DataTemplate 

            foreach (var item in itens) //   soluction to vanish the checkboxfrom screen
            {
                item.Visible = false;
                _repository.Update(item);
            }
        }
        catch
        {
            return;
        }


    }

    private void Reload()
    {
        // function to atualize the list models
        try
        {
            var itens = _repository.GetListModels();
            if (itens.Capacity == 0) { GridListas.IsVisible = false; }
            else { GridListas.IsVisible = true; }
            collectionViewListas.ItemsSource = itens.OrderByDescending(x => x.Id);
        }
        catch
        {
            return;
        }

    }


    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {     //button to open the products page and send the listName
        try
        {
            var grid = (Grid)sender;
            var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];
            ListModels listModel = (ListModels)gesture.CommandParameter;

            var produtos = Handler.MauiContext.Services.GetService<Produtos>();
            produtos.NomeLista(listModel);
            Navigation.PushAsync(produtos);
        }
        catch
        {
            return;
        }

    }

    private void SwipeItem_Invoked(object sender, EventArgs e)
    {
        try
        {
            var swiped = (SwipeItem)sender;
            ListModels listModels = (ListModels)swiped.CommandParameter;
            _repository.Delete(listModels);
            var pro = _repository.GetProduct().Where(x => x.PListName == listModels.ListName);
            foreach (var item in pro)
            {
                _repository.DeleteProduct(item);
            }
            Reload();
        }
        catch
        {
            return;
        }
        //function swipe to delete lists


    }
    private void Button_Clicked_Nav_Add_New(object sender, EventArgs e)
    {
        try
        {
            this.ShowPopup(new ListasAdd(_repository));
        }
        catch
        {
            return;
        }
        //green button to add new list

    }

    private void Button_Clicked_Share(object sender, EventArgs e)
    {
        try
        {

        }
        catch
        {
            return;
        }


        //// button shere off now 
        //var cLists = _repository.GetListModels();
        //switch (shareGlyph.Glyph)
        //{
        //    case "share":
        //        shareGlyph.Glyph = "send";
        //        foreach (var item in cLists)
        //        {
        //            item.Visible = true;
        //            item.Ccheck = false;
        //            _repository.Update(item);
        //        }
        //        Reload();

        //        break;
        //    case "send":
        //        shareGlyph.Glyph = "share";
        //        foreach (var item in cLists)
        //        {
        //            item.Visible = false;
        //            _repository.Update(item);
        //        }
        //        shareList();
        //        Reload();
        //        break;
        //}
    }

    //public async void shareList()
    //{
    //    var ListasCcheck = _repository.GetListModels().Where(x => x.Ccheck == true).ToList();
    //    var produtcsList = _repository.GetProduct().ToList();

    //    string fn = "MukiranaListas.mkdb";
    //    string file = Path.Combine(FileSystem.CacheDirectory, fn);



    //    var listaLinhas = ListasCcheck.Where(x => x.ListName.Count() > 0).ToList().Count() + ListasCcheck.Where(x => x.ToExpand >= 0).ToList().Count();

    //    int productQuant = 0;

    //    string produtos = "";

    //    foreach (var item1 in ListasCcheck)
    //    {
    //        foreach (var item2 in produtcsList)
    //        {
    //            if (item1.ListName == item2.PListName)
    //            {
    //                produtos += item2.PListName.ToString()+"(s)"+"\n"+item2.Quant.ToString()+"\n"+item2.ProductName.ToString()+
    //                    "\n"+item2.UserValue.ToString()+"\n"
    //                    ;
    //                productQuant += 4;
    //            }
    //        }
    //    }

    //    string nomes = $"{listaLinhas}\n{productQuant}\n";
    //    foreach (var item in ListasCcheck)
    //    {
    //        nomes += item.ListName.ToString()+"(s)"+"\n"+item.ToExpand.ToString()+"\n";
    //    }
    //    File.WriteAllText(file, $"{nomes}\n{produtos}");


    //    await Share.Default.RequestAsync(new ShareFileRequest
    //    {
    //        Title = "Share text file do mukirana",
    //        File = new ShareFile(file),


    //    });
    //}

    //private void checkProduto_CheckedChanged(object sender, CheckedChangedEventArgs e)
    //{
    //    var checkbox = sender as CheckBox;
    //    var dataObj = checkbox.BindingContext as ListModels;

    //    var Clista = _repository.GetListModels().Where(x => x.Id == dataObj.Id).First();

    //    Clista.Ccheck = e.Value;
    //    _repository.Update(Clista);





    //}



    private void Button_Clicked_Download(object sender, EventArgs e)
    {
        try
        {

        }
        catch
        {
            return;
        }

        //try
        //{
        //    if (DeviceInfo.Platform != DevicePlatform.Android) return;

        //    var status = PermissionStatus.Unknown;

        //    if (DeviceInfo.Version.Major >= 12)
        //    {
        //        status = await Permissions.CheckStatusAsync<StoragePermission>();

        //        if (status == PermissionStatus.Granted) return;

        //        if (Permissions.ShouldShowRationale<StoragePermission>())
        //        {
        //            await Shell.Current.DisplayAlert("Precisa de permissão", "Para adcionar nova lista", "OK");
        //        }
        //        status = await Permissions.RequestAsync<StoragePermission>();
        //    }
        //    else
        //    {

        //        var receive = await FilePicker.PickAsync(default);
        //        var filePath = receive.FullPath;

        //        //FileInfo f = new FileInfo(filePath.FullPath.ToString());
        //        //f.MoveTo(Path.ChangeExtension(filePath.FullPath, ".txt"));

        //        //var itens = _repository.GetListModels().OrderByDescending(x => x.Id);  volto o metodo com 4 envios e refaco um foreach para a criacao das somas separadamnete


        //        int numbList = int.Parse(File.ReadLines(filePath).Take(1).First());

        //        for (var i = 0; i < numbList; i+=2)
        //        {

        //            ListModels listModels = new ListModels
        //            {
        //                ListName = File.ReadAllLines(filePath).Skip(2+i).Take(1).First(),
        //                ToExpand = double.Parse(File.ReadAllLines(filePath).Skip(3+i).Take(1).First()),
        //                DateTime = DateTime.Now,
        //                Ccheck = false,
        //            };
        //            _repository.Add(listModels);

        //        }

        //        int numbProduct = int.Parse(File.ReadLines(filePath).Skip(1).Take(1).First());

        //        var produtId = _repository.GetProduct().ToList();

        //        for (var i = 0; i < numbProduct; i+=4)
        //        {
        //            Products products = new Products
        //            {
        //                PListName =              File.ReadAllLines(filePath).Skip(3 + numbList + i).Take(1).First(),
        //                Quant =     double.Parse(File.ReadAllLines(filePath).Skip(4 + numbList + i).Take(1).First()),
        //                ProductName =            File.ReadAllLines(filePath).Skip(5 + numbList + i).Take(1).First(),
        //                UserValue = double.Parse(File.ReadAllLines(filePath).Skip(6 + numbList + i).Take(1).First()),
        //                UserQtdPrice =  double.Parse(File.ReadAllLines(filePath).Skip(4 + numbList + i).Take(1).First())*
        //                double.Parse(File.ReadAllLines(filePath).Skip(6 + numbList + i).Take(1).First()),
        //            };
        //            _repository.AddProduto(products);
        //        }


        //    }
        //}
        //catch
        //{
        //    return;
        //}


        //Reload();
    }

    private void Button_Clicked_Help(object sender, EventArgs e)
    {
        try
        {
            this.ShowPopup(new ListasHelp());
        }
        catch
        {
            return;
        }
    }
}



