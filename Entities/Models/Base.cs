using Entities.Extension;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.ComponentModel.DataAnnotations.Schema;



namespace Entities.Models
{

    public class Base : IEntity
    {


        public bool IdValido()
        {
            Guid guiValido;
           return Guid.TryParse(Id.ToString(), out guiValido);

        }

        [Column("id")]
        public Guid Id { get ; set; }
        [NotMapped]
        public bool Valid { get; private set; }

        [NotMapped]
        public virtual bool Invalid { get; }

        public bool IsEmptyObject()
        {
            return IEntityExtensions.IsEmptyObject(this);
        }
        public bool IsObjectNull()
        {
            return IEntityExtensions.IsObjectNull(this);
        }


        [NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }


        public string GetErros
        {
            get
            {
                var erros = new System.Text.StringBuilder();
                foreach (var item in ValidationResult.Errors)
                {
                    erros.Append(item);
                }

                return erros.ToString();
            }
        }
    }
}
