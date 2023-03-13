import { useEffect } from "react";
import { useNavigate } from "react-router";
import keycloak from "../../keycloak";
import { useAuth } from "../context/AuthenticateContext";


const LoginForm = () => {

    const {auth, setAuth,setRole} = useAuth();
    const navigate = useNavigate();
    
    useEffect(() => {
        if (auth) {
            setRole(keycloak.tokenParsed.roles)
            navigate("dashboard")
        }
    }, )

    return (
        <div>
            <h1>Start Page</h1>
            <section className="actions">
                {!auth && (
                    <button onClick={() => keycloak.login(setAuth(keycloak.authenticated))}>Login</button>
                )}
            </section>

        </div>
    );
}

export default LoginForm