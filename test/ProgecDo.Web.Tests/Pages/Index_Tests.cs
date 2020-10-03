using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace ProgecDo.Pages
{
    public class Index_Tests : ProgecDoWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
