using IbanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeBank.Library.IBAN
{
    public static class IBAN
    {
        public static bool ValidateIBAN(string IBANValue)
        {
            IIbanValidator validator = new IbanValidator();
            ValidationResult validationResult = validator.Validate(IBANValue);
            if (validationResult.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GenerateIBANNumber()
        {
            IbanNet.Builders.IbanBuilder builder = new IbanNet.Builders.IbanBuilder();
            var IBANNumber = builder.Build();
            return IBANNumber;
        }
    }
}
