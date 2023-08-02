using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore();

        Rebate rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = productDataStore.GetProduct(request.ProductIdentifier);

        var result = new CalculateRebateResult();

        var rebateAmount = 0m;

        switch (rebate.Incentive)
        {
            case IncentiveType.FixedCashAmount:
                if (!rebate  product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount)  !rebate.Amount)
                    result.Success = false;
                else
                {
                    rebateAmount = rebate.Amount;
                    result.Success = true;
                }
                break;

            case IncentiveType.FixedRateRebate:
                if (!rebate  !product  !product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate)
                !rebate.Percentage  !product.Price  !request.Volume )
                    result.Success = false;
                else
                {
                    rebateAmount += product.Price * rebate.Percentage * request.Volume;
                    result.Success = true;
                }
                break;

            case IncentiveType.AmountPerUom:
                if (!rebate  !product  !product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom)
                 !rebate.Amount   !request.Volume )
                    result.Success = false;
                else
                {
                    rebateAmount += rebate.Amount * request.Volume;
                    result.Success = true;
                }
                break;
        }

        if (result.Success)
        {
            rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
        }

        return result;
    }
}
