import { ensureTeamsSdkInitialized, inTeams } from '/modules/teamsHelpers.js';
import 'https://res.cdn.office.net/teams-js/2.0.0/js/MicrosoftTeams.min.js';

async function getAccessToken(inTeamsTrue) {
    if (await inTeams()) {
        inTeamsTrue.value = true;
        await ensureTeamsSdkInitialized();
        const token = await microsoftTeams.authentication.getAuthToken();
        return { token, inTeamsTrue: inTeamsTrue.value };
    }
    return { token: null, inTeamsTrue: inTeamsTrue.value };
}