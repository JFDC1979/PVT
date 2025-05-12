
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PVTv1.Models
{
    public class TablerosPageViewModel
    {
        public IEnumerable<TableroViewModel> Tableros { get; set; }
        public string? SearchTerm { get; set; }

        public TablerosPageViewModel()
        {
            Tableros = Enumerable.Empty<TableroViewModel>();
           
        }

        public TablerosPageViewModel(IEnumerable<TableroViewModel> tableros, string? searchTerm)
        {
            Tableros = tableros ?? Enumerable.Empty<TableroViewModel>(); 
            SearchTerm = searchTerm;
        }
    }
}