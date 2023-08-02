using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceTests
{
    [Fact]
    public void Test1()
    {
         var rebateService = new RebateService();
         var request = new CalculateRebateRequest();
         request.setRebateIdentifier("rebase identifier");
         request.setProductIdentifier("product identifier")
         var result = rebateService.Calculate(request);
         //expected and actual
         Assert.Equal(result,"true");
    }

}
