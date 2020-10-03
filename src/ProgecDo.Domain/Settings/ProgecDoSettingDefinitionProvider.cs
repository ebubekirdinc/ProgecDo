using Volo.Abp.Settings;

namespace ProgecDo.Settings
{
    public class ProgecDoSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(ProgecDoSettings.MySetting1));
        }
    }
}
