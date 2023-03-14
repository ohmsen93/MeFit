
import { useNavigate } from "react-router";
import keycloak from "../../keycloak";

const RegisterForm = () => {

    const navigate = useNavigate();

    return (
        <div>
            <div className="actions">
                <button onClick={() => keycloak.register(navigate("profile"))}>Register</button>
            </div>
        </div>
    );
}

export default RegisterForm

