using EFCore_SQLite.Models;
using EFCore_SQLite.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore_SQLite.ViewModels
{
    public class ItemPageViewModel : ViewModelBase
    {
        private IItemService _itemService;

        private string _id;
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _imageAvatar;
        public string ImageAvatar
        {
            get { return _imageAvatar; }
            set { SetProperty(ref _imageAvatar, value); }
        }

        public ObservableCollection<Item> ItemList { get; set; } = new ObservableCollection<Item>();

        private DelegateCommand _onAddItemCommand;
        public DelegateCommand OnAddItemCommand =>
            _onAddItemCommand ?? (_onAddItemCommand = new DelegateCommand(async () => await HandleAddItem()));

        private DelegateCommand _onDeleteItemCommand;
        public DelegateCommand OnDeleteItemCommand =>
            _onDeleteItemCommand ?? (_onDeleteItemCommand = new DelegateCommand(async () => await HandleDeleteItem()));

        private DelegateCommand _onUpdateCommand;
        public DelegateCommand OnUpdateCommand =>
            _onUpdateCommand ?? (_onUpdateCommand = new DelegateCommand(async () => await HandleOnUpdateCommand()));

        private DelegateCommand<Item> _bindingDataOnEntryCommand;
        public DelegateCommand<Item> BindingDataOnEntryCommand =>
            _bindingDataOnEntryCommand ?? (_bindingDataOnEntryCommand = new DelegateCommand<Item>((item) => HandleBindingDataOnEntryCommand(item)));

        private void HandleBindingDataOnEntryCommand(Item item)
        {
            Id = item.Id.ToString();
            Name = item.Name;
            ImageAvatar = item.Image;
            Description = item.Description;
        }

        private async Task HandleAddItem()
        {
            //await _itemService.AddStudentAsync(new Item(int.Parse(Id), Name, Image, Description));
            await _itemService.AddItemAsync(new Item(Name, ImageAvatar, Description));
            await LoadData();
        }

        private string MessDelete = "Do you sure delete item?";
        private async Task HandleDeleteItem()
        {
            var result = await PageDialogService.DisplayAlertAsync("Notification!", MessDelete, "OK", "No!");
            if (result == true)
            {
                await _itemService.DeleteItemAsync(int.Parse(Id));
                await LoadData();
            }
        }

        private string MessUpdate = "Do you sure update item?";
        private async Task HandleOnUpdateCommand()
        {
            var result = await PageDialogService.DisplayAlertAsync("Notification!", MessUpdate, "OK", "No!");
            if (result == true)
            {
                await _itemService.UpdateItemAsync(new Item(int.Parse(Id) ,Name, ImageAvatar, Description));
                await LoadData(); 
            }
        }

        public ItemPageViewModel(INavigationService navigationService, 
                                 IItemService itemService, 
                                 IPageDialogService pageDialogService) 
            :base(navigationService, pageDialogService)
        {
            _itemService = itemService;
            Title = "Item Page";
        }
        async Task LoadData()
        {
            ItemList.Clear();
            var result = await _itemService.GetItemAsync();
            foreach (var item in result)
            {
                ItemList.Add(item);
            }
        }

        public async override void OnAppearing()
        {
            await LoadData();
        }
    }
}
