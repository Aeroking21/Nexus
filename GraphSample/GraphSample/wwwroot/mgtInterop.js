 
const { Providers, MsalProvider } = require('@microsoft/microsoft-graph-client');

window.mgtInterop = {
  configureProvider: function (accessToken) {
    const mgtProvider = new MsalProvider({
      clientId: '0f0f6aac-da6d-4a9a-938d-42fb95cbda4c',
      scopes: ['user.read'], // Specify the required scopes for accessing Microsoft Graph API
      // Set the access token obtained from Blazor
      accessToken: accessToken,
    });

    // Register the MGT provider
    Providers.globalProvider = mgtProvider;
  },
};