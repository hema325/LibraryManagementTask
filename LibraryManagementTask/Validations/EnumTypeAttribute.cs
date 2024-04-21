using System.ComponentModel.DataAnnotations;

namespace LibraryManagementTask.Validations
{
    public class EnumTypeAttribute: ValidationAttribute
    {
        private readonly Type _type;

        public EnumTypeAttribute(Type type)
        {
            _type = type;
        }

        public override bool IsValid(object? value)
        {
            if(value == null) 
                return false;

            return Enum.IsDefined(_type, value);
        }
    }
}
