using Azure.Data.AppConfiguration;
using HelloWorld;
using System;
using System.Threading.Tasks;

namespace HelloWorldExtended
{
	class Program
	{
		static async Task Main(string[] args)
		{

			// Create a ConfigruationClient
			var client = new ConfigurationClient(Settings.AppConfigConnectionString);

			// Asynchronously create configuration settings
			var betaEndpoint = new ConfigurationSetting("endpoint", "https://beta.endpoint.com", "beta");
			var betaInstances = new ConfigurationSetting("instances", "1", "beta");
			var productionEndpoint = new ConfigurationSetting("endpoint", "https://production.endpoint.com", "production");
			var productionInstances = new ConfigurationSetting("instances", "1", "production");

			await client.AddConfigurationSettingAsync(betaEndpoint);
			await client.AddConfigurationSettingAsync(betaInstances);
			await client.AddConfigurationSettingAsync(productionEndpoint);
			await client.AddConfigurationSettingAsync(productionInstances);
			// client.SetConfigurationSettingAsync

			Console.WriteLine("Two keys have been created with two values each");
			Console.WriteLine();
			Console.ReadLine();


			// Asynchronously update a configuration setting
			ConfigurationSetting instancesToUpdate = await client.GetConfigurationSettingAsync(productionInstances.Key, productionInstances.Label);
			instancesToUpdate.Value = "5";
			await client.SetConfigurationSettingAsync(instancesToUpdate);

			Console.WriteLine("The production instance has been updated");
			Console.WriteLine();
			Console.ReadLine();

			// Search by label filter
			var selector = new SettingSelector { LabelFilter = "production" };

			Console.WriteLine("Settings for Production environment:");
			await foreach (ConfigurationSetting setting in client.GetConfigurationSettingsAsync(selector))
			{
				Console.WriteLine($"{setting.Key}\t{setting.Value}");
			}
			Console.WriteLine();
			Console.ReadLine();

			// Asynchronously delete configuration settings
			await client.DeleteConfigurationSettingAsync(betaEndpoint.Key, betaEndpoint.Label);
			await client.DeleteConfigurationSettingAsync(betaInstances.Key, betaInstances.Label);
			await client.DeleteConfigurationSettingAsync(productionEndpoint.Key, productionEndpoint.Label);
			await client.DeleteConfigurationSettingAsync(productionInstances.Key, productionInstances.Label);

			Console.WriteLine("The settings have been deleted");

		}

	}

}