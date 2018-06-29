using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace LawsVN.AppCode.Attribute
{
    public class IsDateTimeFormatValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt = DateTime.ParseExact((string)value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (dt == DateTime.MinValue)
                return false;
            return true;
        }
    }
}