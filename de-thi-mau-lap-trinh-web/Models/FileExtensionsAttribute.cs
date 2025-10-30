using System.ComponentModel.DataAnnotations;

namespace de_thi_mau_lap_trinh_web.Models
{
    public class FileExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public FileExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Tên file ảnh phải có đuôi: {string.Join(", ", _extensions)}";
        }
    }
}
