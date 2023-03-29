
import { useNavigate } from "react-router";
import keycloak from "../../keycloak";

const LoginForm = () => {

    const navigate = useNavigate();

    return (
        
                <button className="col-6 btn btn-lg btn-primary" onClick={() => keycloak.login(navigate("dashboard"))}>Login</button>
        
    );
}

export default LoginForm

