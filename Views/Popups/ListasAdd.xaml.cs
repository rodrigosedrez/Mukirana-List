using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Muquirana.Models;
using Muquirana.Services;

namespace Muquirana.Views.Popups;

public partial class ListasAdd : Popup
{
    private IServiceRepository _serviceRepository;
    public ListasAdd(IServiceRepository serviceRepository)
    {
        InitializeComponent();
        this._serviceRepository = serviceRepository;
        OnApearing();
    }

    public void OnApearing()
    {
        var numero = _serviceRepository.GetListModels().Count+1;
        nomeEntry.Placeholder = "Lista - "+numero;
    }

    public void AddNewList()
    {
        string newName;

        if (nomeEntry.Text == string.Empty || nomeEntry.Text == null)
        {
            newName = nomeEntry.Placeholder;
        }
        else
        {
            newName = nomeEntry.Text;
        }

        ListModels listModels = new ListModels
        {
            ListName = newName,
            DateTime = DateTime.Now,
            Ccheck = false,
        };

        _serviceRepository.Add(listModels);

        WeakReferenceMessenger.Default.Send<string>(newName);


    }

    private void Button_Clicked_Cancel(object sender, EventArgs e)
    {
        this.Close(true);
    }

    private void Button_Clicked_Ok(object sender, EventArgs e)
    {
        AddNewList();
        this.Close(true);
    }
}