import { useEffect } from "react";
import { useNavigate } from "react-router";
import keycloak from "../../keycloak";
import { initializeAuthentication } from "../Security/Authentication";


const LoginForm = () => {
    const navigate = useNavigate();
    const isAuthorized = initializeAuthentication();

    useEffect(() => {
        if (isAuthorized) {            
            navigate("dashboard")
        }
    }, [])

    return (
        <div>
            <h1>Start Page</h1>

            <section className="actions">
                {!isAuthorized && (
                    <button onClick={() => keycloak.login()}>Login</button>
                )}
            </section>

        </div>
    );
}

export default LoginForm