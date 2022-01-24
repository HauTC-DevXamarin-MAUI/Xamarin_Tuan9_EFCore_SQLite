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
    public class CategoryPageViewModel : ViewModelBase
    {
        private DatabaseContext _dbContext = new DatabaseContext();
        private ICategoryService _categoryService;
        public ObservableCollection<Category> CategoryList { get; set; } = new ObservableCollection<Category>();

        #region Property
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

        #endregion

        #region HandleCommand
        private DelegateCommand _onAddCategoryCommand;
        public DelegateCommand OnAddCategoryCommand =>
            _onAddCategoryCommand ?? (_onAddCategoryCommand = new DelegateCommand(async () => await HandleOnAddCategoryCommand()));
        
        private DelegateCommand<Category> _bindingDataOnEntryCommand;
        public DelegateCommand<Category> BindingDataOnEntryCommand =>
            _bindingDataOnEntryCommand ?? (_bindingDataOnEntryCommand = new DelegateCommand<Category>((category) => HandleBindingDataOnEntryCommand(category)));

        private DelegateCommand _onDeleteCategoryCommand;
        public DelegateCommand OnDeleteCategoryCommand =>
            _onDeleteCategoryCommand ?? (_onDeleteCategoryCommand = new DelegateCommand(async () => await HandelOnDeleteCategoryCommand()));

        private DelegateCommand _onUpdateCategoryCommand;
        public DelegateCommand OnUpdateCategoryCommand =>
            _onUpdateCategoryCommand ?? (_onUpdateCategoryCommand = new DelegateCommand(async () => await HandleOnUpdateCategoryCommand()));

        private string MessUpdate = "Do you sure update item?";
        private async Task HandleOnUpdateCategoryCommand()
        {
            var result = await PageDialogService.DisplayAlertAsync("Notification!", MessUpdate, "OK", "No!");
            if (result == true)
            {
                await _categoryService.UpdateCategoryAsync(new Category(int.Parse(Id), Name));
                await LoadData();
            }
        }

        private string MessDelete = "Do you sure delete item?";
        private string MessErrorDelete = "Không thể xóa vì có Item tồn tại trong Category này!";
        private async Task HandelOnDeleteCategoryCommand()
        {

            var result = await PageDialogService.DisplayAlertAsync("Notification!", MessDelete, "OK", "No!");
            if (result == true)
            {
                var result1 = _dbContext.Items.Where(t => t.IdCategory == int.Parse(Id)).FirstOrDefault();
                if(result1 == null)
                {
                    await _categoryService.DeleteCategoryAsync(int.Parse(Id));
                    await LoadData();
                }
                else
                {
                    await PageDialogService.DisplayAlertAsync("Notification!", MessErrorDelete, "Cancel");
                }
            }
        }

        private void HandleBindingDataOnEntryCommand(Category category)
        {
            Id = category.Id.ToString();
            Name = category.Name;
        }

        private string MessNameNull = "Không được bỏ trống Name";
        private async Task HandleOnAddCategoryCommand()
        {
            //private DatabaseContext _dbContext = new DatabaseContext();
            if (Name == null)
            {
                await PageDialogService.DisplayAlertAsync("Thông Báo!", MessNameNull, "OK");
                return;
            }
            else { 
                await _categoryService.AddCategoryAsync(new Category(Name));
                await LoadData();
            }
        }
        #endregion

        #region Constructor
        public CategoryPageViewModel(INavigationService navigationService,
                                     ICategoryService categoryService,
                                     IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            _categoryService = categoryService;
            Title = "Category Page";
        }
        #endregion
        async Task LoadData()
        {
            CategoryList.Clear();
            var result = await _categoryService.GetCategoryAsync();
            foreach (var category in result)
            {
                CategoryList.Add(category);
            }
        }
        public async override void OnAppearing()
        {
            await LoadData();
        }
    }
}
