﻿using System.Threading.Tasks;

using Sales.Web.Controllers;

using Shouldly;

using Xunit;

namespace Sales.Web.Tests.Controllers
{
    public class HomeController_Tests : SalesWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
