using Microsoft.Extensions.Configuration;

namespace GoogleSecretLoader
{
    public class GoogleSecretLoader : IConfigurationSource
    {
        private readonly string secretFilePath;
        private readonly string projectId;
        private readonly string applicationName;

        public GoogleSecretLoader(string secretFilePath, string projectId, string applicationName)
        {
            this.secretFilePath = secretFilePath;
            this.projectId = projectId;
            this.applicationName = applicationName;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new GoogleSecretLoaderProvider(secretFilePath, projectId, applicationName);
        }
    }
}