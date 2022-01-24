using EFCore_SQLite.Helpers;
using EFCore_SQLite.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore_SQLite.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region HandleCommand
        private DelegateCommand _navigationItemPageCommand;
        public DelegateCommand NavigationItemPageCommand =>
            _navigationItemPageCommand ?? (_navigationItemPageCommand = new DelegateCommand(HandleNavigationItemPageCommand));

        private DelegateCommand _navigationCategoryPageCommand;
        public DelegateCommand NavigationCategoryPageCommand =>
            _navigationCategoryPageCommand ?? (_navigationCategoryPageCommand = new DelegateCommand(HandleNavigationCategoryPageCommand));
        void HandleNavigationItemPageCommand()
        {
            NavigationService.NavigateAsync("ItemPage");
        }

        void HandleNavigationCategoryPageCommand()
        {
            NavigationService.NavigateAsync("CategoryPage");
        }

        #region Handle Backup and Retore

        private DelegateCommand _backupCommand;
        public DelegateCommand BackupCommand =>
            _backupCommand ?? (_backupCommand = new DelegateCommand(async () => await ExcuteBackUp()));

        private DelegateCommand _restoreCommand;
        public DelegateCommand RestoreCommand =>
            _restoreCommand ?? (_restoreCommand = new DelegateCommand(async () => await ExecuteRestore()));


        private string backupSuccess = "Sao lưu thành công";
        private string backupError = "Sao lưu thất bại";
        private async Task ExcuteBackUp()
        {
            try
            {
                File.Copy(Constant.DBPath, Constant.DBPathBackup, true);
                Console.WriteLine(Constant.DBPathBackup);
                await PageDialogService.DisplayAlertAsync("Thông báo", backupSuccess, "Đóng");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await PageDialogService.DisplayAlertAsync("Thông báo", backupError, "Đóng");
            }
        }

        private string retoreSuccess = "Khôi phục thành công";
        private string retoreError = "Bạn chưa thất bại";
        private async Task ExecuteRestore()
        {
            if (File.Exists(Constant.DBPathBackup))
            {
                if (File.Exists(Constant.DBPath))
                {
                    File.Delete(Constant.DBPath);
                }
                File.Copy(Constant.DBPathBackup, Constant.DBPath);
                await PageDialogService.DisplayAlertAsync("Thông báo", retoreSuccess, "Đóng");
            }
            else
            {
                await PageDialogService.DisplayAlertAsync("Thông báo", retoreError, "Đóng");
            }
        }
        #endregion

        #endregion

        #region Constructor
        public MainPageViewModel(INavigationService navigationService,
                                 IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            Title = "Main Page";
        }
        #endregion
    }
}
