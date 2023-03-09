import { useEffect } from "react";
import { useNavigate } from "react-router";
import keycloak from "../../keycloak";


const LoginForm = () => {

    const isAuthenticated = keycloak.authenticated;
    const navigate = useNavigate();

    useEffect(() => {
        if (isAuthenticated) {
            navigate("dashboard")
            console.log("test");
        }
    }, [])

    return (
        <div>
            <h1>Start Page</h1>

            <section className="actions">
                {!isAuthenticated && (
                    <button onClick={() => keycloak.login()}>Login</button>
                )}
            </section>

        </div>
    );
}

export default LoginForm