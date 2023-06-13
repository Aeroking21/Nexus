// Import the providers and credential at the top of the page
import { Providers } from '@microsoft/mgt-element';
import { TeamsFxProvider } from '@microsoft/mgt-teamsfx-provider';
import { TeamsUserCredential, TeamsUserCredentialAuthConfig } from "@microsoft/teamsfx";
const authConfig: TeamsUserCredentialAuthConfig = {
    clientId: process.env.REACT_APP_CLIENT_ID,
    initiateLoginEndpoint: process.env.REACT_APP_START_LOGIN_PAGE_URL,
};
const scope = ["User.Read"];
const credential = new TeamsUserCredential(authConfig);
const provider = new TeamsFxProvider(credential, scope);
Providers.globalProvider = provider;
// Put these code in a call-to-action callback function to avoid browser blocking automatically showing up pop-ups. 
await credential.login(this.scope);
Providers.globalProvider.setState(ProviderState.SignedIn);