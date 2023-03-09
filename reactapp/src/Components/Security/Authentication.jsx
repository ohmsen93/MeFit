import keycloak from "../../keycloak"

const authenticated = {authorized : Boolean, role : String};

export const isAuthenticated = () => {
    return authenticated;
}

export const initializeAuthentication = () => {
    authenticated.authorized = keycloak.authenticated;

    if (authenticated.authorized) {
        authenticated.role = keycloak.tokenParsed.roles[0];
    }

    return authenticated.authorized;
    
}


