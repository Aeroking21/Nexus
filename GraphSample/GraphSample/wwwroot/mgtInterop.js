window.mgtInterop = {
  configureProvider: (accessToken) => {
    if (accessToken) {
      let provider = new mgt.SimpleProvider((scopes) => {
        return Promise.resolve(accessToken);
      });

      if (!mgt.Providers.globalProvider) {
        mgt.Providers.globalProvider = provider;
        mgt.Providers.globalProvider.setState(mgt.ProviderState.SignedIn);
      }
    }
  },
};
