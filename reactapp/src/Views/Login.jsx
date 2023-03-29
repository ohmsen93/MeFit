import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../Components/context/AuthenticateContext";
import LoginForm from "../Components/Login/LoginForm";
import RegisterForm from "../Components/register/RegisterForm";
import Background from "../Images/backgrounds/hd-squad-color.jpeg";

function Login() {

  const { auth } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (auth) {
      { navigate("dashboard") }
    }
  }, [])

  return (
    <>
      <div className="bg">
        <img src={Background} alt=""/>
      </div>
      <div id="loginMain" className="p-5 col-12">
        <h1>MeFit</h1>
        <div id="loginForm" className="container p-5 col-12">
          <div className="btn-group col-12">
            <LoginForm />
            <RegisterForm />
          </div>
        </div>
      </div>

    </>
  )
}

export default Login;