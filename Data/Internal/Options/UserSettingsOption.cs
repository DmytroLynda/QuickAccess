using Data.Internal.DataTypes;

namespace Data.Internal.Options
{
    internal class UserSettingsOption
    {
        public UserSettingsDTO UserSettings { get; set; }

        public UserSettingsOption()
        { }

        public UserSettingsOption(UserSettingsDTO userSettings)
        {
            UserSettings = userSettings;
        }
    }
}
