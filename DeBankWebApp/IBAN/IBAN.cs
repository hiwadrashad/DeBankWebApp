using IbanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeBankWebApp.IBAN
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

        public static string GenerateIBANNumber(string Bankaccountnumber, string BankIdentifier, string branchidentifier, IbanNet.Registry.IbanCountry country)
        {
            IbanNet.Builders.IbanBuilder builder = new IbanNet.Builders.IbanBuilder();
            builder.WithBankAccountNumber(Bankaccountnumber);
            builder.WithBankIdentifier(BankIdentifier);
            builder.WithBranchIdentifier(branchidentifier);
            builder.WithCountry(country);
            var IBANNumber = builder.Build();
            return IBANNumber;
        }
    }
}
