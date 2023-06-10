using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarvelSearch.Core.Models;

namespace MarvelSearch.Core.Services
{
    public interface IMarvelAPIService
    {
        public Task<List<Comic>> GetComics(string marvelTitle);
    }
}
