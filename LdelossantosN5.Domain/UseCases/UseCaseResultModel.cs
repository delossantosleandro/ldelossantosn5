using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.UseCases
{
    public class UseCaseResultModel
    {
        public bool Success { get { return this.Errors.Count == 0; } set { return; } }
        public List<UseCaseErrorModel> Errors { get; set; } = [];

        public object? ResponseData { get; set; }
        public void AddError(string errorCode, string errorMessage)
        {
            this.Errors.Add(new UseCaseErrorModel() { ErrCode = errorCode, Description = errorMessage });
        }
    }
}
