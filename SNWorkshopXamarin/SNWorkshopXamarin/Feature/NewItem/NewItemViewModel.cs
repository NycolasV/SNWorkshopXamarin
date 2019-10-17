using SNWorkshopXamarin.Models;
using SNWorkshopXamarin.Services;
using SNWorkshopXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SNWorkshopXamarin.Feature.NewItem
{
    public class NewItemViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private CepService _cepService;

        public NewItemViewModel()
        {
            Title = "New Item Page";
            _cepService = new CepService();

            SaveItemCommand = new Command(SaveItemAsync);
            SearchCepCommand = new Command(SearchCepAsync);
        }

        Item item = new Item();
        public Item Item
        {
            get { return item; }
            set { SetProperty(ref item, value, "Item"); }
        }

        string cep = "";
        public string Cep
        {
            get { return cep; }
            set { SetProperty(ref cep, value.TrimStart().TrimEnd(), "Cep"); }
        }

        CepResponse cepResponse = new CepResponse();
        public CepResponse CepResponse
        {
            get { return cepResponse; }
            set { SetProperty(ref cepResponse, value, "CepResponse");}
        }

        bool hasCepResponse = false;
        public bool HasCepResponse
        {
            get { return hasCepResponse; }
            set { SetProperty(ref hasCepResponse, value, "HasCepResponse"); }
        }

        public ICommand SaveItemCommand { get; set; }
        public Command SearchCepCommand { get; set; }

        public async void SaveItemAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (string.IsNullOrEmpty(Item.Text))
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Nome do item não pode estar em branco!", "OK");
                IsBusy = false;
                return;
            }

            MessagingCenter.Send(this, "AddItem", Item);
            await App.Current.MainPage.DisplayAlert("Sucesso", "item cadastrado com sucesso!", "OK");
            await App.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;
        }

        public async void SearchCepAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (string.IsNullOrEmpty(Cep) || Cep.Length != 8)
            {
                await App.Current.MainPage.DisplayAlert("Erro", "CEP Inválido!", "OK");
                IsBusy = false;
                return;
            }

            CepResponse = await _cepService.GetCepResponseAsync(Cep);

            if (CepResponse == null || CepResponse.Cep == null)
            {
                await App.Current.MainPage.DisplayAlert("Erro", "CEP Inválido!", "OK");
                IsBusy = false;
                return;
            }

            HasCepResponse = true;
            IsBusy = false;
        }
    }
}
