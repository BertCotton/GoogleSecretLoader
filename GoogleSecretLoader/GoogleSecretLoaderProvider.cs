using System.Collections.Generic;
using System.Linq;
using Google.Api.Gax.ResourceNames;
using Google.Cloud.SecretManager.V1;
using Microsoft.Extensions.Configuration;

namespace GoogleSecretLoader
{
    public class GoogleSecretLoaderProvider : ConfigurationProvider
    {
        private readonly string secretPath;
        private readonly string projectId;
        private readonly string applicationName;

        public GoogleSecretLoaderProvider(string secretPath, string projectId, string applicationName)
        {
            this.secretPath = secretPath;
            this.projectId = projectId;
            this.applicationName = applicationName;
        }

        public override void Load()
        {
            var client = new SecretManagerServiceClientBuilder(){ CredentialsPath = secretPath }.Build();
            
            // List all of the secrets for this project
            var listSecrets = client.ListSecrets(new ProjectName(projectId));
            
            // Filter secretes to just those for this application name
            var secretNames = listSecrets.Where(x =>
                    x.Labels.ContainsKey("application") && x.Labels["application"] == applicationName.ToLower())
                .Select(x => x.SecretName);

            // Loop through each secretName and get all of the versions
            foreach (var secretName in secretNames)
            {
                // List all versions for this secret
                var versions = client.ListSecretVersions(new ListSecretVersionsRequest(){ParentAsSecretName = secretName}).ToList();
                // Filter to the latest, enabled, secret
                var version = versions.Where(x => x.State == SecretVersion.Types.State.Enabled).OrderByDescending(x => x.CreateTime).First();
                // Load the value of this secrete
                var secret = client.AccessSecretVersion(version.Name);
                var value = secret.Payload.Data.ToStringUtf8();
                
                // Set the value of the secret into the configuration
                Set(secretName.SecretId, value);
            }
        }
    }
}