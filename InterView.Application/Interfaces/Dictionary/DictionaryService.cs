using InterView.Application.Models;
using InterView.Application.Services;
using InterView.DataBase.BaseRepository;
using InterView.DataBase.UnitOfWork;
using InterView.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Application.Interfaces.Dictionary
{
    public class DictionaryService : BaseService<InterView.Domain.Entities.Dictionaries>, IDictionaryRepository
    {
        private readonly IBaseRepository<InterView.Domain.Entities.Dictionaries> _dictionariesRepository;

        public DictionaryService(IMemoryUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IBaseRepository<Domain.Entities.Dictionaries> dictionariesRepository) : base(unitOfWork, httpContextAccessor)
        {
            _dictionariesRepository = dictionariesRepository;
        }

        public async Task<ApiResponse> CreateOrUpdateDictianary(DictionariesDto dictionariesDto)
        {
            try
            {
                var message = string.Empty;
                if (dictionariesDto.Id != Guid.Empty && dictionariesDto.Id != null)
                {
                    var dictionary = _dictionariesRepository.Queryable.Where(x => x.Id == dictionariesDto.Id).FirstOrDefault();
                    if (dictionary != null)
                    {
                        dictionary.Status = (Domain.Enum.StatusEnum)dictionariesDto.Status;
                        dictionary.Title = dictionariesDto.Name;
                        dictionary.Description = dictionariesDto.Description;
                        dictionary.ParentId = dictionariesDto.Parentid;
                        dictionary.EditDate = DateTime.UtcNow;
                        await _dictionariesRepository.UpdatePartialAsync(dictionary);
                        message = "Element uğurla update edildi!";

                    }
                }
                else
                {
                    var entityDictionary = new InterView.Domain.Entities.Dictionaries
                    {
                        Status = Domain.Enum.StatusEnum.Active,
                        CreateDate = DateTime.UtcNow,
                        Title = dictionariesDto.Name,
                        Description = dictionariesDto.Description,
                        ParentId = dictionariesDto.Parentid,
                    };
                    await _dictionariesRepository.AddAsync(entityDictionary);
                    message = "Yeni element uğurla əlavə edildi!";
                }
                await _unitOfWork.SaveChangesAsync();
                return new SuccessfulResponse(new ReturnApiDto
                {
                    isSuccess = true,
                    Message = message
                });
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetListDictionary()
        {
            try
            {
                var data = _dictionariesRepository.Queryable.Where(x => x.Status != Domain.Enum.StatusEnum.Deleted).Select(z => new DictionariesDto
                {
                    Id = z.Id,
                    Parentid = z.ParentId,
                    ParentName = z.ParentType.Title,
                    Name = z.Title,
                    Description = z.Description,
                    Code = z.ParentType.Code,
                }).ToList();
                return new SuccessfulResponse(data);
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);

            }
        }
    }
}
