using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawsVNEN.Library
{
    /// <summary>
    /// Chỉ định loại file của trường dữ liệu hợp lệ
    /// </summary>
    /// <param name="ValidTypes">Các loại file được hỗ trợ, ngăn cách bởi dấu phẩy</param>
    public class FileTypeAttribute : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "Luật Việt Nam hỗ trợ các loại file sau: {0}";
        private IEnumerable<string> ValidTypes { get; set; }

        public FileTypeAttribute(string validTypes)
        {
            ValidTypes = validTypes.Split(',').Select(s => s.Trim().ToLower());
            ErrorMessage = string.Format(DefaultErrorMessage, string.Join(" , ", ValidTypes));
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            if (file != null)
            {
                //foreach (HttpPostedFileBase file in files)
                //{
                    if (!ValidTypes.Any(e => file.FileName.EndsWith(e)))
                    {
                        return new ValidationResult(ErrorMessageString);
                    }
                //}
            }
            return ValidationResult.Success;
        }

        //TODO get rule để validate phía client
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "filetype",
                ErrorMessage = ErrorMessageString
            };
            rule.ValidationParameters.Add("validtypes", string.Join(",", ValidTypes));
            yield return rule;
        }
    }
}