using LdelossantosN5.Domain.Patterns;
using System.Runtime.CompilerServices;

namespace LdelossantosN5.Domain.UseCases
{
    public static class ErrorDictionary
    {
        public const string K_RecordNotFound = "RecordNotFound";
        public const string K_GenericException = "InternalError";
        public static void NotFoundError(this UseCaseResultModel modelResult, NotFoundException ex)
        {
            modelResult.AddError(K_RecordNotFound, ex.Message);
        }
        public static void GenericException(this UseCaseResultModel modelResult, String clientReference)
        {
            modelResult.AddError(K_GenericException, clientReference);
        }
        public static Boolean IsNotFound(this UseCaseResultModel modelResult)
        {
            return modelResult.Errors.Any(x => x.ErrCode == K_RecordNotFound);
        }
        public static Boolean IsException(this UseCaseResultModel modelResult)
        {
            return modelResult.Errors.Any(x => x.ErrCode == K_GenericException);
        }
    }
}
