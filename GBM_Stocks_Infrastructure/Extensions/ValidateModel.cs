using GBM_Stocks_Database.Domain;
using GBM_Stocks_Database.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace GBM_Stocks_Infrastructure.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ValidateModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <exception cref="DataAnnotationsException"></exception>
        public static Task<bool> IsValid<T>(this T entity)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(entity);
            var isValid = Validator.TryValidateObject(entity, ctx, validationResults, true);

            if (isValid)
                return Task.FromResult(true);

            var errors = (from validationResult in validationResults
                          let error = new Error
                          {
                              ErrorMessage = validationResult.ErrorMessage,
                              MemberNames = validationResult.MemberNames.ToList()
                          }
                          select error).ToList();

            throw new DataAnnotationsException(validationResults);
        }
    }
}