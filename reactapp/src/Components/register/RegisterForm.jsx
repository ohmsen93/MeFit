
import { useNavigate } from "react-router";
import keycloak from "../../keycloak";

const RegisterForm = () => {

    const navigate = useNavigate();

    return (
                <button className="col-6 btn btn-lg btn-secondary" onClick={() => keycloak.register(navigate("profile"))}>Register</button>
    );
}

export default RegisterForm

