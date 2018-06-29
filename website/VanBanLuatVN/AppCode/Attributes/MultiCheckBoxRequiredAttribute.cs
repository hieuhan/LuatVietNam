using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VanBanLuatVN.AppCode.Attributes
{
    /// <summary>
    /// Chỉ định rằng một trường dữ liệu với nhiều lựa chọn là bắt buộc ít nhất 1 lựa chọn
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MultiCheckBoxRequiredAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            return list != null && list.Count > 0;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new ModelClientValidationRule[] { new ModelClientValidationRule { ValidationType = "multicheckboxrequired", ErrorMessage = this.ErrorMessage } };
            //yield return new ModelClientValidationRule
            //{
            //    ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
            //    ValidationType = "multicheckboxrequired"
            //};
        }
    }
}