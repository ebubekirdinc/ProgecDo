using System;
using System.Collections.Generic;
using System.Text;
using ProgecDo.Localization;
using Volo.Abp.Application.Services;

namespace ProgecDo
{
    /* Inherit your application services from this class.
     */
    public abstract class ProgecDoAppService : ApplicationService
    {
        protected ProgecDoAppService()
        {
            LocalizationResource = typeof(ProgecDoResource);
        }
    }
}
