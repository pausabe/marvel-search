using System;
using System.Threading.Tasks;
using MarvelSearch.Core.Models;

namespace MarvelSearch.Core.ViewModels.Detail
{
    public class DetailViewModel : BaseViewModel<Comic>
    {

        public override void Prepare(Comic selectedComic)
        {
            SelectedComic = selectedComic;
        }

        private Comic _selectedComic;
        public Comic SelectedComic
        {
            get => _selectedComic;
            set => SetProperty(ref _selectedComic, value);
        }
    }
}
