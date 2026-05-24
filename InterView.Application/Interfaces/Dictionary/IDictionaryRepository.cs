using InterView.Application.Models;
using InterView.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Application.Interfaces.Dictionary
{
    public interface IDictionaryRepository
    {
        Task<ApiResponse> GetListDictionary();
        Task<ApiResponse> CreateOrUpdateDictianary(DictionariesDto dictionariesDto);
    }
}
