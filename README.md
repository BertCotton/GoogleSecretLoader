# GoogleSecreteLoader
Sample application for loading secrets from the Google secrete store and setting them in the IConfigurationBuilder

# Files
## GoogleSecretLoaderSource
Implements `IConfigurationSource` and calls the `GoogleSecreteLoaderProvider`

## GoogleSecreteLoaderProvider
Implements `ConfigurationProvider`
This is where the secretes are read from the GCP Secrete store and set into the IConfigurationProvider


# Required Settings
These two environmental variables need to be set
* GoogleSecreteFilePath = `Path to the GCP Secret File`
* GCP_ProjectId = `project_id from the Secret File`
  
Variable that will be loaded from the appsettings.json
* ApplicationName = `lower case application name used as labels for the secrets`
## GCP Secret File
Json file generated as the key.
```json
{
    "type": "service_account",
    "project_id": "ordinal-tractor-311703",
    "private_key_id": "",
    "private_key": "",
    "client_email": "",
    "client_id": "",
    "auth_uri": "https://accounts.google.com/o/oauth2/auth",
    "token_uri": "https://oauth2.googleapis.com/token",
    "auth_provider_x509_cert_url": "https://www.googleapis.com/oauth2/v1/certs",
    "client_x509_cert_url": ""
}
```

