using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.Application.Tools.ValidationAttributes
{

    public class ValidIdAttribute : ValidationAttribute
    {

        /// <summary>
        /// Check if the ID provided is less than 0
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            // Lógica de validación personalizada
            // Aquí puedes realizar tus validaciones personalizadas y retornar ValidationResult.Success o ValidationResult con un mensaje de error si la validación falla


            if ((int)value! < 0)
            {
                return new ValidationResult("Negatives Id is not allowed");
            }

            // Si la validación es exitosa, retorna ValidationResult.Success
            return ValidationResult.Success!;
        }


    }
}
