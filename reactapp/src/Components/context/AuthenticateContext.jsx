import {createContext,useState,useContext} from 'react'
import keycloak from '../../keycloak';

const AuthContext = createContext()

export const useAuth = () => {
    return useContext(AuthContext);
}

const AuthProvider = ({children}) => {
    const [auth, setAuth] = useState(keycloak?.authenticated || false)
    const state = {auth, setAuth}

    return (
        <AuthContext.Provider value={state}>
            {children}
        </AuthContext.Provider>
    )
}

export default AuthProvider;