using Azure;
using Azure.Data.AppConfiguration;
using HelloWorld;
using System;

namespace GetSettingIfChanged
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a ConfigurationClient
			ConfigurationClient client = new ConfigurationClient(Settings.AppConfigConnectionString);

			// Get initial setting with ETag
			ConfigurationSetting setting = client.SetConfigurationSetting("some_key", "initial_value");
			Console.WriteLine($"setting.ETag is '{setting.ETag}'");
			Console.ReadLine();

			// Get latest setting
			ConfigurationSetting latestSetting = GetConfigurationSettingIfChanged(client, setting);
			Console.WriteLine($"Latest version of setting is {latestSetting}.");
			Console.ReadLine();

			// Change the setting
			client.SetConfigurationSetting("some_key", "changed_value");
			ConfigurationSetting newLatestSetting = GetConfigurationSettingIfChanged(client, setting);
			Console.WriteLine($"Latest version of setting is {newLatestSetting}.");
			Console.ReadLine();

			// Get latest setting
			ConfigurationSetting newestLatestSetting = GetConfigurationSettingIfChanged(client, newLatestSetting);
			Console.WriteLine($"Latest version of setting is {newestLatestSetting}.");
			Console.ReadLine();

			// Delete a configuration setting
			client.DeleteConfigurationSetting("some_key");

		}

		public static ConfigurationSetting GetConfigurationSettingIfChanged(ConfigurationClient client, ConfigurationSetting setting)
		{
			Response<ConfigurationSetting> response = client.GetConfigurationSetting(setting, onlyIfChanged: true);
			int httpStatusCode = response.GetRawResponse().Status;
			Console.WriteLine($"Received a response code of {httpStatusCode}");

			return httpStatusCode switch
			{
				200 => response.Value,
				304 => setting,
				_ => throw new InvalidOperationException()
			};
		}

	}
}
