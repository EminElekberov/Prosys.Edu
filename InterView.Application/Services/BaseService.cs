using InterView.DataBase.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Application.Services
{
    public class BaseService<T>
    {
        protected readonly IMemoryUnitOfWork _unitOfWork;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public BaseService(IMemoryUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        //public BaseService(IMemoryUnitOfWork unitOfWork)
        //{
        //    this.unitOfWork = unitOfWork;
        //}
        protected Guid? GetUserId()
        {
            if (_httpContextAccessor.HttpContext == null || !_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                return null;
            }
            var individualId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "external_id");

            if (individualId != null)
                return Guid.Parse(individualId.Value);

            return null;
        }

        protected long? GetTenantId()
        {
            if (_httpContextAccessor.HttpContext == null || !_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                return null;
            }
            var tenantId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "tenant_id");

            if (tenantId != null)
                return long.Parse(tenantId.Value);

            return null;
        }
    }

}
