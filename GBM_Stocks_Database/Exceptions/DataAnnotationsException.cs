using GBM_Stocks_Database.Domain;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace GBM_Stocks_Database.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class DataAnnotationsException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="errors"></param>
        public DataAnnotationsException(List<ValidationResult> errors)
        {
            Errors = JsonSerializer.Deserialize<List<Error>>(JsonSerializer.Serialize(errors));
        }

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public DataAnnotationsException(string message)
            : base(message)
        {
            MissingValidationModel = true;
        }

        /// <summary>
        /// </summary>
        public List<Error>? Errors { get; set; }

        /// <summary>
        /// </summary>
        public bool? MissingValidationModel { get; set; }
    }
}