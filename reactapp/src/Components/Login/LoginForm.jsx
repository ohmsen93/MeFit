
import { useNavigate } from "react-router";
import keycloak from "../../keycloak";

const LoginForm = () => {

    const navigate = useNavigate();

    return (
        <div>
            <div className="actions">
                <button onClick={() => keycloak.login(navigate("dashboard"))}>Login</button>
            </div>
        </div>
    );
}

export default LoginForm

